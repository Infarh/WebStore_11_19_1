// ReSharper disable once CheckNamespace
namespace System.Collections.Generic
{
    internal static class KeyValuePairExtensions
    {
        public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> Pair, out TKey Key, out TValue Value)
        {
            Key = Pair.Key;
            Value = Pair.Value;
        }
    }
}
