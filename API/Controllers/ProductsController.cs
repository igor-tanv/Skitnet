using System.Collections;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Interfaces;
using System.Collections.Generic;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
  public class ProductsController: ControllerBase
    {
   
    private readonly IProductRepository _repo;
    public ProductsController(IProductRepository repo)
    {
       _repo = repo;
    }

    [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            return await _repo.GetProductsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await _repo.GetProductByIdAsync(id);
        }
    }
}