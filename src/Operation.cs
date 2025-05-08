namespace DataExplorer
{
    public class Operation
    {
        public enum Type : byte
        {
            AVG = 0,
            MAX = 1,
            MIN = 2,
            UNKNOW = 3
        }

        public static string[] Allowed() { return ["avg", "min", "max"]; }
    }
}
