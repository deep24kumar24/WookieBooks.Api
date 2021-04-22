using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WookieBooks.Api.DBModels;

namespace WookieBooks.Api.Repositories
{
    public interface IBookRepository
    {
        public Task<BookDTO> GetBook(long id);

        public Task<List<BookDTO>> GetAllBooks();

        public Task<long> SaveBook(BookDTO book);

        public Task<BookDTO> UpdateBook(BookDTO book);

        public Task<bool> DeleteBook(long id);
    }
}
