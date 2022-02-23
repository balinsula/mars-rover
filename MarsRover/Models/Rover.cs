namespace MarsRover.Models
{
    public class Rover
    {
        public Direction Direction { get; private set; }
        public Position Position { get; private set; }
        public string CommandQueueSerialized
        {
            get
            {
                var commandQueueSerialized = string.Empty;
                foreach (var cmd in CommandQueue)
                {
                    commandQueueSerialized += Enum.GetName<Command>(cmd);
                }
                return commandQueueSerialized;
            }
        }
        private Queue<Command> CommandQueue = new Queue<Command>();


        public Rover(Position position, Direction direction)
        {
            Position = position;
            Direction = direction;
        }


        public static bool TryParse(string? line, out Rover rover)
        {
            const int maxLength = 3;
            var pos = line?.Split(" ", maxLength);
            if (pos != null
                && pos.Length == maxLength
                && uint.TryParse(pos[0], out var x)
                && uint.TryParse(pos[1], out var y)
                && Enum.TryParse(pos[2], true, out Direction dir)
                )
            {
                rover = new Rover(new Position(x, y), dir);
                return true;
            }

            rover = default;
            return false;
        }


        public override string ToString()
        {
            return $"{Position.X} {Position.Y} {Enum.GetName<Direction>(Direction)}";
        }


        public void EnqueueCommands(string value)
        {
            foreach (var c in value)
            {
                if (Enum.TryParse(c.ToString(), out Command cmd))
                {
                    CommandQueue.Enqueue(cmd);
                }
            }
        }


        public void ApplyCommandQueue()
        {
            while (CommandQueue.TryDequeue(out var cmd))
            {
                ApplyCommand(cmd);
            }
        }


        public HashSet<Position> GetQueuedToVisit()
        {
            var toVisit = new HashSet<Position>();
            toVisit.Add(Position);
            foreach (var r in GetStateQueue())
            {
                toVisit.Add(r.Position);
            }
            return toVisit;
        }


        private Queue<Rover> GetStateQueue()
        {
            var stateQueue = new Queue<Rover>();

            var r = Copy();
            while (r.CommandQueue.TryDequeue(out var cmd))
            {
                r.ApplyCommand(cmd);
                stateQueue.Enqueue(r.Copy());
            }
            return stateQueue;
        }


        private void ApplyCommand(Command cmd)
        {
            switch (cmd)
            {
                case Command.M:
                    Position = Position.GetNeighbour(Direction);
                    break;
                case Command.L:
                case Command.R:
                    Direction = Direction.Rotate(cmd);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        private Rover Copy()
        {
            TryParse(ToString(), out var r);
            r.EnqueueCommands(CommandQueueSerialized);
            return r;
        }
    }
}