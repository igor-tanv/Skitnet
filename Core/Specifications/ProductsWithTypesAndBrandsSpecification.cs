using Core.Entities;

namespace Core.Specifications
{
  public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
  {
    public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams) 
      : base (x => 
        (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
        (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
      )
    {
      AddInlcudes(x => x.ProductType);
      AddInlcudes(x => x.ProductBrand);
      AddOrderBy(x => x.Name);
      ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

      if (!string.IsNullOrEmpty(productParams.Sort))
      {
        switch(productParams.Sort)
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