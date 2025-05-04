namespace Library.Application.Books;

public record CreateBookDto(string publisher,
    string title,
    string titlePageImageUrl,
    DateTime releaseDate,
    string description,
    string ISBN,
    string genreName);