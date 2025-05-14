using System.Collections.Generic;
using Xunit;

namespace DataExplorer.Tests
{
    public class CalculatorTests
    {
        [Theory]
        [InlineData(Operation.Type.AVG, new[] { "inception", "pulp_fiction", "ender's_game" }, new[] { 7, 6, 8 }, 3, 2)]
        [InlineData(Operation.Type.MIN, new[] { "inception", "pulp_fiction", "ender's_game" }, new[] { 7, 4, 8 }, 3, 2)]
        [InlineData(Operation.Type.MAX, new[] { "inception", "pulp_fiction", "ender's_game" }, new[] { 8, 8, 8 }, 3, 2)]
        [InlineData(Operation.Type.AVG, new[] { "tim", "tamas", "dave" }, new[] { 26, 44, 0 }, 1, 0)]
        [InlineData(Operation.Type.MIN, new[] { "tim", "tamas", "dave" }, new[] { 26, 44, 0 }, 1, 0)]
        [InlineData(Operation.Type.MAX, new[] { "tim", "tamas", "dave" }, new[] { 26, 44, 0 }, 1, 0)]
        [InlineData(Operation.Type.AVG, new[] { "inception", "pulp_fiction", "ender's_game" }, new[] { 8, 4, 8 }, 3, 0)]
        [InlineData(Operation.Type.MIN, new[] { "inception", "pulp_fiction", "ender's_game" }, new[] { 8, 4, 8 }, 3, 0)]
        [InlineData(Operation.Type.MAX, new[] { "inception", "pulp_fiction", "ender's_game" }, new[] { 8, 4, 8 }, 3, 0)]
        public void Execute_ShouldGroupAndAggregateCorrectly(Operation.Type operationType, string[] keys, int[] values, int aggregateColumnID, int groupingColumnID)
        {
            // Arrange
            var calculator = CreateTestCalculator();
            var query = new Query(aggregateColumnID, groupingColumnID, operationType);

            // Act
            var result = calculator.Execute(query);

            // Assert
            var expected = CreateExpectedResult(keys, values);

            Assert.Equal(expected, result);
        }

        private Calculator CreateTestCalculator()
        {
            var dataLoader = new MockDataLoader(new[] { "first_name", "age", "movie_name", "score" },
                new ColumnType[] { ColumnType.STRING, ColumnType.INTEGER, ColumnType.STRING, ColumnType.INTEGER },
                new IColumn[]
                {
                        new MockColumn<string>(new[] { "tim", "tim", "tamas", "tamas", "dave", "dave" }),
                        new MockColumn<int>(new[] { 26, 26, 44, 44, 0, 0 }),
                        new MockColumn<string>(new[] { "inception", "pulp_fiction", "inception", "pulp_fiction", "inception", "ender's_game" }),
                        new MockColumn<int>(new[] { 8, 8, 7, 4, 8, 8 })
                });
            var dataset = new Dataset(dataLoader);
            dataset.Initialize();
            return new Calculator(dataset);
        }

        private Dictionary<string, int> CreateExpectedResult(string[] keys, int[] values)
        {
            var expected = new Dictionary<string, int>();
            for (int i = 0; i < keys.Length; i++)
                expected[keys[i]] = values[i];
            return expected;
        }
    }

    public class MockColumn<T> : IColumn
    {
        private readonly T[] data;

        public MockColumn(T[] data)
        {
            this.data = data;
        }

        public ColumnType GetColumnType()
        {
            return typeof(T) == typeof(int) ? ColumnType.INTEGER :
                   typeof(T) == typeof(string) ? ColumnType.STRING :
                   ColumnType.UNKNOWN;
        }

        public int GetSize()
        {
            return data.Length;
        }

        public T[] GetData()
        {
            return data;
        }
    }

    public class MockDataLoader : IDataLoader
    {
        private readonly string[] headers_;
        private readonly ColumnType[] columnTypes_;
        private readonly IColumn[] data_;

        public MockDataLoader(string[] headers, ColumnType[] columnTypes, IColumn[] data)
        {
            headers_ = headers;
            columnTypes_ = columnTypes;
            data_ = data;
        }

        public bool Load() => true;

        public string[] GetHeaders() => headers_;

        public ColumnType[] GetColumnTypes() => columnTypes_;

        public IColumn[] GetData() => data_;
    }
}
