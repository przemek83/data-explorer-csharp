namespace DataExplorer
{
    public enum ColumnType
    {
        INTEGER,
        STRING,
        UNKNOWN
    }

    public static class ColumnTypeExtensions
    {
        public static string ColumnTypeToString(ColumnType type)
        {
            return type switch
            {
                ColumnType.INTEGER => "INTEGER",
                ColumnType.STRING => "STRING",
                _ => "",
            };
        }

        public static ColumnType StringToColumnType(string type)
        {
            return type.ToUpper() switch
            {
                "INTEGER" => ColumnType.INTEGER,
                "STRING" => ColumnType.STRING,
                _ => ColumnType.UNKNOWN,
            };
        }
    }
}
