using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WookieBooks.Api.Models;
using WookieBooks.Api.Services;

namespace WookieBooks.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {

        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }
        
        /// <summary>
        /// Get all the books
        /// </summary>
        /// <returns>List of the books stored in the DB</returns>
        [Route("getAllBooks")]
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                List<Book> book = await _bookService.GetAllBooks();

                if (book == null)
                {
                    return NotFound();
                }

                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Gets the book by given id 
        /// </summary>
        /// <param name="id">Id to look for in the DB</param>
        /// <returns>Book Object, if found</returns>
        [Route("getBook")]
        [HttpGet]
        public async Task<IActionResult> GetBook(long id)
        {
            #region ParamCheck
            if(id <= 0)
            {
                return BadRequest("Id must be greated than 0");
            }
            #endregion

            try
            {
                Book book = await _bookService.GetBook(id);

                if (book == null)
                {
                    return NotFound();
                }

                return Ok(book);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Save a new Book Object in the DB
        /// </summary>
        /// <param name="book">Book Object with Id and Title Required</param>
        /// <returns>Id of newly created Book</returns>
        [Route("saveBook")]
        [HttpPut]
        public async Task<IActionResult> SaveBook(Book book)
        {
            #region ParamCheck
            if (book.Id <= 0)
            {
                return BadRequest("Id must be greated than 0.");
            }
            else if (string.IsNullOrEmpty(book.Title))
            {
                return BadRequest("Title cannot be blank.");
            }
            else if (book.Price <= 0.0m)
            {
                return BadRequest("Price must be greater than 0.");
            }
            #endregion

            try
            {
                long bookId = await _bookService.SaveBook(book);
                return Ok(bookId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update the details of the book
        /// </summary>
        /// <param name="book">Object with new Details and Id for which details needs to be updated</param>
        /// <returns>Updated book object</returns>
        [Route("updateBook")]
        [HttpPatch]
        public async Task<IActionResult> UpdateBook(Book book)
        {
            #region ParamCheck
            if (book.Id <= 0)
            {
                return BadRequest("Id needed to update the item.");
            }
            else if (string.IsNullOrEmpty(book.Title))
            {
                return BadRequest("Title cannot be blank.");
            }
            else if (book.Price <= 0.0m)
            {
                return BadRequest("Price must be greater than 0.");
            }
            #endregion

            try
            {
                Book updatedBook = await _bookService.UpdateBook(book);
                return Ok(updatedBook);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Deletes the book from the DB
        /// </summary>
        /// <param name="id">Id fo the book that needs to be deleted</param>
        /// <returns>Succes: true or false</returns>
        [Route("deleteBook")]
        [HttpDelete]
        public async Task<IActionResult> DeleteBook(long id)
        {
            #region ParamCheck
            if (id <= 0)
            {
                return BadRequest("Id must be greater than 0.");
            }
            #endregion

            try
            {
                bool success = await _bookService.DeleteBook(id);
                return Ok(success);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
