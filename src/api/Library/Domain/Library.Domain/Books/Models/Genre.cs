using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Books.Models
{
    public class Genre
    {
        public static readonly Genre Comedy = new Genre(1, "Comedy");
        public static readonly Genre Thriller = new Genre(2, "Thriller");
        public static readonly Genre Drama = new Genre(3, "Drama");
        public static readonly Genre Horror = new Genre(4, "Horror");
        public static readonly Genre Romance = new Genre(5, "Romance");
        public static readonly Genre SinceFiction = new Genre(6, "SinceFiction");

        public int Id { get; set; }
        public string Name { get; set; }

        private Genre(int Id, string name)
        {
            this.Id = Id;
            this.Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        public static Genre FromValue(int value)
        {
            return value switch
            {
                1 => Comedy,
                2 => Thriller,
                3 => Drama,
                4 => Horror,
                5 => Romance,
                6 => SinceFiction,
                _ => throw new ArgumentException("Invalid status value")
            };
        }

        public static Genre FromName(string name)
        {
            string genreName = (name ?? string.Empty).Trim().ToLower();

            return genreName switch
            {
                "comedy" => Comedy,
                "thriller" => Thriller,
                "drama" => Drama,
                "horror" => Horror,
                "romance" => Romance,
                "sincefiction" => SinceFiction,
                _ => default!
            };
        }
    }
}
