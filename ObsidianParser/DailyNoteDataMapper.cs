using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProcessor;

namespace ObsidianParser
{
    internal class DailyNoteDataMapper : IDataValueMapper
    {
        public Dictionary<string, object> Mappings => new Dictionary<string, object>
        {
            {"yes", true },
            {"no", false },
            {"none", 0 },
            {"very low", 1 },
            {"low", 2 },
            {"medium", 3 },
            {"high", 4 },
            {"very high", 5 },
        };

        public ProcessedDataPoint MapRawDataPoint(RawDataPoint point)
        {
            return new ProcessedDataPoint 
            { 
                Name = point.Name, 
                Values = point.Values.Select(v => Mappings.ContainsKey(v) ? Mappings[v] : v) 
            };
        }


    }
}
