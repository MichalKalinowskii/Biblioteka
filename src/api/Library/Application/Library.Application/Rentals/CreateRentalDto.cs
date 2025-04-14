namespace Library.Application.Rentals;

public record CreateRentalDto(Guid LibraryCardId, int EmployeeId, List<int> BookCopyIds, DateTime ReturnDate);