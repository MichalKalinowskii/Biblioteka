using Library.Domain.Locations.Models;
using Library.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Locations
{
    public interface ILocationPersistance
    {
        Task<Result> AddLocationAsync(Location location, CancellationToken cancellationToken);
        Task<Result<List<Location>>> GetLocationByIdsAsync(List<Guid> locationIds, CancellationToken cancellationToken);
        Task<Result<List<Location>>> GetAllLocationsAsync(CancellationToken cancellationToken);
        Task<Result<bool>> LocationCodeExistsAsync(string locationCode, CancellationToken cancellationToken);
        Task<Result<Location>> GetLocationByCodeAsync(string locationCode, CancellationToken cancellationToken);
    }
}
