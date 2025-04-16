using System;


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
            Calculator calc = new Calculator();
            double result = calc.Add(2, 3);
            Console.WriteLine($"The result is: {result}");
        }
    }
}
