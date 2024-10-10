using App.Exam.Emite.Data.Helpers;
using App.Exam.Emite.Data.Interfaces.Entities;
using Moq;

namespace App.Exam.Emite.Data.Tests.Helpers
{
    public class AuditTrailHelperTests
    {
        [Fact]
        public void SetCreated_ShouldSetCreatedByUserIdAndCreatedDate()
        {
            // Arrange
            var userId = 1;
            var entityMock = new Mock<IAuditable>();
            var entity = entityMock.Object;

            // Act
            AuditTrailHelper.SetCreated(userId, entity);

            // Assert
            entityMock.VerifySet(e => e.CreatedByUserId = userId);
            entityMock.VerifySet(e => e.CreatedDate = It.IsAny<DateTime>(), Times.Once);
        }

        [Fact]
        public void SetUpdated_ShouldSetUpdatedByUserIdAndUpdatedDate()
        {
            // Arrange
            var userId = 1;
            var entityMock = new Mock<IAuditable>();
            var entity = entityMock.Object;

            // Act
            AuditTrailHelper.SetUpdated(userId, entity);

            // Assert
            entityMock.VerifySet(e => e.UpdatedByUserId = userId);
            entityMock.VerifySet(e => e.UpdatedDate = It.IsAny<DateTime>(), Times.Once);
        }
    }
}
