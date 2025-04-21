namespace DataExplorer
{
    public class FileDataLoader : IDataLoader
    {
        public FileDataLoader(Stream stream)
        {
            stream_ = stream;
        }

        public ColumnType[] GetColumnTypes()
        {
            throw new NotImplementedException();
        }

        public IColumn[] GetData()
        {
            throw new NotImplementedException();
        }

        public string[] GetHeaders()
        {
            throw new NotImplementedException();
        }

        public bool Load()
        {
            throw new NotImplementedException();
        }

        private readonly Stream stream_;
    }
}
