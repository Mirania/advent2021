using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent
{
    /// <summary>
    /// Fast but only supports *non-negative* coordinates and up to 4 dimensions. Uses a high amount of memory.
    /// Should only be used when both the inserted points and the space between them matters, and the grid dimensions are small.
    /// </summary>
    public class FixedGrid<TValue>
    {
        private TValue[] Grid { get; }
        private Func<TValue> Supplier { get; }

        public int SizeX { get; }
        public int SizeY { get; }
        public int SizeZ { get; }
        public int SizeD { get; }

        public FixedGrid(int sizeX, int sizeY, Func<TValue> defaultValueSupplier = null)
        {
            Supplier = defaultValueSupplier;
            Grid = new TValue[sizeX * sizeY];
            SizeX = sizeX; SizeY = sizeY; SizeZ = 1; SizeD = 1;
            if (Supplier != null) FillGridUsingSupplier();
        }

        public FixedGrid(int sizeX, int sizeY, int sizeZ, Func<TValue> defaultValueSupplier = null)
        {
            Supplier = defaultValueSupplier;
            Grid = new TValue[sizeX * sizeY * sizeZ];
            SizeX = sizeX; SizeY = sizeY; SizeZ = sizeZ;
            if (Supplier != null) FillGridUsingSupplier();
        }

        public FixedGrid(int sizeX, int sizeY, int sizeZ, int sizeD, Func<TValue> defaultValueSupplier = null)
        {
            Supplier = defaultValueSupplier;
            Grid = new TValue[sizeX * sizeY * sizeZ * sizeD];
            SizeX = sizeX; SizeY = sizeY; SizeZ = sizeZ; SizeD = sizeD;
            if (Supplier != null) FillGridUsingSupplier();
        }

        public TValue this[Point p]
        {
            get => this[p.X, p.Y];
            set => this[p.X, p.Y] = value;
        }

        public TValue this[int x, int y]
        {
            get => Grid[x * SizeY + y];
            set => Grid[x * SizeY + y] = value;
        }

        public TValue this[int x, int y, int z]
        {
            get => Grid[x * SizeY * SizeZ + y * SizeZ + z];
            set => Grid[x * SizeY * SizeZ + y * SizeZ + z] = value;
        }

        public TValue this[int x, int y, int z, int d]
        {
            get => Grid[x * SizeY * SizeZ * SizeD + y * SizeZ * SizeD + z * SizeD + d];
            set => Grid[x * SizeY * SizeZ * SizeD + y * SizeZ * SizeD + z * SizeD + d] = value;
        }

        public IEnumerable<TValue> Values { get => Grid; }

        public string ContentsToString(Func<TValue, string> contentTransformer)
        {
            var sb = new StringBuilder(SizeX * (SizeY + 1));
            for (var y = 0; y < SizeY; y++)
            {
                for (var x = 0; x < SizeX; x++)
                    sb.Append(contentTransformer.Invoke(this[x, y]));
                if (y < SizeY - 1)
                    sb.Append('\n');
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            var defaultAsString = Utils.NullableToString(GetDefault());
            var dimensions = $"{SizeX}x{SizeY}{(SizeZ == 1 ? "" : $"x{SizeZ}")}{(SizeD == 1 ? "" : $"x{SizeD}")}";
            return $"FixedGrid[dimensions = {dimensions}, total size = {Grid.Length}, default value = {defaultAsString}]";
        }

        private void FillGridUsingSupplier()
        {
            for (var i = 0; i < Grid.Length; i++)
                Grid[i] = Supplier.Invoke();
        }

        private TValue GetDefault()
        {
            return Supplier != null ? Supplier.Invoke() : default(TValue);
        }

        private void DebugCheckAccess(int dims, int x, int y, int z = 0, int d = 0)
        {
            var realDimensions = SizeZ == 1 && SizeD == 1 ? 2 : SizeD == 1 ? 3 : 4;
            if (dims != realDimensions) throw new InvalidOperationException($"Checking a {dims}D point in a {realDimensions}D grid");
            if (x < 0 || x >= SizeX) throw new IndexOutOfRangeException($"The x value {x} is out of range. X range is [0, {SizeX - 1}]");
            if (y < 0 || y >= SizeY) throw new IndexOutOfRangeException($"The y value {y} is out of range. Y range is [0, {SizeY - 1}]");
            if (z < 0 || z >= SizeZ) throw new IndexOutOfRangeException($"The z value {z} is out of range. Z range is [0, {SizeZ - 1}]");
            if (d < 0 || d >= SizeD) throw new IndexOutOfRangeException($"The d value {d} is out of range. D range is [0, {SizeD - 1}]");
        }
    }
}
