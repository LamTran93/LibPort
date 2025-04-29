using LibPort.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LibPort.Contexts.EntityConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.CreatedAt)
                .HasDefaultValueSql("getutcdate()");
            builder.Property(c => c.UpdatedAt)
                .HasDefaultValueSql("getutcdate()");

            builder.HasData(
                new Category { Id = 1, Name = "Comedy" },
                new Category { Id = 2, Name = "Documentary" },
                new Category { Id = 3, Name = "Drama" },
                new Category { Id = 4, Name = "Romance" },
                new Category { Id = 5, Name = "Action" },
                new Category { Id = 6, Name = "Fantasy" },
                new Category { Id = 7, Name = "Horror" },
                new Category { Id = 8, Name = "Crime" },
                new Category { Id = 9, Name = "Mystery" },
                new Category { Id = 10, Name = "Adventure" },
                new Category { Id = 11, Name = "Western" },
                new Category { Id = 12, Name = "Animation" }
            );
        }
    }
}
