using Library.Domain.Books;
using Library.Domain.Books.Entites;
using Library.Domain.Books.Interfaces;
using Library.Domain.SeedWork;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.XUnitTests.Books
{
    public class BookTests
    {
        private readonly Mock<IBookPersistence> _bookPersistenceMock;
        private readonly BookEntity _validBookEntity;

        public BookTests()
        {
            _bookPersistenceMock = new Mock<IBookPersistence>();
            _validBookEntity = new BookEntity
            {
                Id = 1,
                Title = "Test Title",
                TitlePageImageUrl = "http://example.com/image.jpg",
                GenreId = 1,
                Genre = new Genre { Id = 1, Name = "Fiction" },
                ReleaseDate = DateTime.Now,
                ISBN = "1234567890",
                Publisher = "Test Publisher",
                Authors = new List<AuthorEntity>
                {
                    new AuthorEntity { Id = 1, FirstName = "John", LastName = "Doe", BirthDate = new DateTime(1980, 1, 1) }
                }
            };
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenBookEntityIsNull()
        {
            Assert.Throws<DomainException>(() => new Book(null, _bookPersistenceMock.Object));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenBookPersistenceIsNull()
        {
            Assert.Throws<DomainException>(() => new Book(_validBookEntity, null));
        }

        [Fact]
        public void ChangeGenre_ShouldThrowException_WhenGenreIsNull()
        {
            var book = new Book(_validBookEntity, _bookPersistenceMock.Object);
            Assert.Throws<DomainException>(() => book.ChangeGenre(null));
        }

        [Fact]
        public void ChangeGenre_ShouldThrowException_WhenGenreNameIsEmpty()
        {
            var book = new Book(_validBookEntity, _bookPersistenceMock.Object);
            var genre = new Genre { Id = 2, Name = "" };
            Assert.Throws<DomainException>(() => book.ChangeGenre(genre));
        }

        [Fact]
        public void ChangeGenre_ShouldChangeGenre_WhenValidGenreIsPassed()
        {
            var book = new Book(_validBookEntity, _bookPersistenceMock.Object);
            var genre = new Genre { Id = 2, Name = "Non-Fiction" };
            book.ChangeGenre(genre);
            Assert.Equal(genre.Id, _validBookEntity.GenreId);
            Assert.Equal(genre, _validBookEntity.Genre);
        }

        [Fact]
        public void AddAuthors_ShouldThrowException_WhenAuthorsListIsEmpty()
        {
            var book = new Book(_validBookEntity, _bookPersistenceMock.Object);
            Assert.Throws<DomainException>(() => book.AddAuthors(new List<AuthorEntity>()));
        }

        [Fact]
        public void AddAuthors_ShouldAddAuthors_WhenValidAuthorsArePassed()
        {
            var book = new Book(_validBookEntity, _bookPersistenceMock.Object);
            var authors = new List<AuthorEntity>
            {
                new AuthorEntity { Id = 2, FirstName = "Jane", LastName = "Doe", BirthDate = new DateTime(1990, 1, 1) }
            };
            book.AddAuthors(authors);
            Assert.Contains(authors[0], _validBookEntity.Authors);
        }

        [Fact]
        public void RemoveAuthors_ShouldThrowException_WhenAuthorIdsListIsEmpty()
        {
            var book = new Book(_validBookEntity, _bookPersistenceMock.Object);
            Assert.Throws<DomainException>(() => book.RemoveAuthors(new List<int>()));
        }

        [Fact]
        public void RemoveAuthors_ShouldRemoveAuthors_WhenValidAuthorIdsArePassed()
        {
            var book = new Book(_validBookEntity, _bookPersistenceMock.Object);
            var authorIds = new List<int> { 1 };
            book.RemoveAuthors(authorIds);
            Assert.DoesNotContain(_validBookEntity.Authors, a => authorIds.Contains(a.Id));
        }

        [Fact]
        public async Task Save_ShouldReturnSuccess_WhenBookIsValid()
        {
            _bookPersistenceMock.Setup(x => x.Save(It.IsAny<BookEntity>())).ReturnsAsync(Result.Success());
            var book = new Book(_validBookEntity, _bookPersistenceMock.Object);
            var result = await book.Save();
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Save_ShouldReturnFailure_WhenBookIsInvalid()
        {
            _bookPersistenceMock.Setup(x => x.Save(It.IsAny<BookEntity>())).ReturnsAsync(Result.Failure(new Domain.SeedWork.Error("Error", "Save failed")));
            var book = new Book(_validBookEntity, _bookPersistenceMock.Object);
            var result = await book.Save();
            Assert.False(result.IsSuccess);
        }
    }
}
