using Library.Domain.Locations.Errors;
using Library.Domain.Locations.Models;
using Library.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Locations
{
    public class LocationFactory
    {
        private LocationFactory()
        {
            
        }

        public static Result<Location> Create(int zone, int level, int shell, string description, string locationCode)
        {
            if (zone <= (int)default)
            {
                return Result<Location>.Failure(LocationErrors.InvalidZoneNumber);
            }

            if (level <= (int)default)
            {
                return Result<Location>.Failure(LocationErrors.InvalidLevelNumber);
            }

            if (shell <= (int)default)
            {
                return Result<Location>.Failure(LocationErrors.InvalidShellNumber);
            }

            if (string.IsNullOrWhiteSpace(locationCode))
            {
                return Result<Location>.Failure(LocationErrors.InvalidLocationDescription);
            }

            var location = new Location(Guid.NewGuid(), zone, shell, level, description ?? string.Empty);

            return Result<Location>.Success(location);
        }
    }
}
