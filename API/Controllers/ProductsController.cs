using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Core.Interfaces;
using System.Collections.Generic;
using Core.Specifications;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController: ControllerBase
    {
    private readonly IGenericRepository<Product> _productsRepo;
    private readonly IGenericRepository<ProductType> _productTypesRepo;
    private readonly IGenericRepository<ProductBrand> _productBrandsRepo;

    public ProductsController(
        IGenericRepository<Product> productsRepo,
        IGenericRepository<ProductBrand> productBrandsRepo,
        IGenericRepository<ProductType> productTypesRepo
    )
    {
        _productsRepo = productsRepo;
        _productBrandsRepo = productBrandsRepo;
        _productTypesRepo = productTypesRepo;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
      var spec = new ProductsWithTypesAndBrandsSpecification();
      return Ok(await _productsRepo.ListAllAsync(spec));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
      var spec = new ProductsWithTypesAndBrandsSpecification(id);
      return await _productsRepo.GetEntityWithSpec(spec);
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
    {
      return Ok(await _productBrandsRepo.ListAllASync());
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
    {
    return Ok(await _productTypesRepo.ListAllASync());
    }
   }
}