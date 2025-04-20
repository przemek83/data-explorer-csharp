using System.CommandLine;
using System.CommandLine.Parsing;

namespace DataExplorer
{
    public class ArgsParser
    {
        public ArgsParser(string[] args)
        { 
            args_ = args;
            command_ = new RootCommand("Small tool for aggregating and grouping data.");
            foreach (Argument argument in arguments_)
                command_.AddArgument(argument);
        }

        public bool IsValid()
        {
            return command_.Invoke(args_) == 0;
        }

        public (string file, string operation, string aggregation, string grouping) GetResults()
        {
            ParseResult result = command_.Parse(args_);
            string file = result.GetValueForArgument<string>((Argument<string>)arguments_[0]);
            string operation = result.GetValueForArgument<string>((Argument<string>)arguments_[1]);
            string aggregation = result.GetValueForArgument<string>((Argument<string>)arguments_[2]);
            string grouping = result.GetValueForArgument<string>((Argument<string>)arguments_[3]);

            return (file, operation, aggregation, grouping);
        }


        private readonly Command command_;
        private readonly string[] args_;
        private readonly Argument<string>[] arguments_ =
        [
            new ("file", "Input file"), 
            new ("operation", "OArithmetic operation to perform"),
            new ("aggregation", "Aggregation column (numerical only)"),
            new ("grouping", "Grouping by column")
        ];
    }
}
