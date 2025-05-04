namespace Library.Application.Rentals;

public record RentalDto(
    int RentalId,
    DateTime RentalDate,
    DateTime? ReturnDate,
    Guid BookCopyId,
    string BookTitle,
    string BookDescription
);
