using System;
using System.Collections.Generic;
using FluentAssertions;
using Utils;
using Xunit;

namespace MarsRover.Test
{
    public class Rover
    {


        [Fact]
        public void CorrectCommandQueueSerialized_WhenEnqueuedCommands()
        {
            #region Arrange
            var commandQueue = string.Empty;

            for (int i = 0; i < 2 << 4; ++i)
            {
                var randCmd = RandomExtensions.GetRandom<Command>();
                commandQueue += Enum.GetName<Command>(randCmd);
            }

            var rover = new Models.Rover(
                new Models.Position(
                    RandomExtensions.GetNextUint(),
                    RandomExtensions.GetNextUint()),
                RandomExtensions.GetRandom<Direction>());
            #endregion

            #region Act
            rover.EnqueueCommands(commandQueue);
            #endregion

            #region Assert
            rover.CommandQueueSerialized.Should().Be(commandQueue);
            #endregion
        }


        [Fact]
        public void CreateCorrectRover_WhenTryParsed()
        {
            #region Arrange
            var x = RandomExtensions.GetNextUint();
            var y = RandomExtensions.GetNextUint();
            var d = RandomExtensions.GetRandom<Direction>();
            var expected = new Models.Rover(new Models.Position(x, y), d);
            #endregion

            #region Act
            Models.Rover.TryParse($"{x} {y} {d}", out var parsed);
            #endregion

            #region Assert
            parsed.Should().BeEquivalentTo(expected);
            #endregion
        }


        [Fact]
        public void CorrectString_WhenToString()
        {
            #region Arrange
            var x = RandomExtensions.GetNextUint();
            var y = RandomExtensions.GetNextUint();
            var d = RandomExtensions.GetRandom<Direction>();
            #endregion

            #region Act
            var o = new Models.Rover(new Models.Position(x, y), d);
            #endregion

            #region Assert
            o.ToString().Should().Be($"{x} {y} {d}");
            #endregion
        }


        [Theory]
        [InlineData("1 2 N", "LMLMLMLMM", "1 3 N")]
        [InlineData("3 3 E", "MMRMMRMRRM", "5 1 E")]
        public void CorrectState_WhenAppliedCommandQueue(string initialState, string commandQueue, string expectedState)
        {
            #region Arrange
            Models.Rover.TryParse(initialState, out var rover);
            rover.EnqueueCommands(commandQueue);
            #endregion

            #region Act
            rover.ApplyCommandQueue();
            #endregion

            #region Assert
            rover.CommandQueueSerialized.Should().BeEmpty();
            rover.ToString().Should().Be(expectedState);
            #endregion
        }


        [Theory]
        [InlineData("1 2 N", "LMLMLMLMM", "1 2,0 2,0 1,1 1,1 3")]
        [InlineData("3 3 E", "MMRMMRMRRM", "3 3,4 3,5 3,5 2,5 1,4 1")]
        public void CorrectQueuedToVisit_WhenEnqueuedCommands(string initialState, string commandQueue, string positionStrings)
        {
            #region Arrange
            Models.Rover.TryParse(initialState, out var rover);

            var expectedQueuedToVisit = new HashSet<Models.Position>();
            foreach (var positionString in positionStrings.Split(','))
            {
                Models.Position.TryParse(positionString, out var pos);
                expectedQueuedToVisit.Add(pos);
            }
            #endregion

            #region Act
            rover.EnqueueCommands(commandQueue);
            #endregion

            #region Assert
            rover.GetQueuedToVisit().Should().BeEquivalentTo(expectedQueuedToVisit);
            #endregion
        }
    }
}


