using Moq;
using ProductManagement.Data.Domain;
using ProductManagement.Data.DTO;
using ProductManagement.Data.Mappers;
using ProductManagement.Data.Models.Validators;
using ProductManagement.Data.Repositories;
using ProductManagement.Services.Services;

namespace ProductManagement.Tests
{
    public class ProductServiceTest : IDisposable
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<IValidateProduct> _mockValidateProduct;
        private readonly ProductService _productService;
        private readonly List<ProductDto> record;

        public ProductServiceTest()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockValidateProduct = new Mock<IValidateProduct>();
            _productService = new ProductService(_mockProductRepository.Object, _mockValidateProduct.Object);
            record = Product.ProductDtoList;
        }

        [Fact]
        public async Task GetAllProducts_ShouldReturnProducts()
        {
            // Arrange

            _mockProductRepository.Setup(repo => repo.GetAll()).ReturnsAsync(Product.ProductRecordList);

            // Act
            var result = await _productService.GetAllProducts();

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(4, result.Count());
        }

        [Fact]
        public async Task GetProductById_ShouldReturnProduct()
        {
            // Arrange
            _mockProductRepository.Setup(repo => repo.GetById(Product.productId)).ReturnsAsync(Product.ProductRecordList[1]);

            // Act
            var result = await _productService.GetProductById(Product.productId);

            // Assert
            Assert.Equal("Mouse", result.Name);
        }
        //[Fact]
        //public async Task UpdateProduct_ShouldCallRepositoryUpdate()
        //{
        //    // Arrange

        //    _mockValidateProduct.Setup(validator => validator.Check(record[0]));
        //    ProductRecord productRecord = product.ProductRecordList()[0];
        //    _mockProductRepository.Setup(repo => repo.Update(productRecord)).Returns(Task.CompletedTask);

        //    // Act
        //    record[0].Name = "Test12";
        //    await _productService.UpdateProduct(record[0]);

        //    // Assert
        //    _mockValidateProduct.Verify(validator => validator.Check(record[0]), Times.Once);


        //    var expectedProductRecord = product.ProductRecordList().First();

        //    var productdto = ProductMapper.ToProductDTO(expectedProductRecord);

        //    _mockProductRepository.Setup(repo => repo.Update(It.Is<ProductRecord>(pr =>

        //    pr.Name == expectedProductRecord.Name &&

        //    pr.Price == expectedProductRecord.Price)))

        //    .Returns(Task.CompletedTask);

        //    // Act

        //    await _productService.UpdateProduct(productdto);

        //    // Assert
        //}
        [Fact]
        public async Task CreateProduct_ValidProductDto_CreatesProduct()
        {
            // Arrange

            var expectedProductRecord = Product.ProductRecordList.First();

            var productdto = ProductMapper.ToProductDTO(expectedProductRecord);

            _mockProductRepository.Setup(repo => repo.Create(It.Is<ProductRecord>(pr =>

            pr.Name == expectedProductRecord.Name &&

            pr.Price == expectedProductRecord.Price)))

            .ReturnsAsync(expectedProductRecord);

            // Act

            var result = await _productService.CreateProduct(productdto);

            // Assert

            Assert.Equal(expectedProductRecord.Name, result.Name);

            _mockProductRepository.Verify(repo => repo.Create(It.Is<ProductRecord>(pr => pr.Name == productdto.Name && pr.Price == productdto.Price)), Times.Once);
        }

        public void Dispose() {
            _mockProductRepository.VerifyAll();
        }
    }
}
