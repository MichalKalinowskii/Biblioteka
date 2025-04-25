using Library.Domain.Books.Errors;
using Library.Domain.Books.Interfaces;
using Library.Domain.Books.Models;
using Library.Domain.SeedWork;

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

            return Result<Book>.Success(book.Value);
        }

        public async Task<Result<List<Book>>> GetBooksByTitle(string title, CancellationToken cancellationToken)
        {
            var books = await bookPersistence.GetBooksByTitle(title, cancellationToken);

            if (books.IsFailure) 
            {
                return Result<List<Book>>.Failure(books.Error);
            }

            return Result<List<Book>>.Success(books.Value);
        }

        public async Task<Result<List<Book>>> GetAllBooksByGenreId(string genreName, CancellationToken cancellationToken)
        {
            var genre = Genre.FromName(genreName);

            if (genre is null)
            {
                return Result<List<Book>>.Failure(BookErrors.InvalidaGenre);
            }

            var books = await bookPersistence.GetAllBooksByGenreId(genre.Id, cancellationToken);
            if (books.IsFailure)
            {
                return Result<List<Book>>.Failure(books.Error);
            }

            return Result<List<Book>>.Success(books.Value);
        }
    }
}
