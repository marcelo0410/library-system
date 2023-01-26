using Microsoft.AspNetCore.Mvc;
using server.Models;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private static List<Book> books = new List<Book>
        {
            new Book
            {
                ID = "9b0896fa-3880-4c2e-bfd6-925c87f22878",
                Title = "CQRS for Dummies",
                Publisher = "head publisher",
                DateOfPublication = DateTime.Now
            },
            new Book
            {
                ID = "0550818d-36ad-4a8d-9c3a-a715bf15de76",
                Title = "Visual Studio Tips",
                Publisher = "head publisher",
                DateOfPublication = DateTime.Now
            },
            new Book
            {
                ID = "8e0f11f1-be5c-4dbc-8012-c19ce8cbe8e1",
                Title = "NHibernate Cookbook",
                Publisher = "head publisher",
                DateOfPublication = DateTime.Now
            },
            new Book
            {
                ID = "1",
                Title = "Head First C Sharp",
                Publisher = "head publisher",
                DateOfPublication = DateTime.Now
            }
        };

        [HttpGet]
        [Route("allbooks")]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            return Ok(books);
        }

        [HttpGet("{ID}")]
        public async Task<ActionResult<Book>> GetBookByID(string ID)
        {
            var book = books.Find(b => b.ID.Equals(ID));
            if(book == null)
                return BadRequest("Book not find ");
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<List<Book>>> AddBook(Book book)
        {
            books.Add(book);
            return Ok(books);
        }

        [HttpPut]
        public async Task<ActionResult<List<Book>>> UpdateBook(Book request)
        {
            var book = books.Find(b => b.ID.Equals(request.ID));
            if(book == null)
                return BadRequest("Book not find ");

            book.Title = request.Title;
            book.Publisher = request.Publisher;
            book.DateOfPublication = request.DateOfPublication;
            book.IsBorrowed = request.IsBorrowed;

            return Ok(books);
        }

        [HttpDelete("{ID}")]
        public async Task<ActionResult<Book>> DeleteBookByID(string ID)
        {
            var book = books.Find(b => b.ID.Equals(ID));
            if(book == null)
                return BadRequest("Book not find ");
            books.Remove(book);
            return Ok(books);
        }

    }
}