using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Books.Models
{
    public class Genre
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Genre(string name)
        {
            Name = name;
        }
    }
}
