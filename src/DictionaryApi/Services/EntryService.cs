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

    public EntryService(DictionaryDbContext context, ITopicService topicService)
    {
        _context = context;
        _topicService = topicService;
    }

    public async Task<IEnumerable<Entry>> GetAllAsync(Pagination pagination, CancellationToken cancellationToken)
    {
        var entries = await _context.Entries
            .Paged(pagination)
            .ToListAsync(cancellationToken);

        return entries;
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
            .Where(entry => entry.HeadwordTranslations.Select(headwordTranslation => headwordTranslation.Value).Contains(searchText))
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
            HeadwordTranslations = request.HeadwordTranslations,
            DefinitionTranslations = request.DefinitionTranslations,
            FunctionalLabel = request.FunctionalLabel,
            Idiom = request.Idiom,
            IllustrationSentenceTranslations = request.IllustrationSentenceTranslations,
            ImageUrl = request.ImageUrl,
            Source = request.Source,
            StylisticQualification = request.StylisticQualification,
            Synonym = request.Synonym,
            UsageNote = request.UsageNote,
            SubTopic = subTopic
        };

        var entityEntry = await _context.Entries.AddAsync(entry, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return entityEntry.Entity.Id;
    }

    public async Task UpdateAsync(UpdateEntryRequest request, CancellationToken cancellationToken)
    {
        var entry = await GetByIdAsync(request.Id, cancellationToken);
        var subTopic = await _topicService.GetSubTopicAsync(request.SubTopicId, cancellationToken);

        entry.HeadwordTranslations = request.HeadwordTranslations;
        entry.DefinitionTranslations = request.DefinitionTranslations;
        entry.FunctionalLabel = request.FunctionalLabel;
        entry.Idiom = request.Idiom;
        entry.IllustrationSentenceTranslations = request.IllustrationSentenceTranslations;
        entry.ImageUrl = request.ImageUrl;
        entry.Source = request.Source;
        entry.StylisticQualification = request.StylisticQualification;
        entry.Synonym = request.Synonym;
        entry.UsageNote = request.UsageNote;
        entry.SubTopic = subTopic;

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var entry = await GetByIdAsync(id, cancellationToken);

        _context.Entries.Remove(entry);

        await _context.SaveChangesAsync(cancellationToken);
    }
}