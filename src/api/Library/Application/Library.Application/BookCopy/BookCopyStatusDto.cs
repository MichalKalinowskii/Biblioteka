using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.BookCopy
{
    public record BookCopyStatusDto(Guid bookId, string bookCopyStatusName);
}
