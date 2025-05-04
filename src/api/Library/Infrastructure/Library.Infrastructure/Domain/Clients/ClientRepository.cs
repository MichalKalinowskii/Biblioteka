using Library.Domain.Clients;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Domain.Clients;

public class ClientRepository : IClientRepository
{
    private readonly DbSet<Client> _clients;
    
    public ClientRepository(LibraryContext context)
    {
        _clients = context.Set<Client>();
    }
    
    public async Task<bool> LibraryCardExistsAsync(Guid libraryCardId, CancellationToken cancellationToken)
    {
        return await _clients.AnyAsync(x => x.LibraryCardId == libraryCardId, cancellationToken);
    }

    public async Task<Client?> GetByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _clients.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
    }

    public async Task AddAsync(Client client, CancellationToken cancellationToken)
    {
        await _clients.AddAsync(client, cancellationToken);
    }
}