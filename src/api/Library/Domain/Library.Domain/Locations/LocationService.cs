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
            CancellationToken cancellationToken)
        {
            var location = LocationFactory.Create(zone, level, shell, description);

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
    }
}
