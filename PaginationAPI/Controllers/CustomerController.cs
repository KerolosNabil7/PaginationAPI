using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaginationAPI.Data;
using PaginationAPI.Filters;
using PaginationAPI.Models;
using PaginationAPI.Wrappers;

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
        public async Task<IActionResult> GetAllCustomers([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var customers = await _context.Customers
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            var totalRecords = await _context.Customers.CountAsync();
            return Ok(new PagedResponse<List<Customer>>(customers, filter.PageNumber, filter.PageSize));
        }
        [HttpGet("GetCustomerById({Id})")]
        public async Task<IActionResult> GetCustomerById(int Id)
        {
            var customer = await _context.Customers.FindAsync(Id);
            return Ok(new Response<Customer>(customer));
        }
    }
}
