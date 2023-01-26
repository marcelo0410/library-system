using Microsoft.EntityFrameworkCore;
using server.Models;

namespace Server.DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}

        public DbSet<Book> books { get; set; }
    }
}