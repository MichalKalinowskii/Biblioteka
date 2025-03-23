using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Books.Entites
{
    public class BookEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitlePageImageUrl { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; } 
        public List<AuthorEntity> Authors { get; set; }
    }
}
