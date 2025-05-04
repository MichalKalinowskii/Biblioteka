using Library.Domain.SeedWork;

namespace Library.Domain.Clients;

public class Client
{
    public Guid UserId { get; private set; }
    
    public Guid LibraryCardId { get; private set; }

    internal Client(Guid userId, Guid libraryCardId)
    {
        UserId = userId;
        LibraryCardId = libraryCardId;
    }

    public static Result<Client> Create(Guid userId, Guid libraryCardId)
    {
        if (Guid.Empty == userId)
        {
            return Result<Client>.Failure(ClientErrors.InvalidUserId());
        }

        if (Guid.Empty == libraryCardId)
        {
            return Result<Client>.Failure(ClientErrors.InvalidLibraryCardId());
        }
        
        return Result<Client>.Success(new Client(userId, libraryCardId));
    }
}