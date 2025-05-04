using Library.Domain.Locations.Models;

namespace Library.Application.Books
{
    public class GetBookLocationsDto
    {
        public Library.Domain.Books.Models.Book Book { get; set; }
        public List<Location> Locations { get; set; }
    }
}
