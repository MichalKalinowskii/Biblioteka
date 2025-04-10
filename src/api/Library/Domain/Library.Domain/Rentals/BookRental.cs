namespace Library.Domain.Rentals;

public class BookRental
{
    public int Id { get; private set; }
    
    public int RentalId { get; private set; }
    
    public int BookCopyId { get; private set; }
    
    public DateTime ReturnDate { get; private set; }

    private BookRentalStatus Status;

    internal BookRental(int bookCopyId, DateTime returnDate)
    {
        BookCopyId = bookCopyId;
        ReturnDate = returnDate;
        Status = BookRentalStatus.Borrowed;
    }

    internal void ReturnBook()
    {
        Status = BookRentalStatus.Returned;
        ReturnDate = DateTime.UtcNow;
    }
    
    internal bool Returned() => Status == BookRentalStatus.Returned;
}