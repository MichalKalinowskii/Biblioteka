using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Authors.Models
{
    public class AuthorBook
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public Guid BookId { get; set; }
    }
}
