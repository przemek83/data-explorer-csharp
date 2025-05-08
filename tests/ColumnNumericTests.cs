namespace DataExplorer.Tests
{
    public class ColumnNumericTests
    {
        [Fact]
        public void GetColumnType_ShouldReturnInteger()
        {
            var column = new ColumnNumeric();
            var columnType = column.GetColumnType();
            Assert.Equal(ColumnType.INTEGER, columnType);
        }

        [Fact]
        public void GetSize_ShouldReturnZero_WhenNoDataAdded()
        {
            var column = new ColumnNumeric();
            var size = column.GetSize();
            Assert.Equal(0, size);
        }

        [Fact]
        public void AddData_ShouldIncreaseSize()
        {
            var column = new ColumnNumeric();
            column.AddData(10);
            var size = column.GetSize();
            Assert.Equal(1, size);
        }

        [Fact]
        public void GetData_ShouldReturnCorrectValue()
        {
            var column = new ColumnNumeric();
            column.AddData(10);
            var value = column.GetData(0);
            Assert.Equal(10, value);
        }

        [Fact]
        public void GetData_ShouldThrowException_WhenIndexOutOfRange()
        {
            var column = new ColumnNumeric();
            Assert.Throws<ArgumentOutOfRangeException>(() => column.GetData(0));
        }
    }
}
