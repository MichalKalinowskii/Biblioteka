using Library.Infrastructure.Data;
using Library.Domain.SeedWork;

namespace Library.Infrastructure.Domain;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly LibraryContext _fleetManagerDbContext;

    public UnitOfWork(LibraryContext fleetManagerDbContext)
    {
        _fleetManagerDbContext = fleetManagerDbContext;
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        return await _fleetManagerDbContext.SaveChangesAsync(cancellationToken);
    }
}