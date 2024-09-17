using ProductManagement.Services.Models.Validators;
using ProductManagement.Tests.Common;
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
            var product = Product.ProductDtoList[2];
            // Act & Assert
            var exception = Record.Exception(() => _validator.Check(product));
            Assert.Null(exception);
        }

        [Fact]
        public void Check_ShouldThrowException_WhenProductIsNull()
        {
            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => _validator.Check(null));
            Assert.Contains("product cannot be null.", exception.Message);
        }

        [Fact]
        public void Check_ShouldThrowException_WhenNameIsEmpty()
        {
            var product = Product.ProductDtoList[3];
            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => _validator.Check(product));
            Assert.Contains("Name cannot be empty.", exception.Message);
        }

        [Fact]
        public void Check_ShouldThrowException_WhenPriceIsBelowMinimum()
        {
            var product = Product.ProductDtoList[4];
            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => _validator.Check(product));
            Assert.Contains("Price value should be greater than: 1", exception.Message);
        }

        [Fact]
        public void Check_ShouldThrowException_WhenStockQuantityIsBelowMinimum()
        {
            var product = Product.ProductDtoList[5];
            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => _validator.Check(product));
            Assert.Contains("StockQuantity value should be greater than: 1", exception.Message);
        }
    }
}
