using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WookieBooks.Api.Controllers;
using WookieBooks.Api.Models;
using WookieBooks.Api.Services;

namespace WookieBooks.Api.UnitTest.Controllers
{
    [TestClass]
    public class BooksControllerTests
    {
        #region Setup
        private BooksController _booksController;
        private Mock<IBookService> _booksService;
        private Book book1;
        private Book book2;

        [TestInitialize]
        public void Initialize()
        {
            _booksService = new Mock<IBookService>();
            _booksController = new BooksController(_booksService.Object);
            book1 = new Book
            {
                Id = 1,
                Title = "Test Title 1",
                Description = "Test Description 1",
                Author = "Test Author 1",
                CoverImage = null,
                Price = 5.5m
            };

            book2 = new Book
            {
                Id = 2,
                Title = "Test Title 2",
                Description = "Test Description 2",
                Author = "Test Author 2",
                CoverImage = null,
                Price = 2.5m
            };
        }

        #endregion

        #region GetBook

        [TestMethod]
        public async Task GetBook_GivenInvalidId_ExpectBadRequest()
        {
            var result = await _booksController.GetBook(-1);

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }


        [TestMethod]
        public async Task GetBook_GivenValidId_ExpectOkResult()
        {
            _booksService.Setup(bs => bs.GetBook(It.IsAny<long>())).ReturnsAsync(book1);

            var result = await _booksController.GetBook(1);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        #endregion

        #region GetAllBooks

        [TestMethod]
        public async Task GetAllBooks_ExpectOkResult()
        {
            _booksService.Setup(bs => bs.GetAllBooks()).ReturnsAsync(new List<Book>
            {
                book1, book2
            });

            var result = await _booksController.GetAllBooks();

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        #endregion

        #region SaveBook

        [TestMethod]
        public async Task SaveBook_GivenInvalidId_ExpectBadRequest()
        {
            var result = await _booksController.SaveBook(new Book
            {
                Id = -1,
            });

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            Assert.AreEqual((result as BadRequestObjectResult).Value, "Id must be greated than 0.");
        }

        [TestMethod]
        public async Task SaveBook_GivenEmptyTitle_ExpectBadRequest()
        {
            var result = await _booksController.SaveBook(new Book
            {
                Id = 10,
                Title = "",
            });

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            Assert.AreEqual((result as BadRequestObjectResult).Value, "Title cannot be blank.");
        }

        [TestMethod]
        public async Task SaveBook_GivenInvalidPrice_ExpectBadRequest()
        {
            var result = await _booksController.SaveBook(new Book
            {
                Id = 11,
                Title = "Test Title",
                Price = 0.0m
            });

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            Assert.AreEqual((result as BadRequestObjectResult).Value, "Price must be greater than 0.");
        }

        [TestMethod]
        public async Task SaveBook_GivenValidObject_ExpectOkResult()
        {
            _booksService.Setup(bs => bs.SaveBook(It.IsAny<Book>())).ReturnsAsync(book1.Id);

            var result = await _booksController.SaveBook(book1);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        #endregion

        #region UpdateBook

        [TestMethod]
        public async Task UpdateBook_GivenInvalidId_ExpectBadRequest()
        {
            var result = await _booksController.UpdateBook(new Book
            {
                Id = -1,
            });

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            Assert.AreEqual((result as BadRequestObjectResult).Value, "Id needed to update the item.");
        }

        [TestMethod]
        public async Task UpdateBook_GivenEmptyTitle_ExpectBadRequest()
        {
            var result = await _booksController.SaveBook(new Book
            {
                Id = 10,
                Title = "",
            });

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            Assert.AreEqual((result as BadRequestObjectResult).Value, "Title cannot be blank.");
        }

        [TestMethod]
        public async Task UpdateBook_GivenInvalidPrice_ExpectBadRequest()
        {
            var result = await _booksController.SaveBook(new Book
            {
                Id = 11,
                Title = "Test Title",
                Price = 0.0m
            });

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            Assert.AreEqual((result as BadRequestObjectResult).Value, "Price must be greater than 0.");
        }

        [TestMethod]
        public async Task UpdateBook_GivenValidObject_ExpectOkResult()
        {
            _booksService.Setup(bs => bs.UpdateBook(It.IsAny<Book>())).ReturnsAsync(book1);

            var result = await _booksController.SaveBook(book1);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        #endregion

        #region DeleteBook

        [TestMethod]
        public async Task DeleteBook_GivenInvalidId_ExpectBadRequest()
        {
            var result = await _booksController.DeleteBook(-1);

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            Assert.AreEqual((result as BadRequestObjectResult).Value, "Id must be greater than 0.");
        }

        [TestMethod]
        public async Task DeleteBook_GivenValidObject_ExpectOkResult()
        {
            _booksService.Setup(bs => bs.DeleteBook(It.IsAny<long>())).ReturnsAsync(true);

            var result = await _booksController.DeleteBook(book1.Id);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        #endregion
    }
}
