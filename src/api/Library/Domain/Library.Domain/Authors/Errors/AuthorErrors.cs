using Library.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Authors.Errors
{
    public class AuthorErrors
    {
        public static readonly Error AuthorNameMissing = 
            new Error("AuthorErros.AuthorNameMissing", "Author name is missing");
        public static readonly Error AuthorLastNameMissing = 
            new Error("AuthorErros.AuthorLastNameMissing", "Author last name is missing");
        public static readonly Error AuthorBirthDateIsMissing =
            new Error("AuthorErrors.AuthorBirthDateIsMissing", "Author must have date of his birth");
    }
}
