namespace DataExplorer.Tests
{
    public class ColumnStringTests
    {
        [Fact]
        public void GetColumnType_ShouldReturnString()
        {
            var column = new ColumnString();
            var columnType = column.GetColumnType();
            Assert.Equal(ColumnType.STRING, columnType);
        }

        [Fact]
        public void GetSize_ShouldReturnCorrectSize()
        {
            var column = new ColumnString();
            column.AddData("Test1");
            column.AddData("Test2");
            var size = column.GetSize();
            Assert.Equal(2, size);
        }

        [Fact]
        public void AddData_ShouldAddDataCorrectly()
        {
            var column = new ColumnString();
            column.AddData("TestValue");
            Assert.Equal("TestValue", column.GetData(0));
        }

        [Fact]
        public void GetData_ShouldReturnCorrectData()
        {
            var column = new ColumnString();
            column.AddData("Value1");
            column.AddData("Value2");
            var data = column.GetData(1);
            Assert.Equal("Value2", data);
        }

        [Fact]
        public void GetData_ShouldThrowExceptionForInvalidIndex()
        {
            var column = new ColumnString();
            Assert.Throws<ArgumentOutOfRangeException>(() => column.GetData(0));
        }
    }
}
