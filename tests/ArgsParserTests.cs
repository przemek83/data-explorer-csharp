using DataExplorer;
using static DataExplorer.ArgsParser;

namespace Tests
{
    public class ArgsParserTests : IClassFixture<ConsoleSuppressor>
    {
        [Fact]
        public void IsValid_ShouldReturnTrue_WhenArgumentsAreValid()
        {
            string[] args = ["sample.txt", "avg", "score", "first_name"];
            var parser = new ArgsParser(args, Operation.Allowed());

            var result = parser.IsValid();

            Assert.True(result);
        }

        [Fact]
        public void IsValid_ShouldReturnFalse_WhenOperationIsInvalid()
        {
            string[] args = ["sample.txt", "invalid", "score", "first_name"];
            var parser = new ArgsParser(args, Operation.Allowed());

            bool result = parser.IsValid();

            Assert.False(result);
        }

        [Fact]
        public void IsValid_ShouldReturnFalse_WhenNotEnoughArgsPassed()
        {
            string[] args = ["sample.txt", "invalid", "score"];
            var parser = new ArgsParser(args, Operation.Allowed());

            bool result = parser.IsValid();

            Assert.False(result);
        }

        [Fact]
        public void IsValid_ShouldReturnFalse_WhenTooManyArgsPassed()
        {
            string[] args = ["sample.txt", "avg", "score", "first_name", "excesive"];
            var parser = new ArgsParser(args, Operation.Allowed());

            bool result = parser.IsValid();

            Assert.False(result);
        }

        [Fact]
        public void GetResults_ShouldReturnCorrectParsedValues_WhenArgumentsAreValid()
        {
            string[] args = ["sample.txt", "sum", "score", "first_name"];
            var parser = new ArgsParser(args, Operation.Allowed());

            ParserResults results = parser.GetResults();

            Assert.Equal("sample.txt", results.FilePath);
            Assert.Equal("sum", results.Operation);
            Assert.Equal("score", results.Aggregation);
            Assert.Equal("first_name", results.Grouping);
        }

        [Fact]
        public void ShouldExit_ReturnsFalse_WhenNoSpecialOptionIsPresent()
        {
            var args = new[] { "file.csv", "avg", "score", "group" };
            var parser = new ArgsParser(args, ["avg", "sum", "min", "max"]);
            Assert.False(parser.ShouldExit());
        }
    }
}
