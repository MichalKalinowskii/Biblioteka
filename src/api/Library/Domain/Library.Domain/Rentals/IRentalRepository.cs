namespace Library.Domain.Rentals;

public interface IRentalRepository
{
    public Task<Rental?> GetActiveRentalByLibraryCardIdAsync(Guid libraryCardId, CancellationToken cancellationToken);
    
    public Task AddAsync(Rental rental, CancellationToken cancellationToken);
    
    public Task<List<Rental>> GetActiveRentalsAsync(Guid libraryCardId,CancellationToken cancellationToken);
}