using DictionaryApi.Entities;
using DictionaryApi.Models;
using DictionaryApi.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DictionaryApi.Services;

public class TopicService : ITopicService
{
    private readonly DictionaryDbContext _context;
    private readonly ISubTopicService _subTopicService;

    public TopicService(DictionaryDbContext context, 
        ISubTopicService _subTopicService)
    {
        _context = context;
        this._subTopicService = _subTopicService;
    }
    
    public async Task<IEnumerable<TopicDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        // var topics = await _context.Topics
        //     .Join(_context.Translations,
        //         topic => topic.TranslationId,
        //         translation => translation.TranslationId,
        //         (topic, translation) => new
        //         {
        //             TopicId = topic.Id,
        //             Translation = translation,
        //         })
        //     .GroupBy(result => result.TopicId)
        //     .Select(grouped => new TopicDto
        //     {
        //         Id = grouped.Key,
        //         NameTranslations = grouped.Select(a => new TranslationModel
        //         {
        //             Language = a.Translation.Language,
        //             Value = a.Translation.Value
        //         })
        //     })
        //     .ToListAsync(cancellationToken);

        return new List<TopicDto>();
    }

    public async Task<TopicDto> GetByIdAsync(int id,CancellationToken cancellationToken)
    {
        var topic = await _context.Topics
            .SingleOrDefaultAsync(topic => topic.Id == id, cancellationToken);

        if (topic is null)
            throw new Exception("Topic not found");

        return new TopicDto
        {
            Id = topic.Id,
            // NameTranslations = await _translationService.GetByTranslationIdAsync(topic.TranslationId, cancellationToken),
            SubTopics = await _subTopicService.GetByTopicIdAsync(topic.Id, cancellationToken)
        };
    }

    public async Task<int> AddAsync(AddTopicRequest request,CancellationToken cancellationToken)
    {
        if (await CheckTopicExistsWithNames(request.NameTranslations, cancellationToken))
            throw new Exception("translation with same names already exists");
            
        var topic = new Topic
        {
            NameTranslations = request.NameTranslations
        };
        var entry = await _context.Topics.AddAsync(topic, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return entry.Entity.Id;
    }

    public async Task UpdateAsync(UpdateTopicRequest request,CancellationToken cancellationToken)
    {
        if (await CheckTopicExistsWithNames(request.NameTranslations, cancellationToken))
            throw new Exception("translation with same names already exists");
        
        var topic = await _context.Topics
            .SingleOrDefaultAsync(topic => topic.Id == request.Id, cancellationToken);

        if (topic is null)
            throw new Exception("Topic not found");

        topic.NameTranslations = request.NameTranslations;
        
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id,CancellationToken cancellationToken)
    {
        var topic = await _context.Topics
            .SingleOrDefaultAsync(topic => topic.Id == id, cancellationToken);

        if (topic is null)
            throw new Exception("Topic not found");
        
        _context.Topics.Remove(topic);
        await _context.SaveChangesAsync(cancellationToken);
    }

    #region Private

    private async Task<bool> CheckTopicExistsWithNames(IEnumerable<Translation> nameTranslations, CancellationToken cancellationToken)
    {
        var georgianTranslation = nameTranslations.First(translation => translation.Language == Language.Ka).Value;
        var englishTranslation = nameTranslations.First(translation => translation.Language == Language.En).Value;

        var exists = await _context.Topics
            .AnyAsync(topic => topic.NameTranslations.Any(translation => translation.Language == Language.Ka && translation.Value == georgianTranslation)
                               || topic.NameTranslations.Any(translation => translation.Language == Language.En && translation.Value == englishTranslation),
                cancellationToken);
        
        return exists;
    }

    #endregion
}