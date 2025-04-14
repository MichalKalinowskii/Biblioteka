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
    
    public async Task<bool> LibraryCardExistsAsync(Guid libraryCardId)
    {
        return await _clients.AnyAsync(x => x.LibraryCardId == libraryCardId);
    }
}