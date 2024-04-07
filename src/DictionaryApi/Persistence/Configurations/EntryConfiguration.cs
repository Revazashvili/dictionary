using DictionaryApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DictionaryApi.Persistence.Configurations;

public class EntryConfiguration : IEntityTypeConfiguration<Entry>
{
    public void Configure(EntityTypeBuilder<Entry> builder)
    {
        builder.ToTable(nameof(Entry), DictionaryDbContext.DictionarySchema);
        
        builder.HasKey(entry => entry.Id);

        builder.Property(entry => entry.EnglishHeadword)
            .IsUnicode()
            .IsRequired();
        
        builder.Property(entry => entry.GeorgianHeadword)
            .IsUnicode()
            .IsRequired();

        builder.Property(entry => entry.FunctionalLabel)
            .IsUnicode()
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(entry => entry.StylisticQualification)
            .IsUnicode()
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(entry => entry.GeorgianDefinition)
            .IsUnicode()
            .IsRequired();
        
        builder.Property(entry => entry.EnglishDefinition)
            .IsUnicode()
            .IsRequired();
        
        builder.Property(entry => entry.GeorgianIllustrationSentence)
            .IsUnicode()
            .IsRequired();
        
        builder.Property(entry => entry.EnglishIllustrationSentence)
            .IsUnicode()
            .IsRequired();
        
        builder.Property(entry => entry.Source)
            .IsUnicode()
            .HasMaxLength(200);

        builder.Property(entry => entry.Idiom)
            .IsUnicode()
            .HasMaxLength(200);
        
        builder.Property(entry => entry.Synonym)
            .IsUnicode()
            .HasMaxLength(200);

        builder.Property(entry => entry.UsageNote)
            .IsUnicode();

        builder.Property(entry => entry.ImageUrl)
            .IsUnicode()
            .HasMaxLength(500);
    }
}