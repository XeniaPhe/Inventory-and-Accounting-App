namespace Xenia.IaA.AppDomain.Extensions;
internal static class DictionaryExtensions
{
    internal static TValue? GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key) where TValue : class
    {
        return dictionary.ContainsKey(key) ? dictionary[key] : default;
    }

    internal static TValue? GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, TValue? defaultValue)
    {
       return dictionary.ContainsKey(key) ? dictionary[key] : defaultValue;
    }
}