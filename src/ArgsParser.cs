using System.CommandLine;
using System.CommandLine.Parsing;

namespace DataExplorer
{
    public class ArgsParser
    {
        public ArgsParser(string[] appParams, string[] operations)
        {
            appParams_ = appParams;
            command_ = new RootCommand("Small tool for aggregating and grouping data.");
            foreach (Argument argument in arguments_)
                command_.AddArgument(argument);

            command_.AddValidator(result =>
            {
                string operation = result.GetValueForArgument<string>((Argument<string>)arguments_[1]);
                if (string.IsNullOrEmpty(operation))
                {
                    result.ErrorMessage = "Operation is required.";
                    return;
                }

                if (!operations.Contains(operation.ToLower()))
                    result.ErrorMessage = $"Invalid operation: {operation}. Allowed operations are: {string.Join(", ", operations)}.";
            });
        }

        public bool IsValid()
        {
            return command_.Invoke(appParams_) == 0;
        }

        public (string file, string operation, string aggregation, string grouping) GetResults()
        {
            ParseResult result = command_.Parse(appParams_);
            string file = result.GetValueForArgument<string>((Argument<string>)arguments_[0]);
            string operation = result.GetValueForArgument<string>((Argument<string>)arguments_[1]);
            string aggregation = result.GetValueForArgument<string>((Argument<string>)arguments_[2]);
            string grouping = result.GetValueForArgument<string>((Argument<string>)arguments_[3]);

            return (file, operation, aggregation, grouping);
        }


        private readonly Command command_;
        private readonly string[] appParams_;
        private readonly Argument<string>[] arguments_ =
        [
            new ("file", "Input file"),
            new ("operation", "Arithmetic operation to perform"),
            new ("aggregation", "Aggregation column (numerical only)"),
            new ("grouping", "Grouping by column")
        ];
    }
}
