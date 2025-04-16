namespace Library.Application.Book;

public record CreateBookDto(string publisher,
            string title,
            string titlePageImageUrl,
            DateTime releaseDate,
            string description,
            string ISBN,
            string genrenName);