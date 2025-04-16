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
    public class BookService
    {
        private readonly IBookPersistence bookPersistence;
        private readonly IUnitOfWork unitOfWork;

        public BookService(IBookPersistence bookPersistence, IUnitOfWork unitOfWork)
        {
            this.bookPersistence = bookPersistence;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> AddNewBookAsync(string publisher,
            string title,
            string titlePageImageUrl,
            DateTime releaseDate,
            string description,
            string ISBN,
            Genre genre, 
            CancellationToken cancellationToken)
        {
            var result = BookFactory.Create(publisher,
            title,
            titlePageImageUrl,
            releaseDate,
            description,
            ISBN,
            genre);

            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }

            var newBook = result.Value!;

            var bookAlreadyExist = await bookPersistence.GetBookByISBN(newBook.ISBN, cancellationToken);

            if (bookAlreadyExist.IsFailure)
            {
                return Result.Failure(bookAlreadyExist.Error);
            }

            if (bookAlreadyExist.Value is not null)
            {
                return Result.Failure(BookErrors.BookAlreadyExists);
            }

            var addBookResult = await bookPersistence.AddBookAsync(newBook, cancellationToken);

            if (addBookResult.IsFailure)
            {
                return Result.Failure(addBookResult.Error);
            }

            await unitOfWork.CommitAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result> ChangeBookGenreAsync(string ISBN, Genre newGenre, CancellationToken cancellationToken)
        {
            var bookISBN = await bookPersistence.GetBookByISBN(ISBN, cancellationToken);
            
            if (bookISBN.IsFailure)
            {
                return Result.Failure(bookISBN.Error);
            }

            if (bookISBN.Value is null)
            {
                return Result.Failure(BookErrors.BookNotFound);
            }

            var book = bookISBN.Value!;

            book.ChangeGenre(newGenre);

            var updateBookResult = bookPersistence.UpdateBook(book, cancellationToken);
            if (updateBookResult.IsFailure)
            {
                return Result.Failure(updateBookResult.Error);
            }
            
            await unitOfWork.CommitAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result<Book>> GetBookByISBN(string ISBN, CancellationToken cancellationToken)
        {
            var book = await bookPersistence.GetBookByISBN(ISBN, cancellationToken);
            if (book.IsFailure)
            {
                return Result<Book>.Failure(book.Error);
            }
            if (book.Value is null)
            {
                return Result<Book>.Failure(BookErrors.BookNotFound);
            }
            return Result<Book>.Success(book.Value);
        }

        public async Task<Result<List<Book>>> GetAllBooksByGenreId(int genreId, CancellationToken cancellationToken)
        {
            var books = await bookPersistence.GetAllBooksByGenreId(genreId, cancellationToken);
            if (books.IsFailure)
            {
                return Result<List<Book>>.Failure(books.Error);
            }
            return Result<List<Book>>.Success(books.Value);
        }
    }
}
