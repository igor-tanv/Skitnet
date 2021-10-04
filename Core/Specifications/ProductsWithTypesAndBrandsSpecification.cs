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
  }
}