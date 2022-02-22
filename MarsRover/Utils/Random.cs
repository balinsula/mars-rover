namespace MarsRover.Utils
{
    public static class RandomExtensions
    {
        // maximum number of uniform bits is 30 according to:
        // https://stackoverflow.com/a/17080161/7032856
        const int MaxNoOfUniformBitSize = 30;
        const int UintBitSize = 32;
        const int RemainderBitSize = UintBitSize - MaxNoOfUniformBitSize;
        private static Random Random = new Random();

        public static uint GetNextUint()
        {
            var maxAvailableUniformBits = (uint)Random.Next(1 << MaxNoOfUniformBitSize);
            var remainderBitsToComplete = (uint)Random.Next(1 << RemainderBitSize);
            return (maxAvailableUniformBits << RemainderBitSize) | remainderBitsToComplete;
        }
    }
}