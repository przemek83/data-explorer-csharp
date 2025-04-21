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
            return headers_;
        }

        public bool Load()
        {
            StreamReader reader = new StreamReader(stream_);
            if (reader.EndOfStream)
                return false;

            headers_ = processHeadersLine(reader.ReadLine() ?? "");
            
            if (reader.EndOfStream)
                return false;

            //columns_ = processColumnTypesLine(reader.ReadLine() ?? "");

            return true;
        }

        private string[] processHeadersLine(string line)
        {
            string[] headers = line.Split(';');
            for (int i = 0; i < headers.Length; i++)
                headers[i] = headers[i].Trim();
            return headers;
        }

        private IColumn[] processColumnTypesLine(string line)
        {
            string[] columnTypeNames = line.Split(';');
            IColumn[] columns = new IColumn[columnTypeNames.Length];
            for (int i = 0; i < columnTypeNames.Length; i++)
            {
                ColumnType type = ColumnTypeExtensions.StringToColumnType(columnTypeNames[i]);
                columns[i] = type switch
                {
                    ColumnType.INTEGER => new ColumnNumeric(),
                    ColumnType.STRING => new ColumnString(),
                    _ => throw new NotImplementedException(),
                };
            }

            return columns;
        }

        private readonly Stream stream_;
        private string[] headers_;
        private IColumn[] columns_;
    }
}
