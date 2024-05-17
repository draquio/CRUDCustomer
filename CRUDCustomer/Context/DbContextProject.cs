using CRUDCustomer.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDCustomer.Context
{
    public class DbContextProject : DbContext
    {
        public DbContextProject(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
    }
}
