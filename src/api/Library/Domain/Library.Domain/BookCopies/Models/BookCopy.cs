using Library.Domain.BookCopies.Errors;
using Library.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.BookCopies.Models
{
    public class BookCopy
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public Guid LocationId { get; set; }
        public BookCopyStatus Status { get; set; }

        public BookCopy(Guid id, 
            Guid bookId, 
            Guid locationId, 
            BookCopyStatus status)
        {
            Id = id;
            BookId = bookId;
            LocationId = locationId;
            Status = status;
        }

        public void ChangeStatus(BookCopyStatus newStatus)
        {
            Status = newStatus;
        }

        public Result ChangeLocation(Guid locationId)
        {
            if (locationId == Guid.Empty)
            {
                return Result.Failure(BookCopyErrors.UncorrectLocationIdGiven);
            }

            LocationId = locationId;

            return Result.Success();
        }
    }
}
