namespace DataExplorer
{
    public class Calculator(Dataset dataset)
    {
        public Dictionary<string, int> Execute(Query query)
        {
            ColumnNumeric aggregateColumn = dataset.GetColumn(query.AggregateColumnID) as ColumnNumeric;
            ColumnString groupingColumn = dataset.GetColumn(query.GroupingColumnID) as ColumnString;

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

            var groupAverages = new Dictionary<string, int>();
            foreach (var groupKey in groupSums.Keys)
                groupAverages[groupKey] = groupSums[groupKey] / groupCounts[groupKey];

            return groupAverages;
        }

        private static Dictionary<string, int> ComputeExtremum(ColumnNumeric aggregateColumn, ColumnString groupingColumn, Operation.Type type)
        {
            var groupExtremums = new Dictionary<string, int>();
            for (int i = 0; i < groupingColumn.GetSize(); i++)
            {
                string groupKey = groupingColumn.GetData(i);
                int value = aggregateColumn.GetData(i);
                if (!groupExtremums.ContainsKey(groupKey))
                    groupExtremums[groupKey] = value;
                else
                    groupExtremums[groupKey] = type == Operation.Type.MIN ? Math.Min(groupExtremums[groupKey], value) : Math.Max(groupExtremums[groupKey], value);
            }
            return groupExtremums;
        }

        private readonly Dataset dataset_ = dataset;
    }
}
