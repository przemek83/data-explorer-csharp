namespace DataExplorer
{
    public class FileDataLoader(Stream stream) : IDataLoader
    {
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
            var reader = new StreamReader(stream_);
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
                string[] data = line.Split(';');
                if (data.Length != headers_.Length)
                {
                    Console.WriteLine($"Data mismach in line {lineNumber}. Expected {headers_.Length} values, got {data.Length}.");
                    return false;
                }

                ProcessData(data, lineNumber);
                ++lineNumber;
            }
            return true;
        }

        private void ProcessData(string[] data, int lineNumber)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (columns_[i].GetColumnType() == ColumnType.INTEGER)
                {
                    if (int.TryParse(data[i], out int value))
                        ((ColumnNumeric)columns_[i]).AddData(value);
                    else
                        Console.WriteLine($"Cannot parse value in line {lineNumber}, column {i} to int. Value is {data[i]}.");
                }
                else if (columns_[i].GetColumnType() == ColumnType.STRING)
                {
                    ((ColumnString)columns_[i]).AddData(data[i]);
                }
            }
        }

        private readonly Stream stream_ = stream;
        private string[] headers_ = [];
        private IColumn[] columns_ = [];
    }
}
