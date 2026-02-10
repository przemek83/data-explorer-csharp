namespace Tests
{
    public class ConsoleSuppressor : IDisposable
    {
        private readonly TextWriter originalStdOutput;
        private readonly StringWriter suppressedStdOutput;
        private readonly TextWriter originalErrorOutput;
        private readonly StringWriter suppressedErrorOutput;

        public ConsoleSuppressor()
        {
            originalStdOutput = Console.Out;
            suppressedStdOutput = new StringWriter();
            Console.SetOut(suppressedStdOutput);

            originalErrorOutput = Console.Error;
            suppressedErrorOutput = new StringWriter();
            Console.SetError(suppressedErrorOutput);
        }

        public void Dispose()
        {
            Console.SetOut(originalStdOutput);
            suppressedStdOutput.Dispose();
            Console.SetError(originalErrorOutput);
            suppressedErrorOutput.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
