using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataExplorer
{
    public class ColumnString : IColumn
    {
        public ColumnString()
        {
            throw new NotImplementedException();
        }
        public ColumnType GetColumnType()
        {
            return ColumnType.STRING;
        }
        public int GetSize()
        {
            throw new NotImplementedException();
        }
        public void AddData(string value)
        {
            throw new NotImplementedException();
        }
        public string GetData(int index)
        {
            throw new NotImplementedException();
        }
    }
}
