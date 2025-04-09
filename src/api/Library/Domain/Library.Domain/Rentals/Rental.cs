namespace Library.Domain.Rentals;

public class Rental
{
    public int Id { get; private set; }
    
    public Guid LibraryCardId { get; private set; }
    
    public int EmployeeId { get; private set; }
    
    public DateTime RentalDate { get; private set; }
    
    public DateTime ReturnDate { get; private set; }
    
    private readonly List <BookRental> _bookRentals = new List<BookRental>();
}