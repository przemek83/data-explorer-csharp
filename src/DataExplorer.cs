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
            var rootCommand = new RootCommand("Small tool for aggregating and grouping data.");
            rootCommand.AddArgument(new Argument<string>("file", "Input file"));
            rootCommand.AddArgument(new Argument<string>("operation", "OArithmetic operation to perform"));
            rootCommand.AddArgument(new Argument<string>("aggregation", "Aggregation column (numerical only)"));
            rootCommand.AddArgument(new Argument<string>("grouping", "Grouping by column"));

            rootCommand.Invoke(args);

            Calculator calc = new Calculator();
            double result = calc.Add(2, 3);
            Console.WriteLine($"The result is: {result}");
        }
    }
}
