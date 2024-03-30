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

        builder.OwnsMany(entry => entry.HeadwordTranslations, 
            ownedNavigationBuilder => ownedNavigationBuilder.ToJson());

        builder.Property(entry => entry.FunctionalLabel)
            .IsUnicode()
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(entry => entry.StylisticQualification)
            .IsUnicode()
            .IsRequired()
            .HasMaxLength(50);
        
        builder.OwnsMany(entry => entry.DefinitionTranslations, 
            ownedNavigationBuilder => ownedNavigationBuilder.ToJson());
        
        builder.OwnsMany(entry => entry.IllustrationSentenceTranslations, 
            ownedNavigationBuilder => ownedNavigationBuilder.ToJson());
        
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
            .IsUnicode()
            .HasMaxLength(500);

        builder.Property(entry => entry.ImageUrl)
            .IsUnicode()
            .HasMaxLength(500);
    }
}