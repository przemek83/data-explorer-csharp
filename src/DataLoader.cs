using System.ComponentModel.DataAnnotations.Schema;

namespace DataExplorer
{
    public interface IDataLoader
    {
        public abstract bool Load();

        public abstract string[] GetHeaders();

        public abstract ColumnType[] GetColumnTypes();

        public abstract IColumn[] GetData();
    }
}
