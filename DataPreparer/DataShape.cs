using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessor
{
    public struct FieldShape
    {
        public string Name { get; init; }
        public Type ValueType { get; init; }
    }
    
    public struct DataShape
    {
        public IEnumerable<FieldShape> Fields { get; init; }

        public Tuple<DateOnly, DateOnly> DateRange { get; init; }
    }
}
