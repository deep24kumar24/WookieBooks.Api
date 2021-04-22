using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WookieBooks.Api.DBModels;
using WookieBooks.Api.Models;
using WookieBooks.Api.Repositories;

namespace WookieBooks.Api.Services
{
    public class BookService : IBookService
    {

        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<Book> GetBook(long id)
        {
            var book = await _bookRepository.GetBook(id);
            return _mapper.Map<Book>(book);
        }

        public async Task<List<Book>> GetAllBooks()
        {
            var book = await _bookRepository.GetAllBooks();
            return _mapper.Map<List<Book>>(book);
        }

        public async Task<long> SaveBook(Book book)
        {
            return await _bookRepository.SaveBook(_mapper.Map<BookDTO>(book));
        }

        public async Task<Book> UpdateBook(Book book)
        { 
            var bookDTO = await _bookRepository.UpdateBook(_mapper.Map<BookDTO>(book));
            return _mapper.Map<Book>(bookDTO);
        }

        public async Task<bool> DeleteBook(long id)
        {
            return await _bookRepository.DeleteBook(id);
        }
    }
}
