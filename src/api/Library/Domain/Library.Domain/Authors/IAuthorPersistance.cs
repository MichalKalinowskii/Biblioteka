using Library.Domain.Authors.Models;
using Library.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Authors
{
    public interface IAuthorPersistance
    {
        Task<Result<Author>> AddNewAuthor(Author author, CancellationToken cancellationToken);
    }
}
