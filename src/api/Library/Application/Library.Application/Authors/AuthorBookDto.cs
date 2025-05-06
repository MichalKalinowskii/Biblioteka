using Library.Domain.Authors.Models;
using Library.Domain.Books.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Authors
{
    public class AuthorBookDto
    {
        public Book Book { get; set; }
        public List<Author> Authors { get; set; }
    }
}
