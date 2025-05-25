using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaginationAPI.Data;
using PaginationAPI.Filters;
using PaginationAPI.Helpers;
using PaginationAPI.Models;
using PaginationAPI.Services;
using PaginationAPI.Wrappers;

namespace PaginationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUriService _uriServeice;
        public CustomerController(ApplicationDbContext context, IUriService uriServeice)
        {
            _context = context;
            _uriServeice = uriServeice;
        }
        [HttpGet("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomers([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.Customers
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            var totalRecords = await _context.Customers.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<Customer>(pagedData, validFilter, totalRecords, _uriServeice, route);
            return Ok(pagedReponse);
        }
        [HttpGet("GetCustomerById({Id})")]
        public async Task<IActionResult> GetCustomerById(int Id)
        {
            var customer = await _context.Customers.FindAsync(Id);
            return Ok(new Response<Customer>(customer));
        }
    }
}
