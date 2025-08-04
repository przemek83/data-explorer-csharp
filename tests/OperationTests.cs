using DataExplorer;

namespace Tests
{
    public class OperationTests
    {
        [Theory]
        [InlineData("avg", Operation.Type.AVG)]
        [InlineData("min", Operation.Type.MIN)]
        [InlineData("max", Operation.Type.MAX)]
        [InlineData("unknown", Operation.Type.UNKNOW)]
        [InlineData("invalid", Operation.Type.UNKNOW)]
        [InlineData("", Operation.Type.UNKNOW)]
        public void ToType_ShouldReturnCorrectType(string input, Operation.Type expected)
        {
            // Arrange
            var operation = new Operation();

            // Act
            var result = Operation.ToType(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Allowed_ShouldReturnExpectedValues()
        {
            // Arrange
            var expected = new[] { "avg", "min", "max" };

            // Act
            var result = Operation.Allowed();

            // Assert
            Assert.Equal(expected, result);
        }
    }
}