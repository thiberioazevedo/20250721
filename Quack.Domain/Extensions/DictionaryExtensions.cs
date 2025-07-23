namespace Quack.Domain.Extensions
{
    namespace Quack.Domain.Extensions
    {
        public static class DictionaryExtensions
        {
            public static string GetValueOrDefault(this Dictionary<string, string> dict, string key, string defaultValue)
            {
                if (dict.TryGetValue(key, out var value) && value is string str)
                    return str;

                return defaultValue;
            }
        }
    }
}
