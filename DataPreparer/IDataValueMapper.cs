using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessor
{
    /// <summary>
    /// Mapping used in processing raw data
    /// (i.e map "no" -> false, or "very low - low - high - ..." -> "0 - 1 - 2 - ...")
    /// </summary>
    public interface IDataValueMapper
    {
        public Dictionary<string, object> Mappings { get; } // TODO: dynamic/reflection for values?..

        public ProcessedDataPoint MapRawDataPoint(RawDataPoint rawDataPoint);
    }
}
