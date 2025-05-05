namespace Library.Application.BookCopy;

public class BookCopyDto
{
    public Guid Id { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }

    public BookCopyDto(Guid BookCopyId, string BookTitle, string BookDescription)
    {
        Id = BookCopyId;
        Title = BookTitle;
        Description = BookDescription;
    }
}