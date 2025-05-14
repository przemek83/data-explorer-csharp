using DataExplorer;

namespace DataExplorer.Tests
{
    public class MockDataLoader : IDataLoader
    {
        public bool LoadResult { get;  set; } = true;
        private readonly string[] headers_ = [];
        private readonly ColumnType[] columnTypes_ = [];
        private readonly IColumn[] data_ = [];

        public MockDataLoader(string[] headers, ColumnType[] columnTypes, IColumn[] data)
        {
            headers_ = headers;
            columnTypes_ = columnTypes;
            data_ = data;
        }

        public MockDataLoader()
        {

        }

        public bool Load() => LoadResult;

        public string[] GetHeaders() => headers_;

        public ColumnType[] GetColumnTypes() => columnTypes_;

        public IColumn[] GetData() => data_;
    }
}
