using Library.Domain.Books.Models;
using Library.Domain.Locations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Book
{
    public class GetBookLocationsDto
    {
        public Library.Domain.Books.Models.Book Book { get; set; }
        public List<Location> Locations { get; set; }
    }
}
