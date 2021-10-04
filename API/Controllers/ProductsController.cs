using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Core.Interfaces;
using System.Collections.Generic;
using Core.Specifications;
using API.Dtos;
using System.Linq;
using AutoMapper;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController: ControllerBase
    {
    private readonly IGenericRepository<Product> _productsRepo;
    private readonly IGenericRepository<ProductType> _productTypesRepo;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<ProductBrand> _productBrandsRepo;

    public ProductsController(
        IGenericRepository<Product> productsRepo,
        IGenericRepository<ProductBrand> productBrandsRepo,
        IGenericRepository<ProductType> productTypesRepo,
        IMapper mapper
    )
    {
        _productsRepo = productsRepo;
        _productBrandsRepo = productBrandsRepo;
        _productTypesRepo = productTypesRepo;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts()
    {
      var spec = new ProductsWithTypesAndBrandsSpecification();
      var products = await _productsRepo.ListAllAsync(spec);

      return products.Select(product => new ProductToReturnDto
      {
        Id = product.Id,
        Name = product.Name,
        Description = product.Description,
        PictureUrl = product.PictureUrl,
        Price = product.Price,
        ProductBrand = product.ProductBrand.Name,
        ProductType = product.ProductType.Name
      }).ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
      var spec = new ProductsWithTypesAndBrandsSpecification(id);
      var product =  await _productsRepo.GetEntityWithSpec(spec);

      return _mapper.Map<Product, ProductToReturnDto>(product);
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