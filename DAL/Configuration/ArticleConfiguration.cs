using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    internal class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.Property(article => article.Title)
                .IsRequired()
                .HasMaxLength(75);

            builder
                .HasMany(article => article.Blocks)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
