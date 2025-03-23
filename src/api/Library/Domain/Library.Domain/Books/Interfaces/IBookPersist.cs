using Library.Domain.Books.Entites;
using Library.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Books.Interfaces
{
    public interface IBookPersistence
    {
        Task<Result> Save(BookEntity book);
    }
}
