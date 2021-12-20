using Microsoft.EntityFrameworkCore;

namespace EFCrud.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Person> Persons { get; set; }
    }
}
