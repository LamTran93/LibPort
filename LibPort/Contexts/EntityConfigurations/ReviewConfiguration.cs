using LibPort.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibPort.Contexts.EntityConfigurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .HasDefaultValueSql("newsequentialid()");

            builder.Property(r => r.Rating)
                .IsRequired();
            builder.Property(r => r.Comment)
                .IsRequired();

            builder.Property(r => r.CreatedAt)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("getutcdate()");
            builder.Property(r => r.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("getutcdate()");
        }
    }
}
