namespace DataExplorer
{
    public class Calculator(Dataset dataset)
    {
        public Dictionary<string, int> Execute(Query query)
        {
            return [];
        }

        private readonly Dataset dataset_ = dataset;
    }
}
