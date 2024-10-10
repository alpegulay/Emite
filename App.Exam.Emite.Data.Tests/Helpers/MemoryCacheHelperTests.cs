using App.Exam.Emite.Data.Helpers;
using System.Runtime.Caching;

namespace App.Exam.Emite.Data.Tests.Helpers
{
    public class MemoryCacheHelperTests
    {
        private readonly MemoryCacheHelper _memoryCacheHelper;
        private readonly MemoryCache _cache;

        public MemoryCacheHelperTests()
        {
            _memoryCacheHelper = new MemoryCacheHelper();
            _cache = MemoryCache.Default;
        }

        [Fact]
        public async Task GetObjectFromCache_ShouldReturnDefault_WhenNullValueCached()
        {
            // Arrange
            string cacheItemName = "NullCacheItem";
            _cache.Set(cacheItemName, MemoryCacheHelper.NULL_VALUE, DateTimeOffset.Now.AddMinutes(10));

            // Act
            var result = await _memoryCacheHelper.GetObjectFromCache<string>(cacheItemName, 10, null);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetObjectFromCache_ShouldCallSetterCallback_WhenCacheDoesNotExist()
        {
            // Arrange
            string cacheItemName = "NewCacheItem";
            var expectedValue = "NewValue";
            var objectSetterCallback = new Func<Task<string>>(async () => await Task.FromResult(expectedValue));

            // Act
            var result = await _memoryCacheHelper.GetObjectFromCache(cacheItemName, 10, objectSetterCallback);

            // Assert
            Assert.Equal(expectedValue, result);
            Assert.True(_cache.Contains(cacheItemName)); 
            Assert.Equal(expectedValue, _cache[cacheItemName]); 
        }

        [Fact]
        public async Task GetObjectFromCache_ShouldThrowException_WhenSetterCallbackIsNull()
        {
            // Arrange
            string cacheItemName = "NoSetterCacheItem";

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() =>
                _memoryCacheHelper.GetObjectFromCache<string>(cacheItemName, 10, null));

            Assert.Equal($"There is no cache object setter for {cacheItemName}", exception.Message);
        }

        [Fact]
        public async Task GetObjectFromCache_ShouldCacheNullValue_WhenObjectSetterReturnsNull()
        {
            // Arrange
            string cacheItemName = "NullReturnedCacheItem";
            var objectSetterCallback = new Func<Task<string>>(async () => await Task.FromResult<string>(null));

            // Act
            var result = await _memoryCacheHelper.GetObjectFromCache(cacheItemName, 10, objectSetterCallback);

            // Assert
            Assert.Null(result);
            Assert.True(_cache.Contains(cacheItemName));
            Assert.Equal(MemoryCacheHelper.NULL_VALUE, _cache[cacheItemName]);
        }
    }
}
