using System.CommandLine;
using System.CommandLine.Parsing;
using System.Runtime.Intrinsics.X86;
using Xunit;

namespace DataExplorer.Tests
{
    public class ArgsParserTests
    {
        [Fact]
        public void IsValid_ShouldReturnTrue_WhenArgumentsAreValid()
        {
            string[] args = new[] { "sample.txt", "avg", "score", "first_name" };
            ArgsParser parser = new ArgsParser(args, Operation.Allowed());

            var result = parser.IsValid();

            Assert.True(result);
        }

        [Fact]
        public void IsValid_ShouldReturnFalse_WhenOperationIsInvalid()
        {
            string[] args = new[] { "sample.txt", "invalid", "score", "first_name" };
            ArgsParser parser = new ArgsParser(args, Operation.Allowed());

            bool result = parser.IsValid();

            Assert.False(result);
        }

        [Fact]
        public void IsValid_ShouldReturnFalse_WhenNotEnoughArgsPassed()
        {
            string[] args = new[] { "sample.txt", "invalid", "score" };
            ArgsParser parser = new ArgsParser(args, Operation.Allowed());

            bool result = parser.IsValid();

            Assert.False(result);
        }

        [Fact]
        public void GetResults_ShouldReturnCorrectParsedValues_WhenArgumentsAreValid()
        {
            string[] args = new[] { "sample.txt", "sum", "score", "first_name" };
            ArgsParser parser = new ArgsParser(args, Operation.Allowed());

            var (file, operation, aggregation, grouping) = parser.GetResults();

            Assert.Equal("sample.txt", file);
            Assert.Equal("sum", operation);
            Assert.Equal("score", aggregation);
            Assert.Equal("first_name", grouping);
        }
    }
}
