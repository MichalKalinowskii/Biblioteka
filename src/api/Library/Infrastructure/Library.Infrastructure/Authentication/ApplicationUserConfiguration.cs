using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Library.Domain.Staff;

namespace Library.Infrastructure.Authentication;

internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("Users");

        builder.Property(u => u.FirstName).HasMaxLength(100).IsRequired();
        builder.Property(u => u.LastName).HasMaxLength(100).IsRequired();

        builder.HasOne<Employee>()
            .WithOne()
            .HasConstraintName("FK_ApplicationUser_Employee")
            .OnDelete(DeleteBehavior.Restrict);
    }
}