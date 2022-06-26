using System;
using ConverterService.Business;
using Xunit;

namespace ConverterService.Tests.Business
{
    public class ConverterLogicTests
    {
        [Theory]
        [InlineData(0, "zero dollars")]
        [InlineData(1, "one dollar")]
        [InlineData(25.1, "twenty-five dollars and ten cents")]
        [InlineData(0.01, "zero dollars and one cent")]
        [InlineData(45100, "forty-five thousand one hundred dollars")]
        [InlineData(999999999.99, "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents")]
        public void ConvertNumberToWord_StateUnderTest_ExpectedBehavior(double number, string expected)
        {
            // Arrange
            var converterLogic = new ConverterLogic();

            // Act
            var result = converterLogic.ConvertNumberToWord(
                number);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}