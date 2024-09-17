using Moq;
using ProductManagement.Data.DTO;
using ProductManagement.Services.Services;
using ProductManagement.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Tests.Common;
using ProductManagement.Data.Domain;

namespace ProductManagement.Tests.Controller
{
    public class ProductControllerTest : IDisposable
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly ProductController _controller;
        private readonly List<ProductDto> record;

        public ProductControllerTest()
        {
            // Initialize mocks
            _mockProductService = new Mock<IProductService>();
            _controller = new ProductController(_mockProductService.Object);
            record = Product.ProductDtoList;
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
            _mockProductService.Setup(service => service.GetProductById(Product.productId)).ReturnsAsync(record[1]);

            // Act
            var result = await _controller.GetProduct(Product.productId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProduct = Assert.IsType<ProductDto>(okResult.Value);
            Assert.Equal(Product.productId, returnedProduct.Id);
            Assert.Equal("Mouse", returnedProduct.Name);
        }

        [Fact]
        public async Task UpdateProduct_ShouldReturnOkResult()
        {
            // Arrange
            _mockProductService.Setup(service => service.UpdateProduct(record[0]));

            // Act
            var result = await _controller.UpdateProduct(record[0]);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task AddProduct_ValidProductDto_ReturnsCreatedAtAction()
        {
            // Arrange
            var productDto = Product.ProductDtoList[0];

            var product = new ProductRecord
            {
                Id = Guid.NewGuid(),
                Name = productDto.Name,
                Price = productDto.Price,
                StockQuantity = productDto.StockQuantity
            };

            // Setup the mock to return the created product
            _mockProductService.Setup(service => service.CreateProduct(productDto))
                .ReturnsAsync(product);

            // Act
            var result = await _controller.AddProduct(productDto);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetProduct", actionResult.ActionName);
            Assert.Equal(product.Id, actionResult.RouteValues["id"]);
        }
        public void Dispose()
        {
            _mockProductService.VerifyAll();
        }
    }
}
