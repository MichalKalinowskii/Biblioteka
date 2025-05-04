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

    public async Task<Rental?> GetActiveRentalByLibraryCardIdAsync(Guid libraryCardId, CancellationToken cancellationToken)
    {
        return await _rentals
            .Include(x => x.BookRentals)
            .Where(x => x.Status != RentalStatus.Returned)
            .FirstOrDefaultAsync(x => x.LibraryCardId == libraryCardId, cancellationToken);
    }

    public async Task AddAsync(Rental rental, CancellationToken cancellationToken)
    {
        await _rentals.AddAsync(rental, cancellationToken);
    }

    public Task<List<Rental>> GetActiveRentalsAsync(Guid libraryCardId, CancellationToken cancellationToken)
    {
        var query = _rentals.Include(x => x.BookRentals).
            Where(x => x.Status != RentalStatus.Returned).AsQueryable();

        if (libraryCardId != Guid.Empty)
        {
            query = query.Where(x => x.LibraryCardId == libraryCardId);
        }
        
        return query
            .ToListAsync(cancellationToken);
    }
}