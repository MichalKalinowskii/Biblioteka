using Library.Domain.Authors.Models;
using Library.Domain.Books.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Domain.Authors
{
    public class AuthorBookEntityConfiguration : IEntityTypeConfiguration<AuthorBook>
    {
        public void Configure(EntityTypeBuilder<AuthorBook> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.AuthorId).IsRequired();
            builder.Property(x => x.BookId).IsRequired();

            builder.HasOne<Book>()
                .WithMany()
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Author>()
                .WithMany()
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
