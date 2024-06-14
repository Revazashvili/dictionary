using DictionaryApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DictionaryApi.Persistence.Configurations;

public class MultimediaConfiguration : IEntityTypeConfiguration<Multimedia>
{
    public void Configure(EntityTypeBuilder<Multimedia> builder)
    {
        builder.ToTable(nameof(Multimedia), DictionaryDbContext.DictionarySchema);
            
        builder.HasKey(multimedia => multimedia.Id);

        builder.Property(multimedia => multimedia.FileName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(multimedia => multimedia.Blob)
            .IsRequired();
        
        builder.Property(multimedia => multimedia.ContentType)
            .IsRequired()
            .HasMaxLength(50);
    }
}