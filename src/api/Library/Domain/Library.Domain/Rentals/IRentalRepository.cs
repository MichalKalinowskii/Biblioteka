namespace Library.Domain.Rentals;

public interface IRentalRepository
{
    public Task AddAsync(Rental rental, CancellationToken cancellationToken);
    
    public Task<List<Rental>> GetAsync(CancellationToken cancellationToken);
}