using App.Exam.Emite.Data.Helpers;

namespace App.Exam.Emite.Data.Tests.Helpers
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("hello_world", "helloworld")]
        [InlineData("this_is_a_test", "thisisatest")]
        [InlineData("Another_Test_String", "anotherTestString")]
        [InlineData("AlreadyCamelCase", "alreadyCamelCase")]
        [InlineData("_leadingUnderscore", "leadingUnderscore")]
        [InlineData("trailingUnderscore_", "trailingUnderscore")]
        [InlineData("Multiple___Underscores", "multipleUnderscores")]
        [InlineData("", "Null")] // Edge case: empty string
        public void CamelCase_ShouldConvertToCamelCase(string input, string expected)
        {
            // Act
            var result = input.CamelCase();

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("test", "test", true)]
        [InlineData("test", "Test", true)]
        [InlineData("test", "TEST", true)]
        [InlineData("test", "notTest", false)]
        [InlineData("test", null, false)]
        [InlineData(null, "test", false)]
        [InlineData(null, null, true)]
        public void IgnoreCaseEquals_ShouldCompareStringsIgnoringCase(string s, string a, bool expected)
        {
            // Act
            var result = s.IgnoreCaseEquals(a);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Hello World", "world", true)]
        [InlineData("Hello World", "WORLD", true)]
        [InlineData("Hello World", "notInString", false)]
        [InlineData(null, "notInString", false)]
        [InlineData(null, null, false)]
        [InlineData("", "anything", false)]
        [InlineData("anything", "", true)]
        public void IgnoreCaseContains_ShouldCheckIfStringContainsAnotherIgnoringCase(string s, string a, bool expected)
        {
            // Act
            var result = s.IgnoreCaseContains(a);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
