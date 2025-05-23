﻿using Library.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Books.Errors
{
    public static class BookErrors
    {
        public static Error PublisherNameMissing => new("Book.PublisherNameMissing", "Publisher name is missing");
        public static Error TitleMissing => new("Book.TitleIsMissing", "Title name is missing");
        public static Error ImageUrlMissing => new("Book.ImageUrlMissing", "Title page image URL is missing");
        public static Error ReleaseDateMissing => new("Book.ReleaseDateMissing", "Release date is missing");
        public static Error ISBNMissing => new("Book.ISBNMissing", "ISBN is missing");
        public static Error IncorrectISBNGiven => new("Book.IncorrectISBNGiven", "Incorrect ISBN given, it must be 10 or 13 characters long");
        public static Error GenreMissing => new("Book.GenreMissing", "Book genre is missing");
        public static Error BookAlreadyExists => new("Book.BookAlreadyExists", "Book already exists");
        public static Error BookNotFound => new("Book.BookNotFound", "Book not found");
        public static Error InvalidaGenre => new("Book.InvalidGenre", "Given genre name was invalid");
    }
}
