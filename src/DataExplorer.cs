namespace DataExplorer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ArgsParser parser = new(args, Operation.Allowed());

            if (!parser.IsValid())
                return;

            var (file, operation, aggregation, grouping) = parser.GetResults();

            Console.WriteLine($"Params: {file} {operation} {aggregation} {grouping} ");
        }
    }
}
