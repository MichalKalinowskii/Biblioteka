using Library.Domain.Clients;
using Library.Infrastructure.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Domain.Clients;

public class ClientEntityConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Clients");
        
        builder.HasKey(x => x.UserId);
        
        builder.Property(x => x.LibraryCardId).IsRequired();
        
        builder.HasOne<ApplicationUser>()
            .WithOne()
            .HasForeignKey<Client>(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}