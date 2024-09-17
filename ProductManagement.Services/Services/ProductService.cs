using ProductManagement.Data.Domain;
using ProductManagement.Data.DTO;
using ProductManagement.Data.Mappers;
using ProductManagement.Data.Models.Validators;
using ProductManagement.Data.Repositories;

namespace ProductManagement.Services.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository productRepository;
    private readonly IValidateProduct validateProduct;

    public ProductService(IProductRepository productRepository, IValidateProduct validateProduct)
    {
        this.productRepository = productRepository;
        this.validateProduct = validateProduct;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProducts()
    {
        var products = await productRepository.GetAll();
        return products.Select(ProductMapper.ToProductDTO);
    }

    public async Task<ProductDto> GetProductById(Guid id)
    {
        var product = await productRepository.GetById(id);
        return ProductMapper.ToProductDTO(product);
    }

    public Task<ProductRecord> CreateProduct(ProductDto productDto)
    {
        validateProduct.Check(productDto);
        productDto.Id = Guid.NewGuid();
        var productRecord = ProductMapper.ToProductRecord(productDto);
        return productRepository.Create(productRecord);
    }

    public Task UpdateProduct(ProductDto productDto)
    {
        validateProduct.Check(productDto);
        var productRecord = ProductMapper.ToProductRecord(productDto);
        return productRepository.Update(productRecord);
    }
}
