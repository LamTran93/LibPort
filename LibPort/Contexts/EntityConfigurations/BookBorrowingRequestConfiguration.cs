﻿using LibPort.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LibPort.Contexts.EntityConfigurations
{
    public class BookBorrowingRequestConfiguration : IEntityTypeConfiguration<BookBorrowingRequest>
    {
        public void Configure(EntityTypeBuilder<BookBorrowingRequest> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .HasDefaultValueSql("newsequentialid()");

            builder.Property(r => r.RequestedDate)
                .IsRequired();

            builder.Property(r => r.Status)
                .IsRequired();

            builder.Property(r => r.CreatedAt)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("getutcdate()");
            builder.Property(r => r.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("getutcdate()");

            builder.HasOne(r => r.Requestor)
                .WithMany(u => u.BorrowingRequests)
                .HasForeignKey(r => r.RequestorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.Approver)
                .WithMany()
                .HasForeignKey(r => r.ApproverId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
