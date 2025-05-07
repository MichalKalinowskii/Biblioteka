using Library.Domain.SeedWork;
using Library.Domain.Clients;
using Library.Domain.BookCopies.Interfaces;
using Library.Domain.BookCopies.Models;

namespace Library.Domain.Rentals;

public class RentalService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRentalRepository _rentalRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IBookCopyPersistance _bookCopyPersistance;

    public RentalService(IUnitOfWork unitOfWork, IRentalRepository rentalRepository, IClientRepository clientRepository, IBookCopyPersistance bookCopyPersistance)
    {
        _unitOfWork = unitOfWork;
        _rentalRepository = rentalRepository;
        _clientRepository = clientRepository;
        _bookCopyPersistance = bookCopyPersistance;
    }
    
    public async Task<Result<Rental>> CreateRentalAsync(Guid libraryCardId, Guid employeeId, List<Guid> bookCopyIds, DateTime returnDate, CancellationToken cancellationToken)
    {
        if (!await _clientRepository.LibraryCardExistsAsync(libraryCardId, cancellationToken))
        {
            return Result<Rental>.Failure(RentalErrors.InvalidLibraryCard());
        }

        var unavailableBookCopyIds = await _bookCopyPersistance.UnavailableBookCopyIds(bookCopyIds, cancellationToken);

        if (unavailableBookCopyIds.Any())
        {
            return Result<Rental>.Failure(new Error("UnavailableBookCopyIds", $"Those book are unavailable {string.Join(" ,", unavailableBookCopyIds)}"));
        }
        
        Result<Rental> result = Rental.Create(libraryCardId, employeeId, bookCopyIds, returnDate);

        if (result.IsFailure)
        {
            return Result<Rental>.Failure(result.Error);
        }
        
        await _bookCopyPersistance.SetBookCopiesStatus(bookCopyIds, BookCopyStatus.Unavailable, cancellationToken);
        await _rentalRepository.AddAsync(result.Value!, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return result;
    }

    public async Task<Result> ReturnBookAsync(Guid libraryCardId, List<Guid> bookCopyIds, CancellationToken cancellationToken)
    {
        Rental? rental = await _rentalRepository.GetActiveRentalByLibraryCardIdAsync(libraryCardId, cancellationToken);

        if (rental is null)
        {
            return Result.Failure(RentalErrors.ThereWereNoActiveRentalsForLibraryCard());
        }
        
        Result result = rental.ReturnBooks(bookCopyIds);

        if (result.IsFailure)
        {
            return result;
        }

        await _bookCopyPersistance.SetBookCopiesStatus(bookCopyIds, BookCopyStatus.Available, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return result;
    }
}