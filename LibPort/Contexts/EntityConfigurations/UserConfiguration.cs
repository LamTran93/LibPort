using LibPort.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LibPort.Contexts.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasDefaultValueSql("newsequentialid()");

            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(80);

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(80);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.UserType)
                .IsRequired();

            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("getutcdate()");
            builder.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("getutcdate()");

            builder.HasMany(u => u.BorrowingRequests)
                .WithOne(r => r.Requestor)
                .HasForeignKey(r => r.RequestorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
