using System.CommandLine;

public class Calculator
{
  public double Add(double a, double b)
  {
    return a + b;
  }
}

namespace DataExplorer
{
    internal class Program
    {
        static void Main(string[] args)
        {
           ArgsParser parser = new ArgsParser(args);
            if (!parser.IsValid())
                return;

            var (file, operation, aggregation, grouping) = parser.GetResults();

            Console.WriteLine($"Params: {file} {operation} {aggregation} {grouping} ");

            Calculator calc = new Calculator();
            double result = calc.Add(2, 3);
            Console.WriteLine($"The result is: {result}");
        }
    }
}
