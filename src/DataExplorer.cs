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

            ParserResults parserResults = parser.GetResults();
            Console.WriteLine($"Execute: {parserResults.Operation.ToUpper()} {parserResults.Aggregation} GROUP BY {parserResults.Grouping}");

            Dataset? dataset = LoadData(parserResults.FilePath);
            if (dataset == null)
                return;

            var (ok, query) = PrepareQuery(parserResults);
            if (!ok)
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

        internal static (bool, Query) PrepareQuery(ParserResults results)
        {
            var query = new Query();
            Dataset? dataset = LoadData(results.FilePath);
            if (dataset == null)
                return (false, query);

            (bool ok, int aggregationColumnId) = dataset.ColumnNameToId(results.Aggregation);
            if (!ok)
            {
                Console.WriteLine($"Column name {results.Aggregation} is not valid.");
                return (false, query);
            }

            (ok, int groupingColumnId) = dataset.ColumnNameToId(results.Grouping);
            if (!ok)
            {
                Console.WriteLine($"Column name {results.Grouping} is not valid.");
                return (false, query);
            }

            if (dataset.GetColumnType(aggregationColumnId).Item2 != ColumnType.INTEGER)
            {
                Console.WriteLine($"Column {results.Aggregation} is not of type INTEGER.");
                return (false, query);
            }

            if (dataset.GetColumnType(groupingColumnId).Item2 != ColumnType.STRING)
            {
                Console.WriteLine($"Column {results.Grouping} is not of type STRING.");
                return (false, query);
            }

            query = new(aggregationColumnId, groupingColumnId, Operation.ToType(results.Operation));

            return (true, query);
        }
    }
}
