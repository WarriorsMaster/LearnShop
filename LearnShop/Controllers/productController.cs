using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearnShop;

namespace LearnShop.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class productController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public productController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<product>> Getproduct(string id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putproduct(string id, product product)
        {
            if (id != product.id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!productExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<product>> Postproduct(product product)
        {

            _context.Products.Add(product);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (productExists(product.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getproduct", new { id = product.id }, product);
        }

        // DELETE: api/product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteproduct(string id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool productExists(string id)
        {
            return _context.Products.Any(e => e.id == id);
        }
    }
}
