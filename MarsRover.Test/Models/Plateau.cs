using FluentAssertions;
using Xunit;

namespace MarsRover.Test
{
    public class Plateau
    {
        [Theory]
        [InlineData(4, 3)]
        [InlineData(5, 5)]
        public void CreateCorrectPlateau_WhenGivenDimensions(int x, int y)
        {
            #region Arrange
            #endregion

            #region Act
            Models.Plateau.TryParse($"{x} {y}", out var plateau);
            #endregion

            #region Assert
            plateau.Map.GetLength(0).Should().Be(x + 1);
            plateau.Map.GetLength(1).Should().Be(y + 1);
            #endregion
        }
    }
}


