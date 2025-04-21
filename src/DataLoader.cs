using System.ComponentModel.DataAnnotations.Schema;

namespace DataExplorer
{
    public interface IDataLoader
    {
        bool Load();

        string[] GetHeaders();

        ColumnType[] GetColumnTypes();

        IColumn[] GetData();
    }
}
