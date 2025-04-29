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
    }
}
