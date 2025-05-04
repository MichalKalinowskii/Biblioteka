namespace Library.Domain.Clients;

public interface IClientRepository
{
    public Task<bool> LibraryCardExistsAsync(Guid libraryCardId, CancellationToken cancellationToken);
    
    public Task<Client?> GetByIdAsync(Guid userId, CancellationToken cancellationToken);

    public Task AddAsync(Client client, CancellationToken cancellationToken);
}