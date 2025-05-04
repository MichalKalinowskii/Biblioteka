namespace Library.Application.Rentals;

public record CreateRentalDto(Guid LibraryCardId, List<Guid> BookCopyIds, DateTime ReturnDate);