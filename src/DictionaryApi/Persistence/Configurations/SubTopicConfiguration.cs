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
        
        builder.Property(subTopic => subTopic.GeorgianName)
            .IsUnicode()
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(subTopic => subTopic.EnglishName)
            .IsUnicode()
            .IsRequired()
            .HasMaxLength(200);
        
        builder.HasOne(subTopic => subTopic.Topic)
            .WithMany(topic => topic.SubTopics);
    }
}