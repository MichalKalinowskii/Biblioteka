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
        Task<Result> AddNewLocation(Location location, CancellationToken cancellationToken);
    }
}
