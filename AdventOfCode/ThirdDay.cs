using System;
namespace AdventOfCode {
    public class ThirdDay : Solution {
        public ThirdDay() {
        }
        private static readonly int LIMIT = 347991;
        private static readonly int SQRT_LIMIT = 590;
        private int[,] storage = new int[SQRT_LIMIT + 1, SQRT_LIMIT + 1];

        private enum Direction {
            RIGHT,
            UP,
            LEFT,
            DOWN
        }

        public override void Run() {
            base.PrintHeader();

            Fill(0, 0, 1);
            int currentValue = 2;
            int x = 1;
            int y = 0;
            Direction currentDirection = Direction.RIGHT;
            while (currentValue <= LIMIT) {
                Fill(x, y, currentValue);
                currentValue++;
                if (ChangeDirection(x, y, currentDirection)) {
                    currentDirection = NextDirection(currentDirection);
                }
                NextValue(x, y, currentDirection, out x, out y);
            }
            Console.Out.WriteLine(
                "Position of {0} is ({1}, {2}) with distance {3}",
                LIMIT,
                x,
                y,
                Math.Abs(x) + Math.Abs(y) - 1);

            storage = new int[SQRT_LIMIT + 1, SQRT_LIMIT + 1];

            int lastSum = 0;
            x = 1;
            y = 0;
            Fill(0, 0, 1);
            currentDirection = Direction.RIGHT;

            while (lastSum <= LIMIT) {
                lastSum = SumOfNeighbours(x, y);
                Fill(x, y, lastSum);
                if (ChangeDirection(x, y, currentDirection)) {
                    currentDirection = NextDirection(currentDirection);
                }
                NextValue(x, y, currentDirection, out x, out y);
            }

            Console.Out.WriteLine();
            Console.Out.WriteLine(
        "First value to excede the limit {0} is at ({1},{2}) with value {3}",
                    LIMIT,
                    x,
                    y,
                    lastSum);
        }

        // Fills the array, using (0,0) as the centre position.
        void Fill(int x, int y, int value) {
            storage[(SQRT_LIMIT / 2) + x, (SQRT_LIMIT / 2) + y] = value;
        }

        void NextValue(int x, int y, Direction direction, out int newX, out int newY) {
            switch (direction) {
                case Direction.RIGHT:
                    newX = x + 1;
                    newY = y;
                    return;
                case Direction.UP:
                    newY = y + 1;
                    newX = x;
                    return;
                case Direction.LEFT:
                    newX = x - 1;
                    newY = y;
                    return;
                case Direction.DOWN:
                    newY = y - 1;
                    newX = x;
                    return;
            }
            newX = x;
            newY = y;
        }

        // Picks the next position to fill in the square.
        Direction NextDirection(Direction direction) {
            switch (direction) {
                case Direction.RIGHT:
                    return Direction.UP;
                case Direction.UP:
                    return Direction.LEFT;
                case Direction.LEFT:
                    return Direction.DOWN;
                case Direction.DOWN:
                    return Direction.RIGHT;
            }
            return Direction.RIGHT;
        }

        // Checks if we should change direction.
        bool ChangeDirection(int x, int y, Direction currentDirection) {
            switch (currentDirection) {
                case Direction.RIGHT:
                    return IsFilled(x, y + 1);
                case Direction.UP:
                    return IsFilled(x - 1, y);
                case Direction.LEFT:
                    return IsFilled(x, y - 1);
                case Direction.DOWN:
                    return IsFilled(x + 1, y);
            }
            return false;
        }

        bool IsFilled(int x, int y) {
            return storage[(SQRT_LIMIT / 2) + x, (SQRT_LIMIT / 2) + y] == 0;
        }

        int SumOfNeighbours(int x, int y) {
            return GetValue(x - 1, y + 1) + GetValue(x, y + 1) + GetValue(x + 1, y + 1)
                 + GetValue(x - 1, y) + 0 + GetValue(x + 1, y)
                 + GetValue(x - 1, y - 1) + GetValue(x, y - 1) + GetValue(x + 1, y - 1);
        }

        int GetValue(int x, int y) {
            return storage[(SQRT_LIMIT / 2) + x, (SQRT_LIMIT / 2) + y];
        }
    }
}
