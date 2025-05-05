using Library.Application.BookCopy;

namespace Library.Application.Rentals;

public class RentalDto
{
    public int RentalId { get; set; }
    public DateTime RentalDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public int Status { get; set; }
    public List<BookCopyDto> BookCopies { get; set; } =  new List<BookCopyDto>();
}
