using Library.Domain.Authors;
using Library.Domain.Authors.Models;
using Library.Domain.SeedWork;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Domain.Authors
{
    internal class AuthorRepository : IAuthorPersistance
    {
        private DbSet<Author> authors;

        public AuthorRepository(LibraryContext context)
        {
            authors = context.Set<Author>();
        }

        public async Task<Result<Author>> AddNewAuthor(Author author, CancellationToken cancellationToken)
        {
            Result<Author> result;

            try
            {
                await authors.AddAsync(author, cancellationToken);
                result = Result<Author>.Success(author);
            }
            catch (Exception ex) 
            {
                result = Result<Author>.Failure(new Error("AuthorRepository.AddNewAuthor", ex.Message));
            }

            return result;
        }
    }
}
