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
            ColumnType[] types = new ColumnType[columns_.Length];
            for (int i = 0; i < types.Length; ++i)
                types[i] = columns_[i].GetColumnType();

            return types;
        }

        public IColumn[] GetData()
        {
            return columns_;
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

            headers_ = ProcessHeadersLine(reader.ReadLine() ?? "");

            if (reader.EndOfStream)
                return false;

            columns_ = ProcessColumnTypesLine(reader.ReadLine() ?? "");

            if (headers_.Length != columns_.Length ||
                columns_.Any(column => column.GetColumnType() == ColumnType.UNKNOWN))
                return false;

            return ProcessDataLines(reader);
        }

        private static string[] ProcessHeadersLine(string line)
        {
            string[] headers = line.Split(';');
            for (int i = 0; i < headers.Length; i++)
                headers[i] = headers[i].Trim();
            return headers;
        }

        private static IColumn[] ProcessColumnTypesLine(string line)
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

        private bool ProcessDataLines(StreamReader reader)
        {
            int lineNumber = 3;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine() ?? "";
                string[] values = line.Split(';');
                if (values.Length != headers_.Length)
                {
                    Console.WriteLine($"Data mismach in line {lineNumber}. Expected {headers_.Length} values, got {values.Length}.");
                    return false;
                }

                for (int i = 0; i < values.Length; i++)
                {
                    if (columns_[i].GetColumnType() == ColumnType.INTEGER)
                    {
                        if (int.TryParse(values[i], out int value))
                            ((ColumnNumeric)columns_[i]).AddData(value);
                        else
                            Console.WriteLine($"Cannot parse value in line {lineNumber}, column {i} to int. Value is {values[i]}.");
                    }
                    else if (columns_[i].GetColumnType() == ColumnType.STRING)
                    {
                        ((ColumnString)columns_[i]).AddData(values[i]);
                    }
                }
                ++lineNumber;
            }
            return true;
        }

        private readonly Stream stream_;
        private string[] headers_ = [];
        private IColumn[] columns_ = [];
    }
}
