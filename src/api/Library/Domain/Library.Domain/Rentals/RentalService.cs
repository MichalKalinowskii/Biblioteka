using Library.Domain.SeedWork;
using Library.Domain.Clients;

namespace Library.Domain.Rentals;

public class RentalService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRentalRepository _rentalRepository;
    private readonly IClientRepository _clientRepository;

    public RentalService(IUnitOfWork unitOfWork, IRentalRepository rentalRepository, IClientRepository clientRepository)
    {
        _unitOfWork = unitOfWork;
        _rentalRepository = rentalRepository;
        _clientRepository = clientRepository;
    }
    
    public async Task<Result<Rental>> CreateRentalAsync(Guid libraryCardId, int employeeId, List<int> bookCopyIds, DateTime returnDate, CancellationToken cancellationToken)
    {
        if (!await _clientRepository.LibraryCardExistsAsync(libraryCardId))
        {
            return Result<Rental>.Failure(RentalErrors.InvalidLibraryCard());
        }
        
        
        Result<Rental> result = Rental.Create(libraryCardId, employeeId, bookCopyIds, returnDate);

        if (result.IsFailure || result.Value == null)
        {
            return Result<Rental>.Failure(result.Error);
        }
        
        await _rentalRepository.AddAsync(result.Value, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return result;
    }
}