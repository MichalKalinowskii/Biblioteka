namespace Library.Domain.SeedWork
{
    public interface IAuthenticatedUserService
    {
        Guid? UserId { get; }
        string? Role { get; }
        Guid? LibraryCardId { get; }
        string? GetClaim(string claimType);
    }
}
