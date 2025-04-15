using Library.Domain.BookCopies.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Domain.BookCopies
{
    public class BookCopyStatusEntityConfiguration : IEntityTypeConfiguration<BookCopyStatus>
    {
        public void Configure(EntityTypeBuilder<BookCopyStatus> builder)
        {
            builder.HasKey(bcs => bcs.Id);
            builder.Property(bcs => bcs.Name)
                   .IsRequired();

            builder.HasData(
                BookCopyStatus.Available,
                BookCopyStatus.Reserved,
                BookCopyStatus.Lost,
                BookCopyStatus.Damaged,
                BookCopyStatus.Unavailable
            );
        }
    }
}
