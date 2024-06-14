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

    public async Task<int> GetCountAsync(CancellationToken cancellationToken)
    {
        var count = await _context.Topics.CountAsync(cancellationToken);

        return count;
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
        if (await CheckTopicExistsWithNames(request.GeorgianName, request.EnglishName, cancellationToken))
            throw new Exception("translation with same names already exists");

        var topic = new Topic
        {
            GeorgianName = request.GeorgianName,
            EnglishName = request.EnglishName,
            Status = EntityStatus.InActive
        };
        var entry = await _context.Topics.AddAsync(topic, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return entry.Entity.Id;
    }

    public async Task<int> AddSubTopicAsync(AddSubTopicRequest request, CancellationToken cancellationToken)
    {
        var subTopicExistsWithThisNames = await _context.SubTopics
            .AnyAsync(subTopic => subTopic.GeorgianName == request.GeorgianName || subTopic.EnglishName == request.EnglishName, cancellationToken);
        if (subTopicExistsWithThisNames)
            throw new Exception("translation with same names already exists");

        var topic = await GetByIdAsync(request.TopicId, cancellationToken);

        var subTopic = new SubTopic
        {
            GeorgianName = request.GeorgianName,
            EnglishName = request.EnglishName,
            Status = EntityStatus.InActive
        };
        topic.SubTopics.Add(subTopic);

        await _context.SaveChangesAsync(cancellationToken);

        return subTopic.Id;
    }

    public async Task UpdateAsync(UpdateTopicRequest request,CancellationToken cancellationToken)
    {
        if (await CheckTopicExistsWithNames(request.GeorgianName, request.EnglishName, cancellationToken))
            throw new Exception("translation with same names already exists");
        
        var topic = await GetByIdAsync(request.Id, cancellationToken);

        topic.GeorgianName = request.GeorgianName;
        topic.EnglishName = request.EnglishName;
        
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateSubTopicAsync(UpdateSubTopicRequest request, CancellationToken cancellationToken)
    {
        var subTopic = await GetSubTopicAsync(request.Id, cancellationToken);

        subTopic.GeorgianName = request.GeorgianName;
        subTopic.EnglishName = request.EnglishName;

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
    
    public async Task<SubTopic> GetSubTopicAsync(int subTopicId, CancellationToken cancellationToken)
    {
        var subTopic = await _context.SubTopics
            .SingleOrDefaultAsync(subTopic => subTopic.Id == subTopicId, cancellationToken);

        if (subTopic is null)
            throw new Exception("SubTopic not found");
        
        return subTopic;
    }

    public async Task ActivateTopicAsync(int id, CancellationToken cancellationToken)
    {
        var topic = await GetByIdAsync(id, cancellationToken);

        if (topic.Status == EntityStatus.Active)
            throw new Exception("Topic is already active");
        
        topic.Status = EntityStatus.Active;

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeactivateTopicAsync(int id, CancellationToken cancellationToken)
    {
        var topic = await GetByIdAsync(id, cancellationToken);

        if (topic.Status == EntityStatus.InActive)
            throw new Exception("Topic is already inactive");
        
        topic.Status = EntityStatus.InActive;

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeactivateSubTopicAsync(int id, CancellationToken cancellationToken)
    {
        var subTopic = await GetSubTopicAsync(id, cancellationToken);
        
        if (subTopic.Status == EntityStatus.InActive)
            throw new Exception("SubTopic is already inactive");

        subTopic.Status = EntityStatus.InActive;

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task ActivateSubTopicAsync(int id, CancellationToken cancellationToken)
    {
        var subTopic = await GetSubTopicAsync(id, cancellationToken);
        
        if (subTopic.Status == EntityStatus.Active)
            throw new Exception("SubTopic is already active");

        subTopic.Status = EntityStatus.Active;

        await _context.SaveChangesAsync(cancellationToken);
    }


    #region Private

    private async Task<bool> CheckTopicExistsWithNames(string georgianName, string englishName, CancellationToken cancellationToken)
    {
        var topicExistsWithThisNames = await _context.Topics
            .AnyAsync(topic => topic.GeorgianName == georgianName || topic.EnglishName == englishName, cancellationToken);
        
        return topicExistsWithThisNames;
    }
    
    #endregion
}