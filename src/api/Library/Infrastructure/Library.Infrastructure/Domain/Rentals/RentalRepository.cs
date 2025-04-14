using Library.Domain.Rentals;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Domain.Rentals;

public class RentalRepository : IRentalRepository
{
    private readonly DbSet<Rental> _rentals;
    
    public RentalRepository(LibraryContext libraryContext)
    {
        _rentals = libraryContext.Set<Rental>();
    }
    
    public async Task AddAsync(Rental rental, CancellationToken cancellationToken)
    {
        await _rentals.AddAsync(rental, cancellationToken);
    }
}