using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Models;
using Server.DAL;

namespace server.Controllers
{
    /// <summary>
    /// Controller for Borrowing entity
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingController : ControllerBase
    {
        private readonly DataContext _context;

        public BorrowingController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get method for retrieving all the borrowing data
        /// </summary>
        /// <returns>List of borrowing data</returns>
        [HttpGet]
        [Route("allborrowings")]
        public async Task<ActionResult<List<Borrowing>>> GetBorrowings()
        {
            return Ok(await _context.borrowings.ToListAsync());
        }

        /// <summary>
        /// Get method for getting one single borrowing by ID
        /// </summary>
        /// <param name="Id">Request id to be used to search borrowing data </param>
        /// <returns>A single borrowing with ok response or badrequest while borrowing is not found</returns>
        [HttpGet("{Id}")]
        public async Task<ActionResult<Borrowing>> GetBorrowingByID(string Id)
        {
            var borrowing = await _context.borrowings.FindAsync(Id);
            if(borrowing == null)
                return BadRequest("Borrowing not find ");
            return Ok(borrowing);
        }

        /// <summary>
        /// Post method for creating borrowing data
        /// </summary>
        /// <param name="borrowing">borrowing data received from request</param>
        /// <returns>List of updated borrowings</returns>
        [HttpPost]
        public async Task<ActionResult<List<Borrowing>>> AddBorrowing([FromBody]Borrowing borrowing)
        {
            _context.borrowings.Add(borrowing);
            await _context.SaveChangesAsync();
            return Ok(await _context.borrowings.ToListAsync());
        }

        /// <summary>
        /// Put method for updating the borrowing status(isBorrowed)
        /// </summary>
        /// <param name="request">Borrowing data received from request</param>
        /// <returns>A list of updated borrowing with ok response or badrequest while borrowing is not found</returns>
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

/// <summary>
        /// Delete method for deletion of a targeted borrowing
        /// </summary>
        /// <param name="Id">A string uuid received from request</param>
        /// <returns>A list of updated borrowings with ok response or badrequest while borrowing is not found</returns>
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