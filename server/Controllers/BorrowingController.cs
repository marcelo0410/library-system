using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Models;
using Server.DAL;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingController : ControllerBase
    {
        private readonly DataContext _context;

        public BorrowingController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("allborrowings")]
        public async Task<ActionResult<List<Borrowing>>> GetBorrowings()
        {
            return Ok(await _context.borrowings.ToListAsync());
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Borrowing>> GetBorrowingByID(string Id)
        {
            var borrowing = await _context.borrowings.FindAsync(Id);
            if(borrowing == null)
                return BadRequest("Borrowing not find ");
            return Ok(borrowing);
        }

        [HttpPost]
        public async Task<ActionResult<List<Borrowing>>> AddBorrowing(Borrowing borrowing)
        {
            _context.borrowings.Add(borrowing);
            await _context.SaveChangesAsync();
            return Ok(await _context.borrowings.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Borrowing>>> UpdateBorrowing(Borrowing request)
        {
            var dbborrowing = await _context.borrowings.FindAsync(request.Id);
            if(dbborrowing == null)
                return BadRequest("Borrowing not find ");

            dbborrowing.UserId = request.UserId;
            dbborrowing.BookId = request.BookId;
            dbborrowing.DateOfBorrow = request.DateOfBorrow;
            dbborrowing.DueDate = request.DueDate;
            await _context.SaveChangesAsync();
            return Ok(await _context.borrowings.ToListAsync());
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<Borrowing>> DeleteBorrowingByID(string Id)
        {
            var dbborrowing = await _context.borrowings.FindAsync(Id);
            if(dbborrowing == null)
                return BadRequest("borrowing not find ");
            _context.borrowings.Remove(dbborrowing);
            await _context.SaveChangesAsync();
            return Ok(await _context.borrowings.ToListAsync());
        }

    }
}