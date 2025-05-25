using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaginationAPI.Data;

namespace PaginationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _context.Customers.ToListAsync();
            return Ok(customers);
        }
        [HttpGet("GetCustomerById")]
        public async Task<IActionResult> GetCustomerById(int Id)
        {
            var customer = await _context.Customers.FindAsync(Id);
            return Ok(customer);
        }
    }
}
