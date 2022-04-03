using Common;

namespace DataProcessor
{
    internal static class ValueDictionaryExtensions
    {
        /// <summary>
        /// Apply all value dictionaries (mappings) of specified type to a collection of DataPoints
        /// </summary>
        /// <param name="dicts"></param>
        /// <param name="rawData"></param>
        /// <param name="dictType"></param>
        /// <returns></returns>
        public static IEnumerable<DataPoint> MapValues(this IEnumerable<IValueDictionary> dicts,
            IEnumerable<DataPoint> rawData,
            ValueDictionaryType dictType)
        {
            var mappedValues = rawData;
            foreach (var dict in dicts.Where(d => d.DictType == dictType)) 
                mappedValues = dict.MapDataPoints(mappedValues);
            return mappedValues;
        }
    }
}
