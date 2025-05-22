using System.CommandLine.Parsing;
using System.IO;
using static DataExplorer.ArgsParser;

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
            ParserResults results = parser.GetResults();
            Console.WriteLine($"Execute: {results.Operation.ToUpper()} {results.Aggregation} GROUP BY {results.Grouping}");

            Dataset? dataset = LoadData(results.FilePath);
            if (dataset == null)
                return (null, query);

            (bool ok, int aggregationColumnId) = dataset.ColumnNameToId(results.Aggregation);
            if (!ok)
            {
                Console.WriteLine($"Column name {results.Aggregation} is not valid.");
                return (null, query);
            }

            (ok, int groupingColumnId) = dataset.ColumnNameToId(results.Grouping);
            if (!ok)
            {
                Console.WriteLine($"Column name {results.Grouping} is not valid.");
                return (null, query);
            }

            if (dataset.GetColumnType(aggregationColumnId).Item2 != ColumnType.INTEGER)
            {
                Console.WriteLine($"Column {results.Aggregation} is not of type INTEGER.");
                return (null, query);
            }

            if (dataset.GetColumnType(groupingColumnId).Item2 != ColumnType.STRING)
            {
                Console.WriteLine($"Column {results.Grouping} is not of type STRING.");
                return (null, query);
            }

            query = new(aggregationColumnId, groupingColumnId, Operation.ToType(results.Operation));

            return (dataset, query);
        }
    }
}
