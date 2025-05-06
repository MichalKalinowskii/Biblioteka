using Library.Domain.Authors;
using Library.Domain.Authors.Models;
using Library.Domain.SeedWork;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Domain.Authors
{
    internal class AuthorRepository : IAuthorPersistance
    {
        private LibraryContext authorContext;

        public AuthorRepository(LibraryContext context)
        {
            authorContext = context;
        }

        public async Task<Result<Author>> AddNewAuthor(Author author, CancellationToken cancellationToken)
        {
            Result<Author> result;

            try
            {
                await authorContext.Authors.AddAsync(author, cancellationToken);
                result = Result<Author>.Success(author);
            }
            catch (Exception ex) 
            {
                result = Result<Author>.Failure(new Error("AuthorRepository.AddNewAuthor", ex.Message));
            }

            return result;
        }

        public async Task<Result<List<Author>>> GetAllAuthorsAsync(CancellationToken cancellationToken)
        {
            Result<List<Author>> result = default;

            try
            {
                var authors = authorContext.Authors.ToListAsync(cancellationToken);
                result = Result<List<Author>>.Success(authors.Result);
            }
            catch (Exception ex)
            {
                result = Result<List<Author>>.Failure(new Error("AuthorRepository.GetAllAuthorsAsync", ex.Message));
            }

            return result!;
        }

        public async Task<Result<List<Author>>> GetAuthorByBookIdAsync(Guid bookId, CancellationToken cancellationToken)
        {
            Result<List<Author>> result = default;

            try
            {
                var authorIds = await authorContext.AuthorBooks
                    .Where(x => x.BookId == bookId)
                    .Select(x => x.AuthorId)
                    .ToListAsync(cancellationToken);

                var authors = await authorContext.Authors.Where(x => authorIds.Contains(x.Id)).ToListAsync(cancellationToken);

                result = Result<List<Author>>.Success(authors);
            }
            catch (Exception ex)
            {
                result = Result<List<Author>>.Failure(new Error("AuthorRepository.GetAuthorByBookIdAsync", ex.Message));
            }

            return result!;
        }
    }
}
