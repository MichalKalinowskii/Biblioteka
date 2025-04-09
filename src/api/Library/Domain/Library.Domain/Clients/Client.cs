namespace Library.Domain.Clients;

public class Client
{
    public int Id { get; private set; }
    
    public Guid UserId { get; private set; }
    
    public Guid LibraryCardId { get; private set; }

    public Client(Guid userId, Guid libraryCardId)
    {
        UserId = userId;
        LibraryCardId = libraryCardId;
    }
}