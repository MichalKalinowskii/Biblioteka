using Library.Domain.Rentals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Domain.Rentals;

public class BookRentalEntityConfiguration : IEntityTypeConfiguration<BookRental>
{
    public void Configure(EntityTypeBuilder<BookRental> builder)
    {
        builder.ToTable("BookRentals");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ReturnDate);
            
        builder.Property(x => x.RentalId).IsRequired();
        
        builder.Property(x => x.BookCopyId).IsRequired();
        
        builder.HasOne<Rental>()
            .WithMany()
            .HasForeignKey(x => x.RentalId)
            .HasConstraintName("FK_BookRental_Rentals")
            .OnDelete(DeleteBehavior.Cascade);
            
    }
}