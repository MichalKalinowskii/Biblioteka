using Library.Domain.SeedWork;

namespace Library.Domain.Rentals;

public class RentalErrors
{
    public static Error NotBorrowedBook() => new Error("Rentals.NotBorrowedBook", "Book has not been borrowed.");
    
    public static Error EmptyBookList() => new Error("Rentals.EmptyBookList", "Book list was empty.");
    
    public static Error AllBooksWereReturned() => new Error("Rentals.AllBooksReturned", "All books were returned.");
    
    public static Error InvalidLibraryCard() => new Error("Rentals.InvalidLibraryCard", "Library card was invalid.");
    
    public static Error InvalidEmployee() => new Error("Rentals.InvalidEmployee", "Employee was not valid.");
    
    public static Error ReturnDateShouldBePlacedInFuture() => new Error("Rentals.ReturnDateShouldBePlacedInFuture", "Return date should be placed in future.");
    
    public static Error ThereWereNoActiveRentalsForLibraryCard() => new Error("Rentals.ThereWereNoActiveRentalsForLibraryCard", "There were no active rentals for the library card.");
}