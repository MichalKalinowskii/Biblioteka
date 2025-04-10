using Library.Domain.SeedWork;

namespace Library.Domain.Rentals;

public class Rental
{
    public int Id { get; private set; }
    
    public Guid LibraryCardId { get; private set; }
    
    public int EmployeeId { get; private set; }
    
    public DateTime RentalDate { get; private set; }
    
    public DateTime ReturnDate { get; private set; }

    public RentalStatus Status { get; private set; }
    
    public IReadOnlyCollection<BookRental> BookRentals => _bookRentals.AsReadOnly();
    
    private readonly List<BookRental> _bookRentals;
    
    internal Rental(Guid libraryCardId, int employeeId, DateTime returnDate)
    {
        _bookRentals = new List<BookRental>();
        LibraryCardId = libraryCardId;
        EmployeeId = employeeId;
        RentalDate = DateTime.UtcNow;
        ReturnDate = returnDate;
        Status = RentalStatus.InProgress;
    }
    
    internal Result RentBooks(List<int> bookCopyIds, DateTime returnDate)
    {
        if (!bookCopyIds.Any())
        {
            return Result.Failure(RentalErrors.EmptyBookList());
        }

        foreach (var bookCopyId in bookCopyIds)
        {
            _bookRentals.Add(new BookRental(bookCopyId, returnDate));
        }
        
        return Result.Success();
    }

    internal Result ReturnBooks(List<int> bookCopyIds)
    {
        if (Status == RentalStatus.Returned)
        {
            return Result.Failure(RentalErrors.AllBooksWereReturned());
        }
        
        if (!bookCopyIds.Any())
        {
            return Result.Failure(RentalErrors.EmptyBookList());
        }

        if (_bookRentals.Any(bookRental => !bookCopyIds.Contains(bookRental.BookCopyId)))
        {
            return Result.Failure(RentalErrors.NotBorrowedBook());
        }

        foreach (var bookCopyId in bookCopyIds)
        {
            BookRental bookRental = _bookRentals.First(bookRental => bookRental.BookCopyId == bookCopyId);
            
            bookRental.ReturnBook();
        }

        if (AllBooksReturned())
        {
            Status = RentalStatus.Returned;
        }
        
        return Result.Success();
    }

    private bool AllBooksReturned()
    {
        return _bookRentals.All(bookRental => bookRental.Returned());
    }
}