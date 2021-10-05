using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
  public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
  {
    public ProductsWithTypesAndBrandsSpecification(string sort, int? brandId, int? typeId) 
      : base (x => 
        (!brandId.HasValue || x.ProductBrandId == brandId) &&
        (!typeId.HasValue || x.ProductTypeId == typeId)
      )
    {
      AddInlcudes(x => x.ProductType);
      AddInlcudes(x => x.ProductBrand);
      AddOrderBy(x => x.Name);

      if (!string.IsNullOrEmpty(sort))
      {
        switch(sort)
        {
          case "priceAsc":
            AddOrderBy(p => p.Price);
            break;
          case "priceDesc":
            AddOrderByDesc(p => p.Price);
            break;
          default:
           AddOrderBy(x => x.Name);
            break;
        }
      }
    }

    public ProductsWithTypesAndBrandsSpecification(int id) : 
      base(product => product.Id == id)
    {
      AddInlcudes(x => x.ProductType);
      AddInlcudes(x => x.ProductBrand);
    }
  }
}