using Library.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Locations.Errors
{
    public class LocationErrors
    {
        public static readonly Error InvalidLocationDescription =
            new Error("LocationErrors.InvalidLocationDescription", "Invalid description of laction given");

        public static readonly Error InvalidZoneNumber =
            new Error("LocationErrors.InvalidZoneNumber", "Zone number can't be lesser than 1");

        public static readonly Error InvalidLevelNumber =
            new Error("LocationErrors.InvalidLevelNumber", "Level number can't be lesser than 1");

        public static readonly Error InvalidShellNumber =
            new Error("LocationErrors.InvalidShellNumber", "Shell number can't be lesser than 1");
    }
}
