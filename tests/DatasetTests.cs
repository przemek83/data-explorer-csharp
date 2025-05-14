namespace DataExplorer.Tests
{
    public class DatasetTests
    {
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
            var loader = new MockDataLoader(headers: new[] { "Column1", "Column2" }, [], []);
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
            var loader = new MockDataLoader(headers: headers, [], []);
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
            var loader = new MockDataLoader(headers: new[] { "Column1", "Column2" }, [], []);
            var dataset = new Dataset(loader);

            // Act
            var (success, columnName) = dataset.ColumnIdToName(5);

            // Assert
            Assert.False(success);
            Assert.Equal(string.Empty, columnName);
        }

        [Theory]
        [InlineData(0, ColumnType.INTEGER)]
        [InlineData(1, ColumnType.STRING)]
        public void GetColumnType_ShouldReturnCorrectType_WhenColumnIdIsValid(int columnId, ColumnType expectedType)
        {
            // Arrange
            var loader = new MockDataLoader([], columnTypes: new[] { ColumnType.INTEGER, ColumnType.STRING }, []);
            var dataset = new Dataset(loader);
            dataset.Initialize();

            // Act
            var (found, columnType) = dataset.GetColumnType(columnId);

            // Assert
            Assert.True(found);
            Assert.Equal(expectedType, columnType);
        }

        [Fact]
        public void GetColumnType_ShouldReturnUnknown_WhenColumnIdIsInvalid()
        {
            var loader = new MockDataLoader([], columnTypes: new[] { ColumnType.INTEGER, ColumnType.STRING }, []);
            var dataset = new Dataset(loader);
            dataset.Initialize();

            var (found, columnType) = dataset.GetColumnType(2);

            Assert.False(found);
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
