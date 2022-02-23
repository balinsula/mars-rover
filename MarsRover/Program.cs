using MarsRover.Models;

namespace MarsRover
{
    public static class Program
    {
        static async Task MainAsync()
        {
            var inputStream = Console.OpenStandardInput();
            var plateau = await ReadStreamAsync(inputStream);
            using var outputStream = Console.OpenStandardOutput();
            await (plateau?.DeployRoversToPlateau(outputStream) ?? Task.CompletedTask);
        }


        public static async Task<Plateau> ReadStreamAsync(Stream stream)
        {
            using var s = new StreamReader(stream);
            var lineRead = string.Empty;
            Plateau plateau = null;
            Rover rover = null;
            while (!string.IsNullOrWhiteSpace(lineRead = await s.ReadLineAsync()) || rover != null)
            {
                if (plateau == null && !Plateau.TryParse(lineRead, out plateau))
                {
                    Console.WriteLine("Please enter plateau size:");
                    continue;
                }
                if (rover == null && Rover.TryParse(lineRead, out rover))
                {
                    continue;
                }
                if (rover != null)
                {
                    rover.EnqueueCommands(lineRead ?? string.Empty);
                    plateau.EnqueueToDeploy(rover);
                    rover = null;
                }
            }
            return plateau;
        }


        public static async Task DeployRoversToPlateau(this Plateau plateau, Stream stream)
        {
            while (plateau.IsDeploymentQueueNotEmpty())
            {
                var deployedRover = plateau.DeployNext();
                var message = deployedRover?.ToString() ?? "Rover could not be deployed!";
                await stream.WriteLineAsync(message);
            }
        }


        private static async Task WriteLineAsync(this Stream stream, string message)
        {
            var sw = new StreamWriter(stream);
            sw.AutoFlush = true;
            Console.SetOut(sw);
            await sw.WriteLineAsync(message);
        }
    }
}