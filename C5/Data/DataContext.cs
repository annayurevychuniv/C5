using C5.Entities;
using Microsoft.EntityFrameworkCore;

namespace C5.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { 
        }

        public DbSet<Author> Authors { get; set; }
    }
}
