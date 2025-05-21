namespace DataExplorer
{
    public class ColumnString : IColumn
    {
        public ColumnString()
        {
        }
        public ColumnType GetColumnType()
        {
            return ColumnType.STRING;
        }
        public int GetSize()
        {
            return data_.Count;
        }
        public void AddData(string value)
        {
            data_.Add(value);
        }
        public string GetData(int index)
        {
            return data_[index];
        }

        private readonly List<string> data_ = [];
    }
}
