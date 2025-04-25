using Library.Domain.Books.Errors;
using Library.Domain.Books.Interfaces;
using Library.Domain.Books.Models;
using Library.Domain.SeedWork;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Domain.Books;

public class BookRepository : IBookPersistence
{
    private readonly DbSet<Book> bookContext;

    public BookRepository(LibraryContext context)
    {
        bookContext = context.Set<Book>();
    }

    public async Task<Result> AddBookAsync(Book book, CancellationToken cancellationToken)
    {
        var result = Result.Success();
        try
        {
           await bookContext.AddAsync(book, cancellationToken);
        }
        catch (Exception ex)
        {
            result =  Result.Failure(new Error("BookRepository.AddBookAsync", ex.Message));
        }
        return result;
    }

    public async Task<Result<List<Book>>> GetAllBooksByGenreId(int genreId, CancellationToken cancellationToken)
    {
        Result<List<Book>> result = default;
        try
        {
            var books = await bookContext.Where(x => x.Genre.Id == genreId).ToListAsync(cancellationToken);
            result = Result<List<Book>>.Success(books);
        }
        catch (Exception ex)
        {
            result = Result<List<Book>>.Failure(new Error("BookRepository.GetAllBooksByGenreId", ex.Message));
        }
        return result!;
    }

    public async Task<Result<Book>> GetBookByISBN(string ISBN, CancellationToken cancellationToken)
    {
        Result<Book> result = default;

        try
        {
            var book = await bookContext.FirstOrDefaultAsync(b => b.ISBN == ISBN, cancellationToken);
            result = Result<Book>.Success(book);
        }
        catch (Exception ex)
        {
            result = Result<Book>.Failure(new Error("BookRepository.GetBookByISBN", ex.Message));
        }

        return result!;
    }

    public async Task<Result<List<Book>>> GetBooksByTitle(string title, CancellationToken cancellationToken)
    {
        Result<List<Book>> result = default;

        try
        {
            var books = await bookContext.Where(x => x.Title.Contains(title)).ToListAsync(cancellationToken);
            result = Result<List<Book>>.Success(books);
        }
        catch (Exception ex) 
        {
            result = Result<List<Book>>.Failure(new Error("BookRepository.GetBooksByTitle", ex.Message));
        }

        return result;
    }

    public async Task<Result<Book>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        Result<Book> result = default;

        try
        {
            var book = await bookContext.FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
            result = Result<Book>.Success(book);
        }
        catch (Exception ex)
        {
            result = Result<Book>.Failure(new Error("BookRepository.GetByIdAsync", ex.Message));
        }

        return result;
    }

    public Result UpdateBook(Book book, CancellationToken cancellationToken)
    {
        var result = Result.Success();

        try
        {
            bookContext.Update(book);
        }
        catch (Exception ex)
        {
            result = Result.Failure(new Error("BookRepository.UpdateBookAsync", ex.Message));
        }

        return result;
    }
}