namespace MarsRover.Models
{
    public class Plateau
    {
        private readonly Queue<Rover> DeploymentQueue = new Queue<Rover>();
        private readonly Rover[,] Map;

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

        public Rover DeployNext()
        {
            if (DeploymentQueue.TryDequeue(out var rover))
            {
                foreach (var pos in rover.GetQueuedToVisit())
                {
                    if
                    (
                        // Goes beyond plateau horizontally
                        pos.X >= Map.GetLength(0)
                        || pos.X < 0
                        // Goes beyond plateau vertically
                        || pos.Y >= Map.GetLength(1)
                        || pos.Y < 0
                        // There's at least a single coordinate already occupied
                        || Map[pos.X, pos.Y] != null
                    )
                    {
                        return default;
                    }
                }
                rover.ApplyCommandQueue();
                Map[rover.Position.X, rover.Position.Y] = rover;
                return rover;
            }
            return default;
        }

        public void EnqueueToDeploy(Rover rover) => DeploymentQueue.Enqueue(rover);


        public bool IsDeploymentQueueNotEmpty() => DeploymentQueue.Any();
    }
}