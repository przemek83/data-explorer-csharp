namespace DataExplorer
{
    public readonly struct Query(int aggregateColumnID, int groupingColumnID, Operation.Type type)
    {
        public readonly int AggregateColumnID { get; } = aggregateColumnID;
        public readonly int GroupingColumnID { get; } = groupingColumnID;
        public readonly Operation.Type Type { get; } = type;
    }
}
