using FluentAssertions;
using Utils;
using Xunit;

namespace MarsRover.Test
{
    public class Plateau
    {


        [Theory]
        [InlineData(4, 3)]
        [InlineData(5, 5)]
        public void CreatePlateau_WhenTryParsed(int x, int y)
        {
            #region Arrange
            #endregion

            #region Act
            var successful = Models.Plateau.TryParse($"{x} {y}", out var plateau);
            #endregion

            #region Assert
            successful.Should().Be(true);
            #endregion
        }


        [Fact]
        public void CorrectRovers_WhenDeployed()
        {
            #region Arrange
            Models.Plateau.TryParse("5 5", out var plateau);
            Models.Rover.TryParse("1 2 N", out var r1);
            r1.EnqueueCommands("LMLMLMLMM");
            Models.Rover.TryParse("3 3 E", out var r2);
            r2.EnqueueCommands("MMRMMRMRRM");
            #endregion

            #region Act
            plateau.EnqueueToDeploy(r1);
            plateau.EnqueueToDeploy(r2);
            var deployedR1 = plateau.DeployNext();
            var deployedR2 = plateau.DeployNext();
            #endregion

            #region Assert
            deployedR1.ToString().Should().Be("1 3 N");
            deployedR2.ToString().Should().Be("5 1 E");
            #endregion
        }


        [Fact]
        public void FailedDeploy_WhenRoverOutOfBounds()
        {
            #region Arrange
            Models.Plateau.TryParse("5 5", out var plateau);
            Models.Rover.TryParse($"0 5 W", out var r1);
            r1.EnqueueCommands("M");
            Models.Rover.TryParse($"5 0 S", out var r2);
            r2.EnqueueCommands("M");
            Models.Rover.TryParse($"0 6 {RandomExtensions.GetRandom<Direction>()}", out var r3);
            Models.Rover.TryParse($"6 0 {RandomExtensions.GetRandom<Direction>()}", out var r4);
            #endregion

            #region Act
            plateau.EnqueueToDeploy(r1);
            plateau.EnqueueToDeploy(r2);
            plateau.EnqueueToDeploy(r3);
            plateau.EnqueueToDeploy(r4);
            #endregion

            #region Assert
            while (plateau.IsDeploymentQueueNotEmpty())
            {
                plateau.DeployNext().Should().Be(default(Models.Rover));
            }
            #endregion
        }
    }
}


