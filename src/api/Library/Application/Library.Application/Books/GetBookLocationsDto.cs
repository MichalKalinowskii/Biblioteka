using Library.Domain.Locations.Models;

namespace Library.Application.Books;

public record GetBookLocationsDto(Library.Domain.Books.Models.Book Book, List<Location> Location);

