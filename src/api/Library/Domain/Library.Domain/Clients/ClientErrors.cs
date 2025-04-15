using Library.Domain.SeedWork;

namespace Library.Domain.Clients;

public class ClientErrors
{
    public static Error InvalidUserId() => new Error("Clients.InvalidUserId", "User ID is invalid.");
    
    public static Error InvalidLibraryCardId() => new Error("Clients.InvalidLibraryCardId", "Library card ID is invalid.");
}