using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Day18Utils
{
    public class Snailfish
    {
        private Snailfish Left { get; set; }
        private Snailfish Right { get; set; }
        private int Value { get; set; } = -1;
        private Snailfish Parent { get; set; }
        private bool IsNumber { get; set; } // if it's not a number, it's a pair

        private Snailfish() { }

        public Snailfish Reduce()
        {
            while (true)
            {
                var expCandidate = NextToExplode();
                if (expCandidate != null) { expCandidate.Explode(); continue; }

                var splCandidate = NextToSplit();
                if (splCandidate != null) { splCandidate.Split(); continue; }

                break;
            }

            return this;
        }

        private Snailfish NextToExplode()
        {
            if (IsNumber)
                return null;

            if (Left.IsNumber && Right.IsNumber && Parent?.Parent?.Parent?.Parent != null)
                return this;

            return Left.NextToExplode() ?? Right.NextToExplode();
        }

        private void Explode()
        {
            if (IsNumber || !Left.IsNumber || !Right.IsNumber)
                throw new InvalidOperationException($"cannot explode a number or pair of pairs: {this}");

            var nextLeft = FindNearest(true);
            if (nextLeft != null)
                nextLeft.Value += Left.Value;

            var nextRight = FindNearest(false);
            if (nextRight != null)
                nextRight.Value += Right.Value;

            var newSnailfish = new Snailfish() { Value = 0, IsNumber = true };
            newSnailfish.Parent = Parent;
            if (Parent.Left == this) Parent.Left = newSnailfish;
            else Parent.Right = newSnailfish;
        }

        private Snailfish NextToSplit()
        {
            if (IsNumber)
                return Value >= 10 ? this : null;

            return Left.NextToSplit() ?? Right.NextToSplit();
        }

        private void Split()
        {
            if (!IsNumber)
                throw new InvalidOperationException($"cannot split a pair: {this}");

            var left = new Snailfish() { Value = (int)Math.Floor(Value / 2f), IsNumber = true };
            var right = new Snailfish() { Value = (int)Math.Ceiling(Value / 2f), IsNumber = true };

            var newSnailfish = left + right;
            newSnailfish.Parent = Parent;
            if (Parent.Left == this) Parent.Left = newSnailfish;
            else Parent.Right = newSnailfish;
        }

        private Snailfish FindNearest(bool toTheLeft)
        {
            Snailfish candidate;

            var self = this;
            var selfParent = Parent;
            while (selfParent != null && (toTheLeft ? selfParent.Left : selfParent.Right) == self)
            {
                self = selfParent;
                selfParent = selfParent.Parent;
            }

            if (selfParent == null)
                return null;

            candidate = toTheLeft ? selfParent.Left : selfParent.Right;
            while (!candidate.IsNumber)
                candidate = toTheLeft ? candidate.Right : candidate.Left;

            return candidate;
        }

        public long Magnitude()
        {
            return IsNumber ? Value : 3 * Left.Magnitude() + 2 * Right.Magnitude();
        }

        public static Snailfish operator +(Snailfish a, Snailfish b)
        {
            var newSnailfish = new Snailfish() { Left = a, Right = b, IsNumber = false };
            a.Parent = newSnailfish;
            b.Parent = newSnailfish;
            return newSnailfish;
        }

        public static Snailfish Parse(string input)
        {
            var stack = new Stack<Snailfish>();

            foreach (var ch in input)
            {
                if (ch == '[')
                {
                    var parent = stack.Count > 0 ? stack.Peek() : null;
                    var newSnailfish = new Snailfish() { Parent = parent, IsNumber = false };
                    if (parent != null)
                    {
                        if (parent.Left == null) parent.Left = newSnailfish;
                        else parent.Right = newSnailfish;
                    }
                    stack.Push(newSnailfish);
                }
                if (char.IsNumber(ch))
                {
                    var parent = stack.Peek();
                    var newSnailfish = new Snailfish() { Parent = parent, Value = int.Parse(ch.ToString()), IsNumber = true };
                    if (parent.Left == null) parent.Left = newSnailfish;
                    else parent.Right = newSnailfish;
                }
                if (ch == ']')
                {
                    var popped = stack.Pop();
                    if (stack.Count == 0) return popped;
                }
            }

            throw new ArgumentException($"cannot parse input: {input}");
        }

        public override string ToString()
        {
            return IsNumber ? Value.ToString() : $"[{Left},{Right}]";
        }
    }
}
