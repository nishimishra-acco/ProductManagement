using ProductManagement.Data.Exceptions;
using ProductManagement.Data.Repositories;
using ProductManagement.Tests.Common;

namespace ProductManagement.Tests.Repository
{
    public class ProductRepositoryTest
    {
        private readonly ProductRepository _repository;

        public ProductRepositoryTest()
        {
            _repository = new ProductRepository();
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllProducts()
        {
            var firstProduct = Product.ProductRecordList[0];
            await _repository.Create(firstProduct);
            var products = await _repository.GetAll();
            Assert.Contains(firstProduct, products);
        }

        [Fact]
        public async Task GetById_ShouldReturnProduct_WhenProductExists()
        {
            var record = Product.ProductRecordList[0];
            await _repository.Create(record);
            var result = await _repository.GetById(record.Id);
            Assert.NotNull(result);
            Assert.Equal(record.Id, result.Id);
            Assert.Equal(record.Name, result.Name);
        }

        [Fact]
        public async Task GetById_ShouldThrowException_WhenProductDoesNotExist()
        {
            await Assert.ThrowsAsync<NotFoundException>(() => _repository.GetById(Guid.NewGuid()));
        }

        [Fact]
        public async Task Create_ShouldAddProductToRepository()
        {
            var record = Product.ProductRecordList[0];
            var result = await _repository.Create(record);
            Assert.Equal(record.Id, result.Id);
            Assert.Equal(record.Name, result.Name);
            var products = await _repository.GetAll();
            Assert.Contains(record, products);
        }

        [Fact]
        public async Task Update_ShouldModifyProduct_WhenProductExists()
        {
            var originalProduct = Product.ProductRecordList[0];
            await _repository.Create(originalProduct);
            var updatedProduct = originalProduct;
            updatedProduct.Name = "CPU";
            await _repository.Update(updatedProduct);
            var result = await _repository.GetById(originalProduct.Id);
            Assert.Equal(updatedProduct.Name, result.Name);
        }

        [Fact]
        public async Task Update_ShouldThrowException_WhenProductDoesNotExist()
        {
            var product = Product.ProductRecordList[2];
            await Assert.ThrowsAsync<NotFoundException>(() => _repository.Update(product));
        }
    }
}
