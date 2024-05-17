using CRUDCustomer.Context;
using CRUDCustomer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDCustomer.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DbContextProject _context;

        public CustomerController(DbContextProject context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetSingleCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) {
                return NotFound();
            }
            return Ok(customer);
        }
        [HttpPost]
        public async Task<ActionResult<Customer>> saveCustomer(Customer customer) {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCustomers), new { id = customer.Id }, customer);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult> updateCustomer(Customer customer, int id)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }
            try
            {
                _context.Entry(customer).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExist(id))
                {
                    return NotFound();
                }
                throw;
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            try
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Ok();
        }
        
        
        private bool CustomerExist(int id)
        {
            return _context.Customers.Any(c =>  c.Id == id);
        }
    }
}
