using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Models;
using Server.DAL;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly DataContext _context;

        public BookController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("allbooks")]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            return Ok(await _context.books.ToListAsync());
        }

        [HttpGet("{ID}")]
        public async Task<ActionResult<Book>> GetBookByID(string ID)
        {
            var book = await _context.books.FindAsync(ID);
            if(book == null)
                return BadRequest("Book not find ");
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<List<Book>>> AddBook(Book book)
        {
            _context.books.Add(book);
            await _context.SaveChangesAsync();
            return Ok(await _context.books.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Book>>> UpdateBook(Book request)
        {
            var dbbook = await _context.books.FindAsync(request.ID);
            if(dbbook == null)
                return BadRequest("Book not find ");

            dbbook.Title = request.Title;
            dbbook.Publisher = request.Publisher;
            dbbook.DateOfPublication = request.DateOfPublication;
            dbbook.IsBorrowed = request.IsBorrowed;
            await _context.SaveChangesAsync();
            return Ok(await _context.books.ToListAsync());
        }

        [HttpDelete("{ID}")]
        public async Task<ActionResult<Book>> DeleteBookByID(string ID)
        {
            var dbbook = await _context.books.FindAsync(ID);
            if(dbbook == null)
                return BadRequest("Book not find ");
            _context.books.Remove(dbbook);
            await _context.SaveChangesAsync();
            return Ok(await _context.books.ToListAsync());
        }

    }
}