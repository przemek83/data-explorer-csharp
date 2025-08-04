using DataExplorer;

namespace Tests
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
        [InlineData(Operation.Type.AVG, new[] { "tim", "tamas", "dave" }, new[] { 8, 5, 8 }, 3, 0)]
        [InlineData(Operation.Type.MIN, new[] { "tim", "tamas", "dave" }, new[] { 8, 4, 8 }, 3, 0)]
        [InlineData(Operation.Type.MAX, new[] { "tim", "tamas", "dave" }, new[] { 8, 7, 8 }, 3, 0)]
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
                        CreateStringColumn([ "tim", "tim", "tamas", "tamas", "dave", "dave" ]),
                        CreateNumericColumn([26, 26, 44, 44, 0, 0 ]),
                        CreateStringColumn([ "inception", "pulp_fiction", "inception", "pulp_fiction", "inception", "ender's_game" ]),
                        CreateNumericColumn([ 8, 8, 7, 4, 8, 8 ])
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

        private ColumnString CreateStringColumn(string[] data)
        {
            var column = new ColumnString();
            foreach (var item in data)
                column.AddData(item);
            return column;
        }

        private ColumnNumeric CreateNumericColumn(int[] data)
        {
            var column = new ColumnNumeric();
            foreach (var item in data)
                column.AddData(item);
            return column;
        }
    }
}
