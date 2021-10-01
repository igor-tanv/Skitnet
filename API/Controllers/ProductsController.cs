using System.Collections;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
  public class ProductsController: ControllerBase
    {
    private readonly StoreContext _context;
    public ProductsController(StoreContext context)
    {
        _context = context;
    }

    [HttpGet]
        public async Task<ActionResult<IEnumerable>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await _context.Products.FindAsync(id);
        }
    }
}