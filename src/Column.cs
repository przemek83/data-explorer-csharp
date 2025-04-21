namespace DataExplorer
{
    public interface IColumn
    {
        public ColumnType GetColumnType();

        public int GetSize();
    }
}
