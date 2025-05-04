namespace Library.Domain.Clients;

public class ClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }
    
    public async Task AddAsync(Guid applicationUserId, CancellationToken cancellationToken)
    {
        Guid libraryCardId = Guid.NewGuid();
        
        Client client = new Client(applicationUserId, libraryCardId);
        
        await _clientRepository.AddAsync(client, cancellationToken);
    }
}