using Library.Domain.Locations.Models;

namespace Library.Application.Locations;

public record LocationBooksDto(Location Location, List<Library.Domain.Books.Models.Book> Books);
