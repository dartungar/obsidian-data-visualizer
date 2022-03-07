using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessor
{
    public interface IDataProvider
    {
        public void ReadData();
        public IEnumerable<DataPoint> GetData();

    }
}
