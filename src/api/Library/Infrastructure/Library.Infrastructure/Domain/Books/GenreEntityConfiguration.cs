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
    public class GenreEntityConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(g => g.Id);
            builder.Property(g => g.Name)
                   .IsRequired();

            builder.HasData(
                Genre.Comedy,
                Genre.Thriller,
                Genre.Drama,
                Genre.Horror,
                Genre.Romance,
                Genre.SinceFiction
            );
        }
    }
}
