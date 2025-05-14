namespace DataExplorer.Tests
{
    public class DatasetTests
    {
        private class MockDataLoader : IDataLoader
        {
            public bool LoadResult { get; set; } = true;
            public string[] Headers { get; set; } = [];
            public ColumnType[] ColumnTypes { get; set; } = [];
            public IColumn[] Data { get; set; } = [];

            public bool Load() => LoadResult;
            public string[] GetHeaders() => Headers;
            public ColumnType[] GetColumnTypes() => ColumnTypes;
            public IColumn[] GetData() => Data;
        }

        [Fact]
        public void Initialize_ShouldReturnTrue_WhenLoaderSucceeds()
        {
            // Arrange
            var loader = new MockDataLoader();
            var dataset = new Dataset(loader);

            // Act
            var result = dataset.Initialize();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Initialize_ShouldReturnFalse_WhenLoaderFails()
        {
            // Arrange
            var loader = new MockDataLoader { LoadResult = false };
            var dataset = new Dataset(loader);

            // Act
            var result = dataset.Initialize();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ColumnNameToId_ShouldReturnFalseAndNegativeOne_WhenColumnNameNotFound()
        {
            // Arrange
            var loader = new MockDataLoader { Headers = new[] { "Column1", "Column2" } };
            var dataset = new Dataset(loader);

            // Act
            var (success, columnId) = dataset.ColumnNameToId("NonExistentColumn");

            // Assert
            Assert.False(success);
            Assert.Equal(-1, columnId);
        }

        [Fact]
        public void ColumnNameToId_ShouldReturnTrueAndProperId_WhenColumnNameFound()
        {
            string[] headers = new[] { "Column1", "Column2" };
            var loader = new MockDataLoader { Headers = headers };
            var dataset = new Dataset(loader);
            dataset.Initialize();

            int index = 0;
            var (success, columnId) = dataset.ColumnNameToId(headers[index]);
            Assert.True(success);
            Assert.Equal(index, columnId);

            index = 1;
            (success, columnId) = dataset.ColumnNameToId(headers[index]);
            Assert.True(success);
            Assert.Equal(index, columnId);
        }

        [Fact]
        public void ColumnIdToName_ShouldReturnFalseAndEmptyString_WhenColumnIdInvalid()
        {
            // Arrange
            var loader = new MockDataLoader { Headers = new[] { "Column1", "Column2" } };
            var dataset = new Dataset(loader);

            // Act
            var (success, columnName) = dataset.ColumnIdToName(5);

            // Assert
            Assert.False(success);
            Assert.Equal(string.Empty, columnName);
        }

        [Fact]
        public void GetColumnType_ShouldReturnFalseAndUnknown_WhenColumnIdInvalid()
        {
            // Arrange
            var loader = new MockDataLoader { ColumnTypes = new[] { ColumnType.INTEGER, ColumnType.STRING } };
            var dataset = new Dataset(loader);

            // Act
            var (success, columnType) = dataset.GetColumnType(5);

            // Assert
            Assert.False(success);
            Assert.Equal(ColumnType.UNKNOWN, columnType);
        }

        //[Fact]
        //public void GetColumnData_ShouldReturnFalseAndEmptyList_WhenColumnIdInvalid()
        //{
        //    // Arrange
        //    var loader = new MockDataLoader();
        //    var dataset = new Dataset(loader);

        //    // Act
        //    var (success, columnData) = dataset.GetColumnData(5);

        //    // Assert
        //    Assert.False(success);
        //    Assert.Empty(columnData);
        //}
    }
}
