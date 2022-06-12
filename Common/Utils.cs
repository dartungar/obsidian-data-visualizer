using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Utils
    {
        // Get non-null values from collection
        public static IEnumerable<DataSeries> NotNull(this IEnumerable<DataSeries?> ds) =>
            ds.Where(ds => ds != null).Select(ds => ds.Value);
    }
}
