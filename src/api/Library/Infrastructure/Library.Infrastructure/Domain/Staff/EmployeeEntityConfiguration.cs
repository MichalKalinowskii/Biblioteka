using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Library.Domain.Staff;

namespace Library.Infrastructure.Domain.Staff;

public class EmployeeEntityConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id).IsRequired();
    }
}