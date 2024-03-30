using DictionaryApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DictionaryApi.Persistence.Configurations;

public class SubTopicConfiguration : IEntityTypeConfiguration<SubTopic>
{
    public void Configure(EntityTypeBuilder<SubTopic> builder)
    {
        builder.ToTable(nameof(SubTopic), DictionaryDbContext.DictionarySchema);
            
        builder.HasKey(subTopic => subTopic.Id);
        
        //builder.Property(subTopic => subTopic.TranslationId).IsRequired();
        
        builder.OwnsMany(topic => topic.NameTranslations, 
            ownedNavigationBuilder => ownedNavigationBuilder.ToJson());

        
        builder.HasOne(subTopic => subTopic.Topic)
            .WithMany(topic => topic.SubTopics);
    }
}