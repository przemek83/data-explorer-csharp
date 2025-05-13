using System.CommandLine.Parsing;

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

        public static Type ToType(string name)
        {
            return name.ToLower() switch
            {
                "avg" => Type.AVG,
                "min" => Type.MIN,
                "max" => Type.MAX,
                _ => Type.UNKNOW,
            };
        }
    }
}
