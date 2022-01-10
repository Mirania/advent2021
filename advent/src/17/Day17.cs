using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent.Day17Utils;

namespace Advent
{
    public class Day17 : IAdventDay
    {
        public void Solve()
        {
            var solutions = CalculateSolutions(CreateRectangle(Utils.ReadFileRaw(17)));
            Console.WriteLine("Part 1: {0}", solutions.Item1);
            Console.WriteLine("Part 2: {0}", solutions.Item2);
        }

        // Tuple<highestY, solutionCount>
        private Tuple<int, int> CalculateSolutions(Rectangle targetArea)
        {
            // these vx values should be part of at least one solution
            List<int> likelyVxValues = new List<int>();

            for (int startVx = 0; startVx <= targetArea.MaxX; startVx++)
            {
                for (int x = 0, vx = startVx; vx > -1; x += vx, vx--)
                    if (x >= targetArea.MinX && x <= targetArea.MaxX)
                    {
                        likelyVxValues.Add(startVx);
                        break;
                    }

            }

            // these vy values should be part of at least one solution
            List<int> likelyVyValues = new List<int>();

            for (int startVy = targetArea.MinY; startVy <= Math.Max(Math.Abs(targetArea.MinY), Math.Abs(targetArea.MaxY)); startVy++)
            {
                for (int y = 0, vy = startVy; vy > 0 || y >= targetArea.MinY; y += vy, vy--)
                    if (y >= targetArea.MinY && y <= targetArea.MaxY)
                    {
                        likelyVyValues.Add(startVy);
                        break;
                    }
            }

            // combine all vx and vy values and find the solutions
            var highestY = int.MinValue;
            var solutionCount = 0;

            foreach (var startVx in likelyVxValues)
                foreach (var startVy in likelyVyValues)
                {
                    var testResult = TryWithInitialVelocity(new Point(startVx, startVy), targetArea);
                    if (testResult.Item1)
                    {
                        highestY = Math.Max(highestY, testResult.Item2);
                        solutionCount++;
                    }
                }

            return new Tuple<int, int>(highestY, solutionCount);
        }

        // Tuple<success, highestY>
        // The initialVelocity "point" should actually be called a vector
        private Tuple<bool, int> TryWithInitialVelocity(Point initialVelocity, Rectangle targetArea)
        {
            var position = new Point(0, 0);
            var currentVelocity = initialVelocity;
            var highestY = int.MinValue;

            while ((position.Y >= targetArea.MinY || currentVelocity.Y > 0) && position.X <= targetArea.MaxX)
            {
                highestY = Math.Max(highestY, position.Y);

                if (targetArea.Contains(position))
                    return new Tuple<bool, int>(true, highestY);

                position += currentVelocity;
                currentVelocity += new Point(currentVelocity.X > 0 ? -1 : currentVelocity.X < 0 ? 1 : 0, -1);
            }

            return new Tuple<bool, int>(false, highestY);
        }

        private Rectangle CreateRectangle(string input)
        {
            var coords = Utils.ExtractInts(Utils.ReadFileRaw(17));
            return new Rectangle(coords[0], coords[1], coords[2], coords[3]);
        }
    }
}
