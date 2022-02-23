using FluentAssertions;
using Xunit;

namespace MarsRover.Test
{
    public static class DirectionExtensions
    {


        [Theory]
        [InlineData(Direction.N, Command.L, Direction.W)]
        [InlineData(Direction.N, Command.R, Direction.E)]

        [InlineData(Direction.E, Command.L, Direction.N)]
        [InlineData(Direction.E, Command.R, Direction.S)]

        [InlineData(Direction.S, Command.L, Direction.E)]
        [InlineData(Direction.S, Command.R, Direction.W)]

        [InlineData(Direction.W, Command.L, Direction.S)]
        [InlineData(Direction.W, Command.R, Direction.N)]
        public static void CorrectRotation_WhenGivenValidCommand(Direction initialDirection, Command command, Direction expectedDirection)
        {
            #region Arrange
            #endregion

            #region Act
            var resultingDirection = initialDirection.Rotate(command);
            #endregion

            #region Assert
            resultingDirection.Should().Be(expectedDirection);
            #endregion
        }
    }
}


