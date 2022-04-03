using Common;

namespace DataProcessor
{
    /// <summary>
    /// Custom value dictionary. Applies only to specified fields
    /// </summary>
    internal class ValueDictionaryCustom : ValueDictionaryBase
    {
        private string[] _fields;
        public ValueDictionaryCustom(Type valueType, Dictionary<string, string> valueDict, string[] fields) : base(valueType, valueDict)
        {
            _fields = fields;
        }

        public ValueDictionaryCustom(Type valueType, Dictionary<string, string> valueDict, string fieldName) : base(valueType, valueDict)
        {
            _fields = new string[] { fieldName };
        }

        public override DataPoint MapDataPoint(DataPoint point)
        {
            if (!_fields.Contains(point.Name)) return point;
            return base.MapDataPoint(point);
        }
    }
}
