using Microsoft.EntityFrameworkCore;
using server.Models;

namespace Server.DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}

        public DbSet<Book> books { get; set; }
        public DbSet<Borrowing> borrowings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Borrowing>()
                .HasOne(borrowing => borrowing.BookForeignKey)
                .WithMany(book => book.Borrowings)
                .HasForeignKey(borrowing => borrowing.BookId);
        }
    }
}