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

        builder.Property(topic => topic.GeorgianName)
            .IsUnicode()
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(topic => topic.EnglishName)
            .IsUnicode()
            .IsRequired()
            .HasMaxLength(200);
        
        builder.HasMany(topic => topic.SubTopics)
            .WithOne(subTopic => subTopic.Topic)
            .OnDelete(DeleteBehavior.Cascade);
    }
}