namespace DataExplorer
{
    public class Dataset
    {
        public Dataset(IDataLoader loader)
        {
            loader_ = loader;
        }

        public bool Initialize()
        {
            return loader_.Load();
        }

        public (bool, int) ColumnNameToId(string columnName)
        {
            return (false, -1);
        }

        public (bool, string) ColumnIdToName(int columnId)
        {
            return (false, "");
        }

        public (bool, ColumnType) GetColumnType(int columnId)
        {
            return (false, ColumnType.UNKNOWN);
        }

        public (bool, List<dynamic>) GetColumnData(int columnId)
        {
            return (false, new List<dynamic>());
        }

        readonly private string[] headers_;
        readonly private ColumnType[] columnTypes_;
        readonly private string[,] data_;
        readonly private IDataLoader loader_;
    }
}
