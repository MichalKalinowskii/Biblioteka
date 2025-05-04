namespace Library.Domain.Rentals;

public interface IRentalRepository
{
    public Task<Rental?> GetActiveRentalByLibraryCardIdAsync(Guid libraryCardId, CancellationToken cancellationToken);
    
    public Task AddAsync(Rental rental, CancellationToken cancellationToken);
    
    public Task<List<Rental>> GetAsync(Guid libraryCardId,CancellationToken cancellationToken);
}