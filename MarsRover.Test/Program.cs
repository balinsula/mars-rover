using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace MarsRover.Test;

public static class Program
{


    public static string StandardizedNewLine(this string value)
    {
        const char CR = '\r';
        const char LF = '\n';
        return value
            .Replace($"{CR}{LF}", Environment.NewLine)
            .Replace($"{CR}", Environment.NewLine)
            .Replace($"{LF}", Environment.NewLine)
            ;
    }


    public static MemoryStream GenerateStreamFromString(string s)
    {
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(s ?? ""));
        var writer = new StreamWriter(stream);
        writer.Write(s);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }

    [Theory]
    [InlineData(@"5 5
1 2 N
LMLMLMLMM
3 3 E
MMRMMRMRRM
",
@"1 3 N
5 1 E
"
)]
    public static async Task CorrectResult_WhenDeployRoversToPlateau(string input, string expectedOutput)
    {
        #region Arrange
        expectedOutput = expectedOutput.StandardizedNewLine();
        using var stream = GenerateStreamFromString(input);
        #endregion

        #region Act
        var plateau = await MarsRover.Program.ReadStreamAsync(stream);
        using var ms = new MemoryStream();
        await (plateau?.DeployRoversToPlateau(ms) ?? Task.CompletedTask);
        var output = Encoding.UTF8.GetString(ms.ToArray());
        #endregion

        #region Assert
        output.Should().Be(expectedOutput);
        #endregion
    }
}

