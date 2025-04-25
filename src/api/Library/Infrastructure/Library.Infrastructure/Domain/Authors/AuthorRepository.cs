using Library.Domain.Authors;
using Library.Domain.Authors.Models;
using Library.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Domain.Authors
{
    internal class AuthorRepository : IAuthorPersistance
    {
        public Task<Result<Author>> AddNewAuthor(Author author, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
