namespace Library.Application.Rentals;

public record ReturnBooksDto(Guid LibraryCardId, List<Guid> BookCopyIds);