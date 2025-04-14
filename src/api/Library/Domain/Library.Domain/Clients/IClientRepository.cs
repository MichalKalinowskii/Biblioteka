namespace Library.Domain.Clients;

public interface IClientRepository
{
    public Task<bool> LibraryCardExistsAsync(Guid libraryCardId);
}