using Moq;
using ProductManagement.Data.DTO;
using ProductManagement.Services.Services;
using ProductManagement.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Tests;

namespace ProductManagement.API.Tests
{
    public class ProductApiTest : IDisposable
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly ProductController _controller;
        private readonly Product product;
        private readonly List<ProductDto> record;

        public ProductApiTest()
        {
            // Initialize mocks
            _mockProductService = new Mock<IProductService>();
            _controller = new ProductController(_mockProductService.Object);
            product = new Product();
            record = product.ProductDto();
        }

        [Fact]
        public async Task GetAllProducts_ShouldReturnOkResult_WithProducts()
        {
            // Arrange
            _mockProductService.Setup(service => service.GetAllProducts()).ReturnsAsync(record);

            // Act
            var result = await _controller.GetAllProducts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProducts = Assert.IsType<List<ProductDto>>(okResult.Value);
            Assert.Equal(record.Count, returnedProducts.Count);
        }

        [Fact]
        public async Task GetProduct_ShouldReturnOkResult_WithProduct()
        {
            // Arrange
            _mockProductService.Setup(service => service.GetProductById(product.productId)).ReturnsAsync(record[1]);

            // Act
            var result = await _controller.GetProduct(product.productId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProduct = Assert.IsType<ProductDto>(okResult.Value);
            Assert.Equal(product.productId, returnedProduct.Id);
            Assert.Equal("Mouse", returnedProduct.Name);
        }        [Fact]
        public async Task UpdateProduct_ShouldReturnOkResult()
        {
            // Arrange
            _mockProductService.Setup(service => service.UpdateProduct(record[0]));

            // Act
            var result = await _controller.UpdateProduct(record[0]);

            // Assert
            Assert.IsType<OkResult>(result);
        }
        public void Dispose()
        {
            _mockProductService.VerifyAll();
        }
    }
}
