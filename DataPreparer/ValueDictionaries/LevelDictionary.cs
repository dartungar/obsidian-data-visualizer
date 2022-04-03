namespace DataProcessor
{

    internal class LevelDictionary : ValueDictionaryBase
    {
        private static Dictionary<string, string> valueDict
            = new Dictionary<string, string>()
                {
                    {"none", "0" },
                    {"very low", "1" },
                    {"low", "2" },
                    {"medium", "3" },
                    {"high", "4" },
                    {"very high", "5" },
                };

        /// <summary>
        /// Common mapping: very low - very high to 1 - 5
        /// </summary>
        public LevelDictionary() : base(typeof(int), valueDict, ValueDictionaryType.Universal) { }

    }
}
