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
        ValidateBook(book, bookPersistence);

        this.book = book;
        this.bookPersistence = bookPersistence;
    }

    private void ValidateBook(BookEntity book, IBookPersistence bookPersistence)
    {
        if (bookPersistence is null)
        {
            throw new DomainException("Passed IBookPersistence was null");
        }

        if (book == null)
        {
            throw new DomainException("Passed BookEntity was null");
        }

        if (string.IsNullOrWhiteSpace(book.Publisher))
        {
            throw new DomainException("No publisher given");
        }

        if (string.IsNullOrWhiteSpace(book.Title))
        {
            throw new DomainException("No book title given");
        }

        if (string.IsNullOrWhiteSpace(book.TitlePageImageUrl))
        {
            throw new DomainException("No title page image url given");
        }

        if (book.ReleaseDate == default)
        {
            throw new DomainException("No release date given");
        }

        if (string.IsNullOrWhiteSpace(book.ISBN))
        {
            throw new DomainException("No ISBN given");
        }

        if (book.ISBN.Length != 10 && book.ISBN.Length != 13)
        {
            throw new DomainException("ISBN must be 10 or 13 characters long");
        }
    }

    public void ChangeGenre(Genre genre)
    {
        if (genre == null)
        {
            throw new DomainException("Genre is required");
        }

        if (string.IsNullOrWhiteSpace(genre.Name))
        {
            throw new DomainException("Genre name is required");
        }   

        this.book.GenreId = genre.Id;
        this.book.Genre = genre;
    }

    public void AddAuthors(List<AuthorEntity> authors)
    {
        if (!authors.Any())
        {
            throw new DomainException("No authors given");
        }

        this.book.Authors.AddRange(authors);
    }

    public void RemoveAuthors(List<int> authorIds)
    {
        if (!authorIds.Any())
        {
            throw new DomainException("No authors given");
        }
        
        this.book.Authors.RemoveAll(x => authorIds.Contains(x.Id));
    }

    public async Task<Result> Save()
    {
        ValidateGenre();
        ValidateAuthors();

        return await this.bookPersistence.Save(this.book);
    }

    private void ValidateGenre()
    {
        if (this.book.Genre is null)
        {
            throw new DomainException("Genre is required");
        }
        if (string.IsNullOrWhiteSpace(this.book.Genre.Name))
        {
            throw new DomainException("Genre name is required");
        }
    }

    private void ValidateAuthors()
    {
        if (!this.book.Authors.Any())
        {
            throw new DomainException("No authors given");
        }

        if (this.book.Authors.Any(x => string.IsNullOrWhiteSpace(x.FirstName)))
        {
            throw new DomainException("Some authors first name was not given");
        }

        if (this.book.Authors.Any(x => string.IsNullOrWhiteSpace(x.LastName)))
        {
            throw new DomainException("Some authors last name was not given");
        }

        if (this.book.Authors.Any(x => x.BirthDate == default))
        {
            throw new DomainException("Some authors birth date was not given");
        }
    }
}