using Library.Domain.BookCopies.Models;

namespace Library.Application.BookCopy
{
    public record BookCopyAddNewDto(Guid BookId,
        Guid LocationId,
        string statusName,
        CancellationToken cancellationToken);
}
