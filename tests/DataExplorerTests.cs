using DataExplorer;
using static DataExplorer.ArgsParser;

namespace Tests
{
    public class DataExplorerTests
    {
        [Fact]
        public void PrepareQuery_InvalidAggregationColumn_ReturnsFalse()
        {
            // Arrange
            var dataset = GetTestDataset();
            dataset.Initialize();
            var results = new ParserResults("file.csv", "AVG", "InvalidColumn", "GroupingColumn");

            // Act
            var (ok, query) = Program.PrepareQuery(dataset, results);

            // Assert
            Assert.False(ok);
        }

        [Fact]
        public void PrepareQuery_InvalidGroupingColumn_ReturnsFalse()
        {
            // Arrange
            var dataset = GetTestDataset();
            dataset.Initialize();
            var results = new ParserResults("file.csv", "AVG", "AggregationColumn", "InvalidColumn");

            // Act
            var (ok, query) = Program.PrepareQuery(dataset, results);

            // Assert
            Assert.False(ok);
        }

        [Fact]
        public void PrepareQuery_InvalidAggregationColumnType_ReturnsFalse()
        {
            // Arrange
            var dataset = GetTestDataset();
            dataset.Initialize();
            var results = new ParserResults("file.csv", "AVG", "StringColumn", "GroupingColumn");

            // Act
            var (ok, query) = Program.PrepareQuery(dataset, results);

            // Assert
            Assert.False(ok);
        }

        [Fact]
        public void PrepareQuery_InvalidGroupingColumnType_ReturnsFalse()
        {
            // Arrange
            var dataset = GetTestDataset();
            dataset.Initialize();
            var results = new ParserResults("file.csv", "AVG", "AggregationColumn", "IntegerColumn");

            // Act
            var (ok, query) = Program.PrepareQuery(dataset, results);

            // Assert
            Assert.False(ok);
        }

        [Fact]
        public void PrepareQuery_ValidColumns_ReturnsTrue()
        {
            // Arrange
            var dataset = GetTestDataset();
            dataset.Initialize();
            var results = new ParserResults("file.csv", "AVG", "AggregationColumn", "GroupingColumn");

            // Act
            var (ok, query) = Program.PrepareQuery(dataset, results);

            // Assert
            Assert.True(ok);
            Assert.Equal(0, query.AggregateColumnID);
            Assert.Equal(1, query.GroupingColumnID);
            Assert.Equal(Operation.Type.AVG, query.Type);
        }

        Dataset GetTestDataset()
        {
            return new Dataset(new MockDataLoader(headers: new[] { "AggregationColumn", "GroupingColumn", "StringColumn", "IntegerColumn" },
               new[] { ColumnType.INTEGER, ColumnType.STRING, ColumnType.STRING, ColumnType.INTEGER },
               []));
        }
    }
}
