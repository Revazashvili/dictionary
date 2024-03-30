using DictionaryApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DictionaryApi.Persistence.Configurations;

public class TopicConfiguration : IEntityTypeConfiguration<Topic>
{
    public void Configure(EntityTypeBuilder<Topic> builder)
    {
        builder.ToTable(nameof(Topic), DictionaryDbContext.DictionarySchema);
        
        builder.HasKey(topic => topic.Id);

        builder.OwnsMany(topic => topic.NameTranslations, 
            ownedNavigationBuilder => ownedNavigationBuilder.ToJson());
        
        builder.HasMany(topic => topic.SubTopics)
            .WithOne(subTopic => subTopic.Topic)
            .OnDelete(DeleteBehavior.Cascade);
    }
}