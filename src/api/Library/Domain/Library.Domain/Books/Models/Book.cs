using Library.Domain.Books.Entites;
using Library.Domain.Books.Interfaces;
using Library.Domain.SeedWork;

namespace Library.Domain.Books.Models;

public class Book
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string TitlePageImageUrl { get; set; }
    public Genre Genre { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public string ISBN { get; set; }
    public string Publisher { get; set; }


    private IBookPersistence bookPersistence;

    public Book(BookEntity book)
    {
        Id = Guid.NewGuid();
        Title = book.Title;
        TitlePageImageUrl = book.TitlePageImageUrl;
        Genre = new Genre(book.GenreName);
        ReleaseDate = book.ReleaseDate;
        Description = book.Description;
        ISBN = book.ISBN;
        Publisher = book.Publisher;
    }

    public void ChangeGenre(string newGenreName)
    {
        Genre = new Genre(newGenreName);
    }
}