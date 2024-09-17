using ProductManagement.Data.Exceptions;
using ProductManagement.Data.Repositories;
using ProductManagement.Tests.Common;

namespace ProductManagement.Tests.Repository
{
    public class ProductRepositoryTest
    {
        private readonly ProductRepository _mockRepository;

        public ProductRepositoryTest()
        {
            _mockRepository = new ProductRepository();
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllProducts()
        {
            // Arrange
            var firstProduct = Product.ProductRecordList[0];
            var secondProduct = Product.ProductRecordList[1];

            await _mockRepository.Create(firstProduct);
            await _mockRepository.Create(secondProduct);

            // Act
            var products = await _mockRepository.GetAll();

            // Assert
            Assert.Contains(firstProduct, products);
            Assert.Contains(secondProduct, products);
        }

        [Fact]
        public async Task GetById_ShouldReturnProduct_WhenProductExists()
        {
            var record = Product.ProductRecordList[0];
            // Arrange
            await _mockRepository.Create(record);

            // Act
            var result = await _mockRepository.GetById(record.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(record.Id, result.Id);
            Assert.Equal(record.Name, result.Name);
        }

        [Fact]
        public async Task GetById_ShouldThrowException_WhenProductDoesNotExist()
        {
            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _mockRepository.GetById(Guid.NewGuid()));
        }

        [Fact]
        public async Task Create_ShouldAddProductToRepository()
        {
            var record = Product.ProductRecordList[0];
            // Act
            var result = await _mockRepository.Create(record);

            // Assert
            Assert.Equal(record.Id, result.Id);
            Assert.Equal(record.Name, result.Name);

            var products = await _mockRepository.GetAll();
            Assert.Contains(record, products);
        }

        [Fact]
        public async Task Update_ShouldModifyProduct_WhenProductExists()
        {
            // Arrange
            var originalProduct = Product.ProductRecordList[0];
            await _mockRepository.Create(originalProduct);

            var updatedProduct = originalProduct;
            updatedProduct.Name = "CPU";
            // Act
            await _mockRepository.Update(updatedProduct);

            // Assert
            var result = await _mockRepository.GetById(originalProduct.Id);
            Assert.Equal(updatedProduct.Name, result.Name);
        }

        [Fact]
        public async Task Update_ShouldThrowException_WhenProductDoesNotExist()
        {
            // Arrange
            var product = Product.ProductRecordList[2];

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _mockRepository.Update(product));
        }
    }
}
