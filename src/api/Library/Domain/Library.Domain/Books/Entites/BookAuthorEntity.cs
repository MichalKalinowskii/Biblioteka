using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Books.Entites
{
    internal class BookAuthorEntity
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int AuthorId { get; set; }
    }
}
