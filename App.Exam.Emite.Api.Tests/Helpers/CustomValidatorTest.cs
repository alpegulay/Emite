using App.Exam.Emite.Api.Core.Helpers;
using App.Exam.Emite.Api.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace App.Exam.Emite.Api.Core.Tests.Helpers
{
    public class CustomValidatorTests
    {
        [Fact]
        public void IsModelValid_ValidModel_ReturnsTrue()
        {
            // Arrange
            var model = new TestModel
            {
                Name = "Valid Name",
                Age = 30 
            };

            // Act
            var result = CustomValidator.IsModelValid(model);

            // Assert
            Assert.True(result);
            Assert.Empty(model.ValidationResult); 
        }

        [Fact]
        public void IsModelValid_InvalidModel_ReturnsFalse()
        {
            // Arrange
            var model = new TestModel
            {
                Name = "", 
                Age = -1  
            };

            // Act
            var result = CustomValidator.IsModelValid(model);

            // Assert
            Assert.False(result);
            Assert.NotEmpty(model.ValidationResult);
            Assert.Equal(2, model.ValidationResult.Count); 

            Assert.Contains(model.ValidationResult, e => e.Key == "name" && e.Message == "The Name field is required.");
            Assert.Contains(model.ValidationResult, e => e.Key == "age" && e.Message == "The Age field must be greater than or equal to 0.");
        }

        [Fact]
        public void IsModelValid_NullModel_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => CustomValidator.IsModelValid<ValidatableModel>(null));
        }

        [Fact]
        public void IsModelValid_ModelWithMultipleErrors_ReturnsFalse()
        {
            // Arrange
            var model = new TestModel
            {
                Name = "", 
                Age = -1 
            };

            // Act
            var result = CustomValidator.IsModelValid(model);

            // Assert
            Assert.False(result);
            Assert.NotEmpty(model.ValidationResult); 
            Assert.Equal(2, model.ValidationResult.Count); 

            Assert.Contains(model.ValidationResult, e => e.Key == "name" && e.Message == "The Name field is required.");
            Assert.Contains(model.ValidationResult, e => e.Key == "age" && e.Message == "The Age field must be greater than or equal to 0.");
        }
    }

    // Supporting Test Model for Validation
    public class TestModel : ValidatableModel
    {
        [Required(ErrorMessage = "The Name field is required.")]
        public string Name { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "The Age field must be greater than or equal to 0.")]
        public int Age { get; set; }
    }
}
