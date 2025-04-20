namespace DataExplorer.Tests
{
    public class ColumnTypeExtensionsTests
    {
        [Theory]
        [InlineData(ColumnType.INTEGER, "INTEGER")]
        [InlineData(ColumnType.STRING, "STRING")]
        [InlineData(ColumnType.UNKNOWN, "")]
        public void ColumnTypeToString_ShouldReturnExpectedString(ColumnType input, string expected)
        {
            var result = ColumnTypeExtensions.ColumnTypeToString(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("INTEGER", ColumnType.INTEGER)]
        [InlineData("STRING", ColumnType.STRING)]
        [InlineData("integer", ColumnType.INTEGER)]
        [InlineData("string", ColumnType.STRING)]
        [InlineData("unknown", ColumnType.UNKNOWN)]
        [InlineData("INVALID", ColumnType.UNKNOWN)]
        public void StringToColumnType_ShouldReturnExpectedColumnType(string input, ColumnType expected)
        {
            var result = ColumnTypeExtensions.StringToColumnType(input);
            Assert.Equal(expected, result);
        }
    }
}
