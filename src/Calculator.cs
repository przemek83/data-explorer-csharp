namespace DataExplorer
{
    public class Calculator(Dataset dataset)
    {
        public Dictionary<string, int> Execute(Query query)
        {
            ColumnNumeric aggregateColumn = (ColumnNumeric)dataset_.GetColumn(query.AggregateColumnID);
            ColumnString groupingColumn = (ColumnString)dataset_.GetColumn(query.GroupingColumnID);

            switch (query.Type)
            {
                case Operation.Type.AVG:
                    return ComputeAvg(aggregateColumn, groupingColumn);
                case Operation.Type.MIN:
                    return ComputeExtremum(aggregateColumn, groupingColumn, Operation.Type.MIN);
                case Operation.Type.MAX:
                    return ComputeExtremum(aggregateColumn, groupingColumn, Operation.Type.MAX);
            }
            return [];
        }

        private static Dictionary<string, int> ComputeAvg(ColumnNumeric aggregateColumn, ColumnString groupingColumn)
        {
            var groupSums = new Dictionary<string, int>();
            var groupCounts = new Dictionary<string, int>();

            for (int i = 0; i < groupingColumn.GetSize(); i++)
            {
                string groupKey = groupingColumn.GetData(i);
                int value = aggregateColumn.GetData(i);

                if (!groupSums.ContainsKey(groupKey))
                {
                    groupSums[groupKey] = 0;
                    groupCounts[groupKey] = 0;
                }

                groupSums[groupKey] += value;
                groupCounts[groupKey]++;
            }

            var results = new Dictionary<string, int>();
            foreach (var groupKey in groupSums.Keys)
                results[groupKey] = groupSums[groupKey] / groupCounts[groupKey];

            return results;
        }

        private static Dictionary<string, int> ComputeExtremum(ColumnNumeric aggregateColumn,
            ColumnString groupingColumn,
            Operation.Type type)
        {
            var results = new Dictionary<string, int>();
            for (int i = 0; i < groupingColumn.GetSize(); i++)
            {
                string groupKey = groupingColumn.GetData(i);
                int newValue = aggregateColumn.GetData(i);
                if (!results.TryGetValue(groupKey, out int oldValue))
                    results[groupKey] = newValue;
                else
                    results[groupKey] =
                        (type == Operation.Type.MIN ? Math.Min(oldValue, newValue) : Math.Max(oldValue, newValue));
            }
            return results;
        }

        private readonly Dataset dataset_ = dataset;
    }
}
