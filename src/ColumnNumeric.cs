namespace DataExplorer
{
    public  class ColumnNumeric : IColumn
    {
        public ColumnNumeric()
        {
        }

        public ColumnType GetColumnType()
        {
            return ColumnType.INTEGER;
        }

        public int GetSize()
        {
            return data_.Count;
        }

        public void AddData(int value)
        {
            data_.Add(value);
        }

        public int GetData(int index)
        {
            return data_[index];
        }

        private readonly List<int> data_ = [];
    }
}
