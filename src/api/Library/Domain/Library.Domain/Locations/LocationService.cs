using Library.Domain.Locations.Models;
using Library.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Locations
{
    public class LocationService
    {
        private readonly ILocationPersistance locationPersistance;
        private readonly IUnitOfWork unitOfWork;

        public LocationService(ILocationPersistance locationPersistance, IUnitOfWork unitOfWork)
        {
            this.locationPersistance = locationPersistance;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> AddNewLocation(int zone, 
            int level, 
            int shell, 
            string description, 
            string locationCode,
            CancellationToken cancellationToken)
        {
            var locationCodeExists = await locationPersistance
                .LocationCodeExistsAsync(locationCode, cancellationToken);

            if (locationCodeExists.IsFailure)
            {
                Result.Failure(locationCodeExists.Error);
            }

            if (locationCodeExists.Value)
            {
                return Result.Failure(new Error("LocationCodeAlreadyExists", "Location code already exist"));
            }

            var location = LocationFactory.Create(zone, level, shell, description, locationCode);

            if (location.IsFailure)
            {
                return Result.Failure(location.Error);
            }

            var saveLocationResult = await locationPersistance.AddLocationAsync(location.Value!, cancellationToken);

            if (saveLocationResult.IsFailure)
            {
                return Result.Failure(saveLocationResult.Error);
            }

            await unitOfWork.CommitAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result<List<Location>>> GetLocationByIds(List<Guid> locationIds, CancellationToken cancellationToken)
        {
            var locations = await locationPersistance
                .GetLocationByIdsAsync(locationIds, cancellationToken);

            if (locations.IsFailure)
            {
                return Result<List<Location>>.Failure(locations.Error);
            }

            return Result<List<Location>>.Success(locations.Value!);
        }
    
        public async Task<Result<Location>> GetLocationByCode(string locationCode, CancellationToken cancellationToken)
        {
            var location  = await locationPersistance
                .GetLocationByCodeAsync(locationCode, cancellationToken);
            
            if (location.IsFailure)
            {
                return Result<Location>.Failure(location.Error);
            }

            if (location.Value == null)
            {
                return Result<Location>.Failure(new Error("LocationNotFound", "Location not found"));
            }

            return Result<Location>.Success(location.Value!);
        }
    }
}
