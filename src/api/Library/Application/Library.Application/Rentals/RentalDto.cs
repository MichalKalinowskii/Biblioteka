namespace Library.Application.Rentals;

public record RentalDto(
    int RentalId,
    DateTime RentalDate,
    DateTime ReturnDate,
    int Status,
    Guid BookCopyId,
    string BookTitle,
    string BookDescription
);
