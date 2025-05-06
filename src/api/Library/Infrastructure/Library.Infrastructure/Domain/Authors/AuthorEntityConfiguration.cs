using Library.Domain.Authors.Models;
using Library.Domain.Books.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Domain.Authors
{
    public class AuthorEntityConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired();

            builder.Property(x => x.DateOfBirth)
                .IsRequired();

            builder.Property(x => x.DateOfDeath);

            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.Description).IsRequired();

            builder.Property(x => x.LastName)
                .IsRequired();
        }
    }
}
