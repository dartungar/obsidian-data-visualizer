using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessor
{
    public struct ProcessedDataPoint
    {
        public string Name { get; set; }
        public IEnumerable<object> Values { get; set; } // TODO: reflection or dynamic?
    }
}
