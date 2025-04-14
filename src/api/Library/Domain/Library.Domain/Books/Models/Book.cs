using Library.Domain.Books.Entites;
using Library.Domain.Books.Interfaces;
using Library.Domain.SeedWork;

namespace Library.Domain.Books.Models;

public class Book
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string TitlePageImageUrl { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public string ISBN { get; set; }
    public string Publisher { get; set; }
    public Genre Genre { get; set; }

    public Book(string title, string titlePageImageUrl,
        DateTime releaseDate, string description, string ISBN, string publisher, Genre genre)
    {
        Id = Guid.NewGuid();
        Title = title;
        TitlePageImageUrl = titlePageImageUrl;
        Genre = genre;
        ReleaseDate = releaseDate;
        Description = description;
        this.ISBN = ISBN;
        Publisher = publisher;
    }

    public void ChangeGenre(Genre newGenre)
    {
        Genre = newGenre;
    }
}