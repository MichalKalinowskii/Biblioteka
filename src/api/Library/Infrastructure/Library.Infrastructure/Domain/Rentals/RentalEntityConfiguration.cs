using Library.Domain.Rentals;
using Library.Domain.Staff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Domain.Rentals;

public class RentalEntityConfiguration : IEntityTypeConfiguration<Rental>
{
    public void Configure(EntityTypeBuilder<Rental> builder)
    {
        builder.ToTable("Rentals");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.EmployeeId)
            .IsRequired();
        
        builder.Property(x => x.RentalDate)
            .IsRequired();
        
        builder.Property(x => x.ReturnDate)
            .IsRequired();

        builder.Property(x => x.LibraryCardId)
            .IsRequired();

        builder.HasOne<Employee>()
            .WithOne()
            .HasForeignKey<Rental>(x => x.EmployeeId)
            .HasConstraintName("FK_Rental_Employee")
            .OnDelete(DeleteBehavior.Cascade);
    }
}