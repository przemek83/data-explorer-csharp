using System.CommandLine.Parsing;
using System.IO;

namespace DataExplorer
{
    internal class Program
    {
        static void Main(string[] appParams)
        {
            var parser = new ArgsParser(appParams, Operation.Allowed());

            if (!parser.IsValid() || parser.ShouldExit())
                return;

            var (dataset, query) = PrepareDatasetAndQuery(parser);
            if (dataset == null)
                return;

            var calculator = new Calculator(dataset);
            Dictionary<string, int> results = calculator.Execute(query);
            foreach (var result in results)
                Console.WriteLine($"{result.Key}: {result.Value}");
        }

        internal static Dataset? LoadData(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File {filePath} does not exist.");
                return null;
            }

            FileStream stream = File.OpenRead(filePath);
            var loader = new FileDataLoader(stream);
            var dataset = new Dataset(loader);

            if (!dataset.Initialize())
            {
                Console.WriteLine($"Failed to initialize dataset from {filePath}.");
                return null;
            }

            return dataset;
        }

        internal static (Dataset?, Query) PrepareDatasetAndQuery(ArgsParser parser)
        {
            var query = new Query();
            var (filePath, operation, aggregation, grouping) = parser.GetResults();
            Console.WriteLine($"Execute: {operation.ToUpper()} {aggregation} GROUP BY {grouping}");

            Dataset? dataset = LoadData(filePath);
            if (dataset == null)
                return (null, query);

            (bool ok, int aggregationColumnId) = dataset.ColumnNameToId(aggregation);
            if (!ok)
            {
                Console.WriteLine($"Column name {aggregation} is not valid.");
                return (null, query);
            }

            (ok, int groupingColumnId) = dataset.ColumnNameToId(grouping);
            if (!ok)
            {
                Console.WriteLine($"Column name {grouping} is not valid.");
                return (null, query);
            }

            if (dataset.GetColumnType(aggregationColumnId).Item2 != ColumnType.INTEGER)
            {
                Console.WriteLine($"Column {aggregation} is not of type INTEGER.");
                return (null, query);
            }

            if (dataset.GetColumnType(groupingColumnId).Item2 != ColumnType.STRING)
            {
                Console.WriteLine($"Column {grouping} is not of type STRING.");
                return (null, query);
            }

            query = new(aggregationColumnId, groupingColumnId, Operation.ToType(operation));

            return (dataset, query);
        }
    }
}
