using Library.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.BookCopies.Errors
{
    public class BookCopyErrors
    {
        public static Error BookIdMissing => new("BookCopy.BookIdMissing", "Book id is missing");
        public static Error LocationIdMissing => new("BookCopy.LocationIdMissing", "Location id is missing");
        public static Error UncorrectLocationIdGiven => new("BookCopy.UncorrectLocationIdGiven", "Uncorrect location id given");
        public static Error BookCopyStatusMissing => new("BookCopy.BookCopyStatusMissing", "Book copy status must exist");
        public static Error BookCopyNotFound => new("BookCopy.BookCopyNotFound", "Book copy not found");
        public static Error InvalidBookCopyStatusName => new("BookCopy.InvalidBookCopyStatusName", "Given book copy name is invalid");
    }
}
