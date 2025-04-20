namespace DataExplorer
{
    public class Dataset
    {
        public Dataset()
        {
        }

        public bool Initialize(string file)
        {
            return false;
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
    }
}
