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
    public class AuthorService
    {
        private readonly IAuthorPersistance authorPersistance;
        private readonly IUnitOfWork unitOfWork;

        public AuthorService(IAuthorPersistance authorPersistance, IUnitOfWork unitOfWork)
        {
            this.authorPersistance = authorPersistance;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Author>> AddNewAuthor(string name,
            string lastName,
            DateTime dateOfBirth,
            DateTime dateOfDeath,
            CancellationToken cancellationToken,
            string description)
        {
            var authorResult = AuthorFactory.Create(name, lastName, dateOfBirth, dateOfDeath, description);

            if (authorResult.IsFailure)
            {
                return authorResult;
            }

            var persistanceResult = await authorPersistance.AddNewAuthor(authorResult.Value, cancellationToken);

            if (persistanceResult.IsFailure)
            {
                return persistanceResult!;
            }

            await unitOfWork.CommitAsync(cancellationToken);

            return Result<Author>.Success(authorResult.Value!);
        }

        public async Task<Result<List<Author>>> GetAllAuthorsAsync(CancellationToken cancellationToken)
        {
            var authors = await authorPersistance.GetAllAuthorsAsync(cancellationToken);
            if (authors.IsFailure)
            {
                return authors;
            }
            return Result<List<Author>>.Success(authors.Value!);
        }

        public async Task<Result<List<Author>>> GetAuthorBookAsync(Guid bookId, CancellationToken cancellationToken)
        {
            var authors = await authorPersistance.GetAuthorByBookIdAsync(bookId, cancellationToken);

            if (authors.IsFailure)
            {
                return Result<List<Author>>.Failure(new Error("", ""));
            }

            return Result<List<Author>>.Success(authors.Value!);
        }
    }
}
