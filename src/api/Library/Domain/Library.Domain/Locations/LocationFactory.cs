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

        public Result<Location> Create(int zone, int level, int shell, string description)
        {
            if (zone <= (int)default)
            {

            }

            if (level <= (int)default)
            {

            }

            if (shell <= (int)default)
            {

            }

            var location = new Location(Guid.NewGuid(), zone, shell, level, description ?? string.Empty);

            return Result<Location>.Success(location);
        }
    }
}
