namespace MarsRover.Models
{
    public class Position
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool TryParse(string? line, out Position position)
        {
            const int maxLength = 2;
            var pos = line?.Split(" ", maxLength);
            if (pos != null
                && pos.Length == maxLength
                && int.TryParse(pos[0], out var x)
                && int.TryParse(pos[1], out var y)
                )
            {
                position = new Position(x, y);
                return true;
            }

            position = default;
            return false;
        }

        public Position GetNeighbour(Direction direction)
        {
            const int reach = 1;
            var x = X;
            var y = Y;
            switch (direction)
            {
                case Direction.N:
                    y += reach;
                    break;
                case Direction.E:
                    x += reach;
                    break;
                case Direction.S:
                    y -= reach;
                    break;
                case Direction.W:
                    x -= reach;
                    break;
                default:
                    break;
            }
            return new Position(x, y);
        }

        public override bool Equals(object? obj)
        {
            return obj is Position position &&
                   X == position.X &&
                   Y == position.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}