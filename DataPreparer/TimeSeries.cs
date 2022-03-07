using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessor
{
    [Serializable] // TODO: это нужно?
    public struct TimeSeries
    {
        public string Name { get; init; }
        public Dictionary<string, string[]> Entries { get; init; }

    }
}
