using Library.Domain.Books.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Books.Entites
{
    public class BookEntity
    {
        public string Title { get; set; }
        public string TitlePageImageUrl { get; set; }
        public Genre Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public string ISBN { get; set; }
        public string Publisher { get; set; } 
    }
}
