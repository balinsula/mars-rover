namespace MarsRover
{
    public enum Direction
    {
        N,
        E,
        S,
        W,
    }

    public static class DirectionExtensions
    {
        private static int DirectionsCount = Enum.GetValues<Direction>().Length;


        private static int Mod(this int x, int m) => (x % m + m) % m;


        public static Direction Rotate(this Direction dir, Command cmd)
        {
            var howManyClockWise = (int)Math.Pow(-1, (int)cmd);
            var overShotResult = (int)dir + howManyClockWise;
            var result = overShotResult.Mod(DirectionsCount);
            return (Direction)result;
        }
    }
}