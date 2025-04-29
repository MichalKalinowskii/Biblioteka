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

        public AddNewLocation(Location location, CancellationToken cancellationToken) 
        {

        }

    }
}
