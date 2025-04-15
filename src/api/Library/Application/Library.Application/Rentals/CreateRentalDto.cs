namespace Library.Application.Rentals;

public record CreateRentalDto(Guid LibraryCardId, int EmployeeId, List<Guid> BookCopyIds, DateTime ReturnDate);