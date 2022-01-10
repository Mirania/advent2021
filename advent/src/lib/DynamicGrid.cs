using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent
{
    /// <summary>
    /// Slow but supports any coordinates and up to 4 dimensions. Uses a low amount of memory.
    /// Should only be used when the inserted points matter but the space between them does not.
    /// </summary>
    public class DynamicGrid<TValue>
    {
        private IDictionary<string, TValue> Grid { get; }
        private Func<TValue> Supplier { get; }

        public DynamicGrid(Func<TValue> defaultValueSupplier = null)
        {
            Supplier = defaultValueSupplier;
            Grid = new Dictionary<string, TValue>();
        }

        public TValue this[Point p]
        {
            get => this[p.X, p.Y];
            set => this[p.X, p.Y] = value;
        }

        public TValue this[int x, int y]
        {
            get { TValue value = Grid.TryGetValue($"{x},{y}", out value) ? value : GetDefault(); return value; }
            set => Grid[$"{x},{y}"] = value;
        }

        public TValue this[int x, int y, int z]
        {
            get { TValue value = Grid.TryGetValue($"{x},{y},{z}", out value) ? value : GetDefault(); return value; }
            set => Grid[$"{x},{y},{z}"] = value;
        }

        public TValue this[int x, int y, int z, int d]
        {
            get { TValue value = Grid.TryGetValue($"{x},{y},{z},{d}", out value) ? value : GetDefault(); return value; }
            set => Grid[$"{x},{y},{z},{d}"] = value;
        }

        public void Remove(Point p) => Remove(p.X, p.Y);

        public void Remove(int x, int y) => Grid.Remove($"{x},{y}");

        public void Remove(int x, int y, int z) => Grid.Remove($"{x},{y},{z}");

        public void Remove(int x, int y, int z, int d) => Grid.Remove($"{x},{y},{z},{d}");

        public IEnumerable<TValue> Values { get => Grid.Values; }

        public override string ToString()
        {
            var defaultAsString = Utils.NullableToString(GetDefault());
            return $"DynamicGrid[points stored = {Grid.Count}, default value = {defaultAsString}]";
        }

        private TValue GetDefault()
        {
            return Supplier != null ? Supplier.Invoke() : default(TValue);
        }
    }
}
