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

    public async Task<Result<Location>> GetLocationByCodeAsync(string locationCode, CancellationToken cancellationToken)
    {
        Result<Location> result = default;

        try
        {
            var location = await locationContext
                .FirstOrDefaultAsync(x => x.LocationCode == locationCode, cancellationToken);
            result = Result<Location>.Success(location);
        }
        catch(Exception ex)
        {
            result = Result<Location>.Failure(new Error("LocationRepository.GetLocationByCode", ex.Message));
        }

        return result;
    }

    public async Task<Result<List<Location>>> GetLocationByIdsAsync(List<Guid> locationIds, CancellationToken cancellationToken)
    {
        Result<List<Location>> result = default;

        try
        {
            var locations = await locationContext
                .Where(x => locationIds.Contains(x.Id))
                .ToListAsync(cancellationToken);
            result = Result<List<Location>>.Success(locations);
        }
        catch (Exception ex)
        {
            result = Result<List<Location>>.Failure(new Error("LocationRepository.GetLocationByIdsAsync", ex.Message));
        }

        return result!;
    }

    public async Task<Result<bool>> LocationCodeExistsAsync(string locationCode, CancellationToken cancellationToken)
    {
        Result<bool> result = default;

        try
        {
            var locationCodeExists = await locationContext
                .AnyAsync(x => x.LocationCode == locationCode, cancellationToken);
            result = Result<bool>.Success(locationCodeExists);
        }
        catch (Exception ex)
        {
            result = Result<bool>.Failure(new Error("LocationRepository.LocationCodeExistsAsync", ex.Message));
        }

        return result!;
    }
}