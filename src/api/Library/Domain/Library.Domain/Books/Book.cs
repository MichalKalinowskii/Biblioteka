using Library.Domain.Books.Entites;
using Library.Domain.Books.Interfaces;
using Library.Domain.SeedWork;

namespace Library.Domain.Books;

public class Book
{
    private BookEntity book;
    private IBookPersistence bookPersistence;

    public Book(BookEntity book, IBookPersistence bookPersistence)
    {
        this.book = book;
        this.bookPersistence = bookPersistence;
    }

    public void ChangeGenre(Genre genre)
    {
        if (genre == null)
        {
            throw new Exception("Genre is required");
        }

        if (string.IsNullOrWhiteSpace(genre.Name))
        {
            throw new Exception("Genre name is required");
        }   

        this.book.GenreId = genre.Id;
        this.book.Genre = genre;
    }

    public void AddAuthors(List<AuthorEntity> authors)
    {
        if (!authors.Any())
        {
            throw new Exception("None authors given");
        }

        this.book.Authors.AddRange(authors);
    }

    public void RemoveAuthors(List<int> authorIds)
    {
        if (!authorIds.Any())
        {
            throw new Exception("None authors given");
        }
        
        this.book.Authors.RemoveAll(x => authorIds.Contains(x.Id));
    }

    public async Task<Result> Save()
    {
        return await this.bookPersistence.Save(this.book);
    }
}