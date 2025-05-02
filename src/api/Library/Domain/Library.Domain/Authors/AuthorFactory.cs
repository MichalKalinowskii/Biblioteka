using Library.Domain.Authors.Errors;
using Library.Domain.Authors.Models;
using Library.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Authors
{
    public class AuthorFactory
    {
        private AuthorFactory()
        {
            
        }

        public static Result<Author> Create(string name, string lastName, DateTime dateOfBirth, DateTime dateOfDeath)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result<Author>.Failure(AuthorErrors.AuthorNameMissing);
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                return Result<Author>.Failure(AuthorErrors.AuthorLastNameMissing);
            }

            if (dateOfBirth == default)
            {
                return Result<Author>.Failure(AuthorErrors.AuthorBirthDateIsMissing);
            }

            var author = new Author(name, lastName, dateOfBirth, dateOfDeath);

            return Result<Author>.Success(author);
        }
    }
}
