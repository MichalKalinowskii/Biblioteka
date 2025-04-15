namespace Library.Domain.Rentals;

public class BookRental
{
    public int Id { get; private set; }
    
    public int RentalId { get; private set; }
    
    public Guid BookCopyId { get; private set; }
    
    public DateTime ReturnDate { get; private set; }

    public BookRentalStatus Status { get; private set; }

    internal BookRental(Guid bookCopyId, DateTime returnDate)
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