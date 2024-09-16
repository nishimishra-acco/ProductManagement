using ProductManagement.Data.DTO;
using ProductManagement.Services.Models.Validators;
using static ProductManagement.Validations.Validate;

namespace ProductManagement.Services.Tests
{
    public class ProductValidatorTest
    {
        private readonly ProductValidator _validator;

        public ProductValidatorTest()
        {
            _validator = new ProductValidator();
        }

        [Fact]
        public void Check_ShouldNotThrowException_WhenProductIsValid()
        {
            // Arrange
            var product = new ProductDto
            {
                Name = "Valid Product",
                Price = 10.0m,
                StockQuantity = 5
            };

            // Act & Assert
            var exception = Record.Exception(() => _validator.Check(product));
            Assert.Null(exception);
        }

        [Fact]
        public void Check_ShouldThrowException_WhenProductIsNull()
        {
            // Arrange
            ProductDto product = null;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => _validator.Check(product));
            Assert.Contains("product cannot be null.", exception.Message);
        }

        [Fact]
        public void Check_ShouldThrowException_WhenNameIsEmpty()
        {
            // Arrange
            var product = new ProductDto
            {
                Name = string.Empty,
                Price = 10.0m,
                StockQuantity = 5
            };

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => _validator.Check(product));
            Assert.Contains("Name cannot be empty.", exception.Message);
        }

        [Fact]
        public void Check_ShouldThrowException_WhenPriceIsBelowMinimum()
        {
            // Arrange
            var product = new ProductDto
            {
                Name = "Valid Product",
                Price = 0.5m, // Below minimum price of 1
                StockQuantity = 5
            };

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => _validator.Check(product));
            Assert.Contains("Price value should be greater than: 1", exception.Message);
        }

        [Fact]
        public void Check_ShouldThrowException_WhenStockQuantityIsBelowMinimum()
        {
            // Arrange
            var product = new ProductDto
            {
                Name = "Valid Product",
                Price = 10.0m,
                StockQuantity = 0 // Below minimum stock quantity of 1
            };

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => _validator.Check(product));
            Assert.Contains("StockQuantity value should be greater than: 1", exception.Message);
        }
    }
}
