using LibPort.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LibPort.Contexts.EntityConfigurations
{
    public class BookBorrowingRequestConfiguration : IEntityTypeConfiguration<BookBorrowingRequest>
    {
        public void Configure(EntityTypeBuilder<BookBorrowingRequest> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.RequestedDate)
                .IsRequired();

            builder.Property(r => r.Status)
                .IsRequired();

            builder.HasOne(r => r.Requestor)
                .WithMany(u => u.BorrowingRequests)
                .HasForeignKey(r => r.RequestorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.Approver)
                .WithMany()
                .HasForeignKey(r => r.ApproverId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
