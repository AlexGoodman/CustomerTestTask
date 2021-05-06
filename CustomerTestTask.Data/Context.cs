using CustomerTestTask.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerTestTask.Data
{
    public class Context: DbContext
    {
        public DbSet<CustomerEntity> Customers { get; set; }
        public Context(DbContextOptions<Context> options): base(options) 
        {            
            Database.EnsureCreated();
        }
    }
}