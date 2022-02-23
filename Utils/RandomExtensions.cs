namespace Utils
{
    public static class RandomExtensions
    {
        public static Random Random = new Random();


        public static T GetRandom<T>() where T : struct, Enum
        {
            var v = Enum.GetValues<T>();
            return (T)v.GetValue(Random.Next(v.Length));
        }
    }
}