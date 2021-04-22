using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WookieBooks.Api.DBContext;
using WookieBooks.Api.DBModels;

namespace WookieBooks.Api.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly DataBaseContext _context;

        public BookRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<BookDTO> GetBook(long id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            return book;
        }

        public async Task<List<BookDTO>> GetAllBooks()
        {
            var books = await _context.Books.ToListAsync();
            return books;
        }

        public async Task<long> SaveBook(BookDTO book)
        {
            _context.Books.Add(book);

            await _context.SaveChangesAsync();
            return book.Id;
        }

        public async Task<BookDTO> UpdateBook(BookDTO book)
        {
            var bookFound = await _context.Books.FirstOrDefaultAsync(b => b.Id == book.Id);

            if (bookFound != null)
            {
                bookFound.Title = book.Title;
                bookFound.Description = book.Description;
                bookFound.CoverImage = book.CoverImage;
                bookFound.Author = book.Author;
                bookFound.Price = book.Price;

                await _context.SaveChangesAsync();
            }

            return bookFound;
        }

        public async Task<bool> DeleteBook(long id)
        {
            var bookFound = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            
            if (bookFound != null)
            {
                _context.Remove(bookFound);

                await _context.SaveChangesAsync();
                return true;
            }

            return false;
           
        }

    }
}
