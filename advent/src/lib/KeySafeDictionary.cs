using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent
{
    /// <summary>
    /// Supports accessing non-existent keys.
    /// </summary>
    public class KeySafeDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public new TValue this[TKey key]
        {
            get => this[key, default(TValue)];
            set => base[key] = value;
        }

        public TValue this[TKey key, TValue defaultValue]
        {
            get { TValue value = TryGetValue(key, out value) ? value : defaultValue; return value; }
            set => base[key] = value;
        }

        public override string ToString()
        {
            return $"{{{string.Join(", ", this.Select(pair => $"'{pair.Key}':'{pair.Value}'"))}}}";
        }
    }
}
