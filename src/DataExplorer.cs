using System.IO;

namespace DataExplorer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ArgsParser parser = new(args, Operation.Allowed());

            if (!parser.IsValid())
                return;

            var (filePath, operation, aggregation, grouping) = parser.GetResults();

            var (ok, dataset) = LoadData(filePath);
            if (!ok)
                return;

            Console.WriteLine($"Params: {filePath} {operation} {aggregation} {grouping} ");
        }

        internal static (bool, Dataset?) LoadData(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File {filePath} does not exist.");
                return (false, null);
            }

            FileStream stream = File.OpenRead(filePath);
            FileDataLoader loader = new FileDataLoader(stream);
            Dataset dataset = new Dataset(loader);

            if (!dataset.Initialize())
            {
                Console.WriteLine($"Failed to initialize dataset from {filePath}.");
                return (false, null);
            }

            return (true, dataset);
        }
    }
}
