using ProductManagement.Validations;

namespace Validations.Tests
{
    public class ValidationTest
    {
        [Fact]
        public void IsNotNull_ShouldAddError_WhenValueIsNull()
        {
            // Arrange
            var builder = Validate.Begin();

            // Act
            builder.IsNotNull(null, "TestProperty");

            // Assert
            var exception = Assert.Throws<Validate.ValidationException>(() => builder.Check());
            Assert.Contains("TestProperty cannot be null.", exception.Message);
        }

        [Fact]
        public void IsNotEmpty_ShouldAddError_WhenValueIsEmpty()
        {
            // Arrange
            var builder = Validate.Begin();

            // Act
            builder.IsNotEmpty("   ", "TestProperty");

            // Assert
            var exception = Assert.Throws<Validate.ValidationException>(() => builder.Check());
            Assert.Contains("TestProperty cannot be empty.", exception.Message);
        }

        [Fact]
        public void Min_ShouldAddError_WhenValueIsLessThanMinValue()
        {
            // Arrange
            var builder = Validate.Begin();

            // Act
            builder.Min(5, "TestProperty", 10);

            // Assert
            var exception = Assert.Throws<Validate.ValidationException>(() => builder.Check());
            Assert.Contains("TestProperty value should be greater than: 10", exception.Message);
        }

        [Fact]
        public void Min_ShouldAddError_WhenDecimalValueIsLessThanMinValue()
        {
            // Arrange
            var builder = Validate.Begin();

            // Act
            builder.Min(5.5m, "TestProperty", 10);

            // Assert
            var exception = Assert.Throws<Validate.ValidationException>(() => builder.Check());
            Assert.Contains("TestProperty value should be greater than: 10", exception.Message);
        }

        [Fact]
        public void Check_ShouldNotThrowException_WhenNoErrors()
        {
            // Arrange
            var builder = Validate.Begin();

            // Act
            builder.IsNotNull(new object(), "TestProperty")
                   .IsNotEmpty("NotEmpty", "TestProperty")
                   .Min(10, "TestProperty", 5)
                   .Check();
        }
    }
}
