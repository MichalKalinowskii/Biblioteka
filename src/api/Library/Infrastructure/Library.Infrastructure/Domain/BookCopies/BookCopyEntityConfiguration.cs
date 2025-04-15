using Library.Domain.BookCopies.Models;
using Library.Domain.Books.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Domain.BookCopies
{
    public class BookCopyEntityConfiguration : IEntityTypeConfiguration<BookCopy>
    {
        public void Configure(EntityTypeBuilder<BookCopy> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired();

            builder.Property(x => x.BookId)
                .IsRequired();

            builder.Property(x => x.LocationId)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired()
                .HasConversion(
                    v => v.Id,
                    v => BookCopyStatus.FromValue(v));

            builder.HasOne<Book>()
                .WithMany()
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne<Location>()
            //    .WithMany()
            //    .HasForeignKey(x => x.LocationId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
