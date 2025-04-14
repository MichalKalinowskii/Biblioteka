using Library.Domain.Books.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Domain.Books
{
    public class BookEntityConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Title)
                .IsRequired();

            builder.Property(b => b.TitlePageImageUrl);

            builder.Property(b => b.ReleaseDate)
                .IsRequired();

            builder.Property(b => b.Description);

            builder.Property(b => b.ISBN)
                .IsRequired()
                .HasMaxLength(13);

            builder.Property(b => b.Publisher);

            builder.HasOne(b => b.Genre)
                .WithMany()
                .HasForeignKey("Book_GenreId")
                .IsRequired();
        }
    }
}
