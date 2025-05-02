using Library.Domain.Locations;
using Library.Domain.Locations.Models;
using Library.Domain.SeedWork;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Domain.Locations;

public class LocationRepository : ILocationPersistance
{
    private readonly DbSet<Location> locationContext;

    public LocationRepository(LibraryContext context)
    {
        locationContext = context.Set<Location>();
    }

    public async Task<Result> AddLocationAsync(Location location, CancellationToken cancellationToken)
    {
        Result result = default;

        try
        {
            await locationContext.AddAsync(location, cancellationToken);
            result = Result.Success();
        }
        catch (Exception ex)
        {
            result = Result.Failure(new Error("LocationRepository.AddLocationAsync", ex.Message));
        }

        return result!;
    }
}