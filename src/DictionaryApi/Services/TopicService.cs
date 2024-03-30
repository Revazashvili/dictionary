using DictionaryApi.Entities;
using DictionaryApi.Models;
using DictionaryApi.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DictionaryApi.Services;

public class TopicService : ITopicService
{
    private readonly DictionaryDbContext _context;

    public TopicService(DictionaryDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Topic>> GetAllAsync(CancellationToken cancellationToken)
    {
        var topics = await _context.Topics
            .Include(topic => topic.SubTopics)
            .ToListAsync(cancellationToken);
        
        return topics;
    }

    public async Task<Topic> GetByIdAsync(int id,CancellationToken cancellationToken)
    {
        var topic = await _context.Topics
            .Include(topic => topic.SubTopics)
            .SingleOrDefaultAsync(topic => topic.Id == id, cancellationToken);

        if (topic is null)
            throw new Exception("Topic not found");

        return topic;
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

    public async Task<int> AddSubTopicAsync(AddSubTopicRequest request, CancellationToken cancellationToken)
    {
        if (await CheckSubTopicExistsWithNames(request.NameTranslations, cancellationToken))
            throw new Exception("translation with same names already exists");

        var topic = await GetByIdAsync(request.TopicId, cancellationToken);

        var subTopic = new SubTopic
        {
            NameTranslations = request.NameTranslations
        };
        topic.SubTopics.Add(subTopic);

        await _context.SaveChangesAsync(cancellationToken);

        return subTopic.Id;
    }

    public async Task UpdateAsync(UpdateTopicRequest request,CancellationToken cancellationToken)
    {
        if (await CheckTopicExistsWithNames(request.NameTranslations, cancellationToken))
            throw new Exception("translation with same names already exists");
        
        var topic = await GetByIdAsync(request.Id, cancellationToken);

        topic.NameTranslations = request.NameTranslations;
        
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateSubTopicAsync(UpdateSubTopicRequest request, CancellationToken cancellationToken)
    {
        var subTopic = await GetSubTopicAsync(request.Id, cancellationToken);

        subTopic.NameTranslations = request.NameTranslations;

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id,CancellationToken cancellationToken)
    {
        var topic = await GetByIdAsync(id, cancellationToken);
        
        _context.Topics.Remove(topic);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteSubTopicAsync(int id, CancellationToken cancellationToken)
    {
        var subTopic = await GetSubTopicAsync(id, cancellationToken);

        _context.SubTopics.Remove(subTopic);

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
    
    private async Task<bool> CheckSubTopicExistsWithNames(IEnumerable<Translation> nameTranslations, CancellationToken cancellationToken)
    {
        var georgianTranslation = nameTranslations.First(translation => translation.Language == Language.Ka).Value;
        var englishTranslation = nameTranslations.First(translation => translation.Language == Language.En).Value;

        var exists = await _context.SubTopics
            .AnyAsync(topic => topic.NameTranslations.Any(translation => translation.Language == Language.Ka && translation.Value == georgianTranslation)
                               || topic.NameTranslations.Any(translation => translation.Language == Language.En && translation.Value == englishTranslation),
                cancellationToken);
        
        return exists;
    }

    private async Task<SubTopic> GetSubTopicAsync(int subTopicId, CancellationToken cancellationToken)
    {
        var subTopic = await _context.SubTopics
            .SingleOrDefaultAsync(subTopic => subTopic.Id == subTopicId, cancellationToken);

        if (subTopic is null)
            throw new Exception("SubTopic not found");
        
        return subTopic;
    }
    
    #endregion
}