using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
  public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
  {
    public ProductsWithTypesAndBrandsSpecification()
    {
      AddInlcudes(x => x.ProductType);
      AddInlcudes(x => x.ProductBrand);
    }

    public ProductsWithTypesAndBrandsSpecification(int id) : 
      base(product => product.Id == id)
    {
      AddInlcudes(x => x.ProductType);
      AddInlcudes(x => x.ProductBrand);
    }
  }
}