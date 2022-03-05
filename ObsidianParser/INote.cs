using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsidianParser
{
    internal interface IDailyNote: INote
    {
        public DateOnly Date { get; }
    }
    
    internal interface INote
    {
        public IEnumerable<DataPoint> Metadata { get; }
        public void ParseMetadata();
    }
}
