using DictionaryApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DictionaryApi.Persistence.Configurations;

// public class TranslationConfiguration : IEntityTypeConfiguration<Translation>
// {
//     public void Configure(EntityTypeBuilder<Translation> builder)
//     {
//         builder.ToTable(nameof(Translation), DictionaryDbContext.DictionarySchema);
//
//         builder.HasKey(translation => translation.Id);
//
//         builder.Property(translation => translation.TranslationId).IsRequired();
//
//         builder.Property(translation => translation.Language)
//             .IsRequired()
//             .HasMaxLength(2)
//             .IsFixedLength()
//             .HasConversion<string>();
//
//         builder.Property(translation => translation.Language)
//             .IsRequired()
//             .HasMaxLength(100)
//             .IsUnicode();
//     }
// }