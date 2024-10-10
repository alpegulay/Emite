using App.Exam.Emite.Data.Helpers;
using Npgsql;
using System.Data.Common;

namespace App.Exam.Emite.Data.Tests.Helpers
{
    public class PostgresConnectionHelperTests
    {
        private readonly PostgresConnectionHelper _postgresConnectionHelper;

        public PostgresConnectionHelperTests()
        {
            _postgresConnectionHelper = new PostgresConnectionHelper();
        }

        [Fact]
        public async Task GetConnectionStringAsync_ShouldReturnDbConnection_WhenConnectionStringIsValid()
        {
            // Arrange
            var expectedConnectionString = "Host=myserver;Username=myuser;Password=mypassword;Database=mydatabase;";
            Environment.SetEnvironmentVariable("ConnectionString", expectedConnectionString);

            // Act
            var result = await _postgresConnectionHelper.GetConnectionStringAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NpgsqlConnection>(result);
            Assert.Equal(expectedConnectionString, result.ConnectionString);
        }     
    }
}
