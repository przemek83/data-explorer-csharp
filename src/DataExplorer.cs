using System.CommandLine.Parsing;
using System.IO;

namespace DataExplorer
{
    internal class Program
    {
        static void Main(string[] appParams)
        {
            ArgsParser parser = new(appParams, Operation.Allowed());

            if (!parser.IsValid())
                return;

            var (dataset, query) = PrepareDatasetAndQuery(parser);
            if (dataset == null)
                return;

            Console.WriteLine($"Group {query.GroupingColumnID}, aggr {query.AggregateColumnID}, type {query.Type}");

            Calculator calculator = new(dataset);
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
            FileDataLoader loader = new FileDataLoader(stream);
            Dataset dataset = new Dataset(loader);

            if (!dataset.Initialize())
            {
                Console.WriteLine($"Failed to initialize dataset from {filePath}.");
                return null;
            }

            return dataset;
        }

        internal static (Dataset?, Query) PrepareDatasetAndQuery(ArgsParser parser)
        {
            Query query = new Query();
            var (filePath, operation, aggregation, grouping) = parser.GetResults();
            Console.WriteLine($"Params: {filePath} {operation} {aggregation} {grouping} ");

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

            query = new(aggregationColumnId, groupingColumnId, Operation.ToType(operation));

            return (dataset, query);
        }
    }
}
