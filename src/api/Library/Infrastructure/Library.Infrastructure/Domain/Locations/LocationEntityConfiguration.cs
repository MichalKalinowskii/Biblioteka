using Library.Domain.Locations.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Domain.Locations
{
    internal class LocationEntityConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Zone).IsRequired();

            builder.Property(b => b.Level).IsRequired();

            builder.Property(b => b.Shell).IsRequired();

            builder.Property(b => b.Description);

            builder.Property(b => b.LocationCode).IsRequired();
        }
    }
}
