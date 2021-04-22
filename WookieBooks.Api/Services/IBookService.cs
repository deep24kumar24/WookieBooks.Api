using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WookieBooks.Api.Models;

namespace WookieBooks.Api.Services
{
    public interface IBookService
    {
        public Task<Book> GetBook(long id);

        public Task<List<Book>> GetAllBooks();

        public Task<long> SaveBook(Book book);

        public Task<Book> UpdateBook(Book book);

        public Task<bool> DeleteBook(long id);
    }
}
