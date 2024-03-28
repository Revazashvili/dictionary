using DictionaryApi.Models;
using DictionaryApi.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DictionaryApi.Services;

public class SubTopicService : ISubTopicService
{
    private readonly DictionaryDbContext _context;
    public SubTopicService(DictionaryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SubTopicDto>> GetByTopicIdAsync(int topicId, CancellationToken cancellationToken)
    {
        var subTopics = await _context.SubTopics
            .Where(subTopic => subTopic.Topic.Id == topicId)
            .Join(_context.Translations,
                subTopic => subTopic.TranslationId,
                translation => translation.TranslationId,
                (subTopic, translation) => new
                {
                    SubTopicId =subTopic.Id,
                    Translation = translation
                })
            .GroupBy(result => result.SubTopicId)
            .Select(grouped => new SubTopicDto
            {
                Id = grouped.Key,
                NameTranslations = grouped.Select(a => new TranslationModel
                {
                    Language = a.Translation.Language,
                    Value = a.Translation.Value
                })
            })
            .ToListAsync(cancellationToken);

        return subTopics;
    }
}