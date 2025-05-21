namespace DataExplorer
{
    public class Dataset(IDataLoader loader)
    {
        public bool Initialize()
        {
            bool success = loader_.Load();
            if (!success)
                return false;
            headers_ = loader_.GetHeaders();
            columnTypes_ = loader_.GetColumnTypes();

            return true;
        }

        public (bool, int) ColumnNameToId(string columnName)
        {
            int index = Array.IndexOf(headers_, columnName);
            if (index == -1)
                return (false, index);
            return (true, index);
        }

        public (bool, ColumnType) GetColumnType(int columnId)
        {
            if (columnId >= 0 && columnId < columnTypes_.Length)
                return (true, columnTypes_[columnId]);

            return (false, ColumnType.UNKNOWN);
        }

        public IColumn GetColumn(int columnId)
        {
            return loader_.GetData()[columnId];
        }

        private string[] headers_ = [];
        private ColumnType[] columnTypes_ = [];
        readonly private IDataLoader loader_ = loader;
    }
}
