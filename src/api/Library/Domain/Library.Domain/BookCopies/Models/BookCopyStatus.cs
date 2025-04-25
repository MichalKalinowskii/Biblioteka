using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.BookCopies.Models
{
    public class BookCopyStatus
    {
        public static readonly BookCopyStatus Available = new BookCopyStatus(1, "Available");
        public static readonly BookCopyStatus Reserved = new BookCopyStatus(2, "Reserved");
        public static readonly BookCopyStatus Lost = new BookCopyStatus(3, "Lost");
        public static readonly BookCopyStatus Damaged = new BookCopyStatus(4, "Damaged");
        public static readonly BookCopyStatus Unavailable = new BookCopyStatus(5, "Unavailable");

        public int Id { get; set; }
        public string Name { get; set; }

        private BookCopyStatus(int Id, string name)
        {
            this.Id = Id;
            this.Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        public static BookCopyStatus FromValue(int value)
        {
            return value switch
            {
                1 => Available,
                2 => Reserved,
                3 => Lost,
                4 => Damaged,
                5 => Unavailable,
                _ => default!
            };
        }

        public static BookCopyStatus FromName(string name)
        {
            var statusName = (name ?? string.Empty).Trim().ToLower();

            return statusName switch 
            {
                "available" => Available,
                "reserved" => Reserved,
                "lost" => Lost,
                "damaged" => Damaged,
                "unavailable" => Unavailable,
                _ => default!
            };
        }
    }
}
