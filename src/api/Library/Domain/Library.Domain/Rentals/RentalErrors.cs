using Library.Domain.SeedWork;

namespace Library.Domain.Rentals;

public class RentalErrors
{
    public static Error NotBorrowedBook() => new Error("Rentals.NotBorrowedBook", "Book has not been borrowed.");
    
    public static Error EmptyBookList() => new Error("Rentals.EmptyBookList", "Book list was empty.");
    
    public static Error AllBooksWereReturned() => new Error("Rentals.AllBooksReturned", "All books were returned.");
}