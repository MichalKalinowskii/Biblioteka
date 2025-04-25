using Library.Domain.Books.Errors;
using Library.Domain.Books.Models;
using Library.Domain.SeedWork;

namespace Library.Domain.Books
{
    public class BookFactory
    {
        private BookFactory() { }

        public static Result<Book> Create(string publisher,
            string title,
            string titlePageImageUrl,
            DateTime releaseDate,
            string description,
            string ISBN,
            Genre genre)
        {
            if (string.IsNullOrWhiteSpace(publisher))
            {
                return Result<Book>.Failure(BookErrors.PublisherNameMissing);
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                return Result<Book>.Failure(BookErrors.TitleMissing);
            }

            if (string.IsNullOrWhiteSpace(titlePageImageUrl))
            {
                return Result<Book>.Failure(BookErrors.ImageUrlMissing);
            }

            if (releaseDate == default)
            {
                return Result<Book>.Failure(BookErrors.ReleaseDateMissing);
            }

            if (string.IsNullOrWhiteSpace(ISBN))
            {
                return Result<Book>.Failure(BookErrors.ISBNMissing);
            }

            if (ISBN.Length != 10 && ISBN.Length != 13)
            {
                return Result<Book>.Failure(BookErrors.IncorrectISBNGiven);
            }

            if (genre is null)
            {
                return Result<Book>.Failure(BookErrors.GenreMissing);
            }

            return Result<Book>.Success(
                new Book(title,
                    titlePageImageUrl,
                    releaseDate,
                    description,
                    ISBN,
                    publisher,
                    genre)
            );
        }
    }
}
