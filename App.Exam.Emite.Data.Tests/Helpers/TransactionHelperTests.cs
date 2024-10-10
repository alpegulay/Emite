using App.Exam.Emite.Data.Helpers;
using Microsoft.Extensions.Logging;
using Moq;
using System.Data;

namespace App.Exam.Emite.Data.Tests.Helpers
{
    public class TransactionHelperTests
    {
        private readonly Mock<ILogger<TransactionHelper>> _mockLogger;

        public TransactionHelperTests()
        {
            _mockLogger = new Mock<ILogger<TransactionHelper>>();
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_WhenLoggerIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new TransactionHelper(null));
        }

        [Fact]
        public void Constructor_ShouldInitializeLogger_WhenLoggerIsProvided()
        {
            // Act
            var transactionHelper = new TransactionHelper(_mockLogger.Object);

            // Assert
            Assert.NotNull(transactionHelper);
        }
    }
}
