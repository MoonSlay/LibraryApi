using LibraryApi.Database;
using LibraryApi.Database.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibraryApiDbContext _context;

        public BooksController(LibraryApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("get-book")]
        public async Task<ActionResult<IEnumerable<BookModel>>> GetBookList()
        {
            try
            {
                return Ok(await _context.Books.ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while retrieving the book list.", details = ex.Message });
            }
        }

        [HttpPost]
        [Route("add-book")]
        public async Task<ActionResult<BookModel>> AddBook([FromBody] BookModel book)
        {
            if (book == null)
            {
                return BadRequest(new { error = "Book object is null." });
            }

            try
            {
                book.DateCreated = DateTime.Now;
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetBookList), new { id = book.Id }, book);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while adding the book.", details = ex.Message });
            }
        }

        [HttpPost]
        [Route("add-books")]
        public async Task<ActionResult<IEnumerable<BookModel>>> AddBooks([FromBody] List<BookModel> books)
        {
            if (books == null || !books.Any())
            {
                return BadRequest(new { error = "Book list is null or empty." });
            }

            try
            {
                foreach (var book in books)
                {
                    book.DateCreated = DateTime.Now;
                    _context.Books.Add(book);
                }
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetBookList), books);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while adding the books.", details = ex.Message });
            }
        }
       
    }
}
