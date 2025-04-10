namespace Library.Domain.Rentals;

public class BookRental
{
    public int Id { get; private set; }
    
    public int RentalId { get; private set; }
    
    public int BookCopyId { get; private set; }
    
    public DateTime ReturnDate { get; private set; }
}