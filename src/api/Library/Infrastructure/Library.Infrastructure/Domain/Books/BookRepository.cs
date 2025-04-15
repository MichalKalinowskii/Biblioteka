using Library.Domain.Books.Entites;
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