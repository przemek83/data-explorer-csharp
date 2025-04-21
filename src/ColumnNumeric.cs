namespace DataExplorer
{
    public  class ColumnNumeric : IColumn
    {
        public ColumnNumeric()
        {
            throw new NotImplementedException();
        }
        public ColumnType GetColumnType()
        {
            return ColumnType.INTEGER;
        }
        public int GetSize()
        {
            throw new NotImplementedException();
        }
        public void AddData(double value)
        {
            throw new NotImplementedException();
        }
        public double GetData(int index)
        {
            throw new NotImplementedException();
        }
    }
}
