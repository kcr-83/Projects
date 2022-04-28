
using System;

namespace PowerFP
{
    public record Map<K, V>
        where K : notnull
    {
        public LList<(K Key, V Value)>? Items { get; }

        internal Map(LList<(K Key, V Value)>? items) => Items = items;
    }


    public static class MapM
    {
        public static Map<K, V> MapFrom<K, V>(LList<(K Key, V Value)>? items) where K : notnull =>
            new Map<K, V>(items.Aggregate((LList<(K, V)>?)null, (m, kv) => AddToSet(m, kv.Key, kv.Value)));

        public static Map<K, V> Add<K, V>(this Map<K, V> map, K key, V value) where K : notnull
            => new(AddToSet(map.Items, key, value));

        public static (bool IsFound, V? Value) TryFind<K, V>(this Map<K, V> map, K key) where K : notnull =>
            TryFindFirst(map.Items, key);

        public static V Find<K, V>(this Map<K, V> map, K key) where K : notnull =>
            TryFind(map, key) is (true, var Value) ? Value! : throw new Exception($"Map does not contain '{key}' key");

        public static bool TryFind<K, V>(this Map<K, V> map, K key, out V value) where K : notnull
        {
            var (isFound, foundValue) = TryFindFirst(map.Items, key);
            value = (isFound ? foundValue : default)!;
            return isFound;
        }

        // private

        private static LList<(K, V)> AddToSet<K, V>(LList<(K, V)>? items, K newKey, V newValue) where K : notnull =>
            items switch
            {
                null => new((newKey, newValue), null),
                ((var Key, var Value) Head, var Tail) when newKey.Equals(Key) => new((newKey, newValue), Tail),
                (var Head, var Tail) => new(Head, AddToSet(Tail, newKey, newValue))
            };

        private static (bool IsFound, V? Value) TryFindFirst<K, V>(LList<(K, V)>? items, K key) where K : notnull =>
            items switch
            {
                null => (false, default(V)),
                ((var Key, var Value), var Tail) when key.Equals(Key) => (true, Value),
                (_, var Tail) => TryFindFirst(Tail, key)
            };
    }
}