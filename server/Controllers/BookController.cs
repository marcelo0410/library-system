using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Models;
using Server.DAL;

namespace server.Controllers
{
    /// <summary>
    /// Controller for Book entity
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly DataContext _context;

        public BookController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get method for retrieving all the books data
        /// </summary>
        /// <returns>List of books</returns>
        [HttpGet]
        [Route("allbooks")]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            return Ok(await _context.books.ToListAsync());
        }

        /// <summary>
        /// Get method for getting one single book by ID
        /// </summary>
        /// <param name="Id">Request id to be used to search book data </param>
        /// <returns>A single book with ok response or badrequest while book is not found</returns>
        [HttpGet("{Id}")]
        public async Task<ActionResult<Book>> GetBookByID(string Id)
        {
            var book = await _context.books.FindAsync(Id);
            if(book == null)
                return BadRequest("Book not find ");
            return Ok(book);
        }

        /// <summary>
        /// Post method for creating Book data
        /// </summary>
        /// <param name="book">Book data received from request</param>
        /// <returns>List of updated books</returns>
        [HttpPost]
        public async Task<ActionResult<List<Book>>> AddBook(Book book)
        {
            _context.books.Add(book);
            await _context.SaveChangesAsync();
            return Ok(await _context.books.ToListAsync());
        }

        /// <summary>
        /// Put method for updating the book status(isBorrowed)
        /// </summary>
        /// <param name="request">Book data received from request</param>
        /// <returns>A list of updated books with ok response or badrequest while book is not found</returns>
        [HttpPut("status")]
        public async Task<ActionResult<List<Book>>> UpdateBook(Book request)
        {
            var dbbook = await _context.books.FindAsync(request.Id);
            if(dbbook == null)
                return BadRequest("Book not find ");

            dbbook.Title = request.Title;
            dbbook.Publisher = request.Publisher;
            dbbook.DateOfPublication = request.DateOfPublication;
            dbbook.IsBorrowed = !request.IsBorrowed;
            await _context.SaveChangesAsync();
            return Ok(await _context.books.ToListAsync());
        }

        /// <summary>
        /// Delete method for deletion of a targeted book
        /// </summary>
        /// <param name="Id">A string uuid received from request</param>
        /// <returns>A list of updated books with ok response or badrequest while book is not found</returns>
        [HttpDelete("{Id}")]
        public async Task<ActionResult<Book>> DeleteBookByID(string Id)
        {
            var dbbook = await _context.books.FindAsync(Id);
            if(dbbook == null)
                return BadRequest("Book not find ");
            _context.books.Remove(dbbook);
            await _context.SaveChangesAsync();
            return Ok(await _context.books.ToListAsync());
        }

    }
}