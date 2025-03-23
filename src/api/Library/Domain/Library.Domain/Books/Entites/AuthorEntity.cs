using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Books.Entites
{
    public class AuthorEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }
    }
}
