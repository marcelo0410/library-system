using Microsoft.EntityFrameworkCore;
using server.Models;

namespace Server.DAL
{
    /// <summary>
    /// Database context for manipulation of data
    /// </summary>
    /// <returns>List of books</returns>
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}

        public DbSet<Book> books { get; set; }
        public DbSet<Borrowing> borrowings { get; set; }

        /// <summary>
        /// Configure the model 
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Borrowing>()
                .HasOne(borrowing => borrowing.BookForeignKey)
                .WithMany(book => book.Borrowings)
                .HasForeignKey(borrowing => borrowing.BookId);
        }
    }
}