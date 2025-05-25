using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using PaginationAPI.Models;

namespace PaginationAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
    }
}
