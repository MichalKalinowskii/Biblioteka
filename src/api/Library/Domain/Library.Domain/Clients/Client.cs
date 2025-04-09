namespace Library.Domain.Clients;

public class Client
{
    public int Id { get; private set; }
    
    public int UserId { get; private set; }
    
    public Guid LibraryCardId { get; private set; }

    public Client(int userId, Guid libraryCardId)
    {
        UserId = userId;
        LibraryCardId = libraryCardId;
    }
}