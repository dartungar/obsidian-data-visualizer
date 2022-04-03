namespace DataProcessor
{
    internal class BooleanDictionary : ValueDictionaryBase
    {
        /// <summary>
        /// Common mapping: yes-no to true-false
        /// </summary>
        public BooleanDictionary() : base(
            typeof(bool), new Dictionary<string, string> {
                { "yes", "true" },
                { "no", "false" }
            }, ValueDictionaryType.Universal)
        { }
    }
}
