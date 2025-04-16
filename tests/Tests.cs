using DataExplorer;

namespace DataExplorerUnitTests
{
  public class CalculatorUnitTests
  {
    [Fact]
    public void TestAdding2And2()
    {
      double a = 2;
      double b = 2;
      double expected = 4;
      Calculator calc = new();
      double actual = calc.Add(a, b);
      
      Assert.Equal(expected, actual);
    }
  }
}