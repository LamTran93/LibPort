using LibPort.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LibPort.Contexts.EntityConfigurations
{
    public class BookBorrowingRequestDetailsConfiguration : IEntityTypeConfiguration<BookBorrowingRequestDetails>
    {
        public void Configure(EntityTypeBuilder<BookBorrowingRequestDetails> builder)
        {
            builder.HasKey(d => d.Id);

            builder.HasOne(d => d.Book)
                .WithMany()
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.BookBorrowingRequest)
                .WithMany(r => r.Details)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
