using Library.Domain.Books.Entites;
using Library.Domain.Books.Errors;
using Library.Domain.Books.Interfaces;
using Library.Domain.Books.Models;
using Library.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Books
{
    public class BookFactory
    {
        private BookFactory() { }

        public static Result<Book> Create(BookEntity book)
        {
            if (string.IsNullOrWhiteSpace(book.Publisher))
            {
                return Result<Book>.Failure(BookErrors.PublisherNameMissing);
            }

            if (string.IsNullOrWhiteSpace(book.Title))
            {
                return Result<Book>.Failure(BookErrors.TitleMissing);
            }

            if (string.IsNullOrWhiteSpace(book.TitlePageImageUrl))
            {
                return Result<Book>.Failure(BookErrors.ImageUrlMissing);
            }

            if (book.ReleaseDate == default)
            {
                return Result<Book>.Failure(BookErrors.ReleaseDateMissing);
            }

            if (string.IsNullOrWhiteSpace(book.ISBN))
            {
                return Result<Book>.Failure(BookErrors.ISBNMissing);
            }

            if (book.ISBN.Length != 10 && book.ISBN.Length != 13)
            {
                return Result<Book>.Failure(BookErrors.IncorrectISBNGiven);
            }

            if (book.Genre is null)
            {
                return Result<Book>.Failure(BookErrors.GenreNameMissing);
            }

            return Result<Book>.Success(
                new Book(book.Title,
                    book.TitlePageImageUrl,
                    book.ReleaseDate,
                    book.Description,
                    book.ISBN,
                    book.Publisher,
                    book.Genre)
            );
        }
    }
}
