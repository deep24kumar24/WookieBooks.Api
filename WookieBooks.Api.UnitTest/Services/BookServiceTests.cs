using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WookieBooks.Api.DBModels;
using WookieBooks.Api.Models;
using WookieBooks.Api.Repositories;
using WookieBooks.Api.Services;

namespace WookieBooks.Api.UnitTest.Services
{
    [TestClass]
    public class BookServiceTests
    {
        //Test Initialize
        #region Setup

        private Mock<IBookRepository> _bookRepository;
        private BookService _bookService;
        private Mock<IMapper> _mapper;

        private BookDTO bookdto;
        private Book book;

        [TestInitialize]
        public void Initialize()
        {
            _bookRepository = new Mock<IBookRepository>();
            _mapper = new Mock<IMapper>();

            _bookService = new BookService(_bookRepository.Object, _mapper.Object);

            bookdto = new BookDTO
            {
                Id = 1,
                Title = "Test Title 1",
                Description = "Test Description 1",
                Author = "Test Author 1",
                CoverImage = null,
                Price = 5.5m
            };

            book = new Book
            {
                Id = 1,
                Title = "Test Title 1",
                Description = "Test Description 1",
                Author = "Test Author 1",
                CoverImage = null,
                Price = 5.5m
            };
        }

        #endregion

        #region GetBook

        [TestMethod]
        public async Task GetBook_GivenId_ExpectBookObject()
        {
            _bookRepository.Setup(br => br.GetBook(It.IsAny<long>())).ReturnsAsync(bookdto);
            _mapper.Setup(m => m.Map<Book>(It.IsAny<BookDTO>())).Returns(book);

            var result = await _bookService.GetBook(1);

            Assert.IsInstanceOfType(result, typeof(Book));
            Assert.IsNotNull(result);
        }

        #endregion

        #region GetAllBooks

        [TestMethod]
        public async Task GetAllBooks_ExpectListOfBooksObject()
        {
            _bookRepository.Setup(br => br.GetAllBooks()).ReturnsAsync(new List<BookDTO> { bookdto });

            _mapper.Setup(m => m.Map<List<Book>>(It.IsAny<List<BookDTO>>())).Returns(new List<Book>
            {
                book
            });

            var result = await _bookService.GetAllBooks();

            Assert.IsInstanceOfType(result, typeof(List<Book>));
            Assert.IsNotNull(result);
        }

        #endregion

        #region SaveBook

        [TestMethod]
        public async Task SaveBook_GivenBook_ExpectIdOfnewBookSaved()
        {
            _bookRepository.Setup(br => br.SaveBook(It.IsAny<BookDTO>())).ReturnsAsync(bookdto.Id);

            _mapper.Setup(m => m.Map<BookDTO>(It.IsAny<Book>())).Returns(bookdto);

            var result = await _bookService.SaveBook(book);

            Assert.IsInstanceOfType(result, typeof(long));
            Assert.IsNotNull(result);
        }

        #endregion

        #region UpdateBook

        [TestMethod]
        public async Task UpdateBook_GivenBook_ExpectUpdatedBookReturned()
        {

            _bookRepository.Setup(br => br.UpdateBook(It.IsAny<BookDTO>())).ReturnsAsync(bookdto);

            _mapper.Setup(m => m.Map<BookDTO>(It.IsAny<Book>())).Returns(bookdto);
            _mapper.Setup(m => m.Map<Book>(It.IsAny<BookDTO>())).Returns(book);

            var result = await _bookService.SaveBook(book);

            Assert.IsInstanceOfType(result, typeof(long));
            Assert.IsNotNull(result);
        }

        #endregion

        #region DeleteBook

        [TestMethod]
        public async Task DeleteBook_GivenBookId_ExpectSuccessTrue()
        {

            _bookRepository.Setup(br => br.DeleteBook(It.IsAny<long>())).ReturnsAsync(true);

            var result = await _bookService.DeleteBook(book.Id);

            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.AreEqual(true, result);
        }

        #endregion
    }
}
