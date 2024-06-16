using DictionaryApi.Entities;
using DictionaryApi.Extensions;
using DictionaryApi.Models;
using DictionaryApi.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DictionaryApi.Services;

internal class EntryService : IEntryService
{
    private readonly DictionaryDbContext _context;
    private readonly ITopicService _topicService;
    private readonly IMultimediaService _multimediaService;

    public EntryService(DictionaryDbContext context, ITopicService topicService, IMultimediaService multimediaService)
    {
        _context = context;
        _topicService = topicService;
        _multimediaService = multimediaService;
    }

    public async Task<IEnumerable<Entry>> GetAllAsync(Pagination pagination, CancellationToken cancellationToken)
    {
        var entries = await _context.Entries
            .Paged(pagination)
            .ToListAsync(cancellationToken);

        return entries;
    }

    public async Task<int> GetCountAsync(CancellationToken cancellationToken)
    {
        var count = await _context.Entries.CountAsync(cancellationToken);

        return count;
    }

    public async Task<IEnumerable<Entry>> GetAllForSubTopicAsync(int subTopicId, Pagination pagination, CancellationToken cancellationToken)
    {
        var entries = await _context.Entries
            .Where(entry => entry.SubTopic.Id == subTopicId)
            .Paged(pagination)
            .ToListAsync(cancellationToken);

        return entries;
    }

    public async Task<IEnumerable<Entry>> SearchAsync(string searchText, CancellationToken cancellationToken)
    {
        var entries = await _context.Entries
            .Where(entry => entry.GeorgianHeadword.Contains(searchText) || entry.EnglishHeadword.Contains(searchText))
            .ToListAsync(cancellationToken);

        return entries;
    }

    public async Task<Entry> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var entry = await _context.Entries
            .FirstOrDefaultAsync(entry => entry.Id == id, cancellationToken);

        if (entry is null)
            throw new Exception("entry not found");

        return entry;
    }

    public async Task<int> AddAsync(AddEntryRequest request, CancellationToken cancellationToken)
    {
        var subTopic = await _topicService.GetSubTopicAsync(request.SubTopicId, cancellationToken);

        var entry = new Entry
        {
            GeorgianHeadword = request.GeorgianHeadword,
            EnglishHeadword = request.EnglishHeadword,
            GeorgianDefinition = request.GeorgianDefinition,
            EnglishDefinition = request.EnglishDefinition,
            FunctionalLabel = request.FunctionalLabel,
            Idiom = request.Idiom,
            GeorgianIllustrationSentence = request.GeorgianIllustrationSentence,
            EnglishIllustrationSentence = request.EnglishIllustrationSentence,
            ImageUrl = request.ImageUrl,
            Source = request.Source,
            StylisticQualification = request.StylisticQualification,
            Synonym = request.Synonym,
            UsageNote = request.UsageNote,
            SubTopic = subTopic,
            Status = EntityStatus.InActive
        };

        var entityEntry = await _context.Entries.AddAsync(entry, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return entityEntry.Entity.Id;
    }

    public async Task UpdateAsync(UpdateEntryRequest request, CancellationToken cancellationToken)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var entry = await GetByIdAsync(request.Id, cancellationToken);
            var subTopic = await _topicService.GetSubTopicAsync(request.SubTopicId, cancellationToken);

            entry.GeorgianHeadword = request.GeorgianHeadword;
            entry.EnglishHeadword = request.EnglishHeadword;
            entry.GeorgianDefinition = request.GeorgianDefinition;
            entry.EnglishDefinition = request.EnglishDefinition;
            entry.FunctionalLabel = request.FunctionalLabel;
            entry.Idiom = request.Idiom;
            entry.GeorgianIllustrationSentence = request.GeorgianIllustrationSentence;
            entry.EnglishIllustrationSentence = request.EnglishIllustrationSentence;
            if (entry.ImageUrl != request.ImageUrl)
            {
                await _multimediaService.RemoveAsync(entry.ImageUrl, cancellationToken);
                entry.ImageUrl = request.ImageUrl;
            }

            entry.Source = request.Source;
            entry.StylisticQualification = request.StylisticQualification;
            entry.Synonym = request.Synonym;
            entry.UsageNote = request.UsageNote;
            entry.SubTopic = subTopic;

            await _context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var entry = await GetByIdAsync(id, cancellationToken);

            _context.Entries.Remove(entry);

            await _context.SaveChangesAsync(cancellationToken);

            await _multimediaService.RemoveAsync(entry.ImageUrl, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
        }
    }

    public async Task ActivateAsync(int id, CancellationToken cancellationToken)
    {
        var entry = await GetByIdAsync(id, cancellationToken);

        if (entry.Status == EntityStatus.Active)
            throw new Exception("Entry is already active");

        entry.Status = EntityStatus.Active;

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeactivateAsync(int id, CancellationToken cancellationToken)
    {
        var entry = await GetByIdAsync(id, cancellationToken);

        if (entry.Status == EntityStatus.InActive)
            throw new Exception("Entry is already inactive");

        entry.Status = EntityStatus.InActive;

        await _context.SaveChangesAsync(cancellationToken);
    }
}