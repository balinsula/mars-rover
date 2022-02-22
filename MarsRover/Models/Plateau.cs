namespace MarsRover.Models
{
    public class Plateau
    {
        public readonly Queue<Rover> DeploymentQueue = new Queue<Rover>();
        public readonly Rover[,] Map;

        public Plateau(uint x, uint y)
        {
            Map = new Rover[x + 1, y + 1];
        }

        public static bool TryParse(string? line, out Plateau plateau)
        {
            const int maxLength = 2;
            var pos = line?.Split(" ", maxLength);
            if (pos != null
                && pos.Length == maxLength
                && uint.TryParse(pos[0], out var x)
                && uint.TryParse(pos[1], out var y)
                )
            {
                plateau = new Plateau(x, y);
                return true;
            }

            plateau = default;
            return false;
        }
    }
}