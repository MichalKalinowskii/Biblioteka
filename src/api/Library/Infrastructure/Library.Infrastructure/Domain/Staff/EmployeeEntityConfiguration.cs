using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Library.Infrastructure.Authentication;
using Microsoft.EntityFrameworkCore;
using Library.Domain.Staff;

namespace Library.Infrastructure.Domain.Staff;

public class EmployeeEntityConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");
        
        builder.HasKey(x => x.UserId);
        
        builder.HasOne<ApplicationUser>()
            .WithOne()
            .HasForeignKey<Employee>(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}