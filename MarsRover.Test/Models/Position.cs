using System.Collections.Generic;
using FluentAssertions;
using Utils;
using Xunit;

namespace MarsRover.Test
{
    public class Position
    {


        [Fact]
        public void NoDuplicates_WhenDuplicatesTriedToBeAdded()
        {
            #region Arrange
            var x = RandomExtensions.GetNextUint();
            var y = RandomExtensions.GetNextUint();
            var p1 = new Models.Position(x, y);
            var p2 = new Models.Position(x, y);
            #endregion

            #region Act
            var set = new HashSet<Models.Position>() { p1, p2 };
            #endregion

            #region Assert
            p1.Should().Be(p2);
            set.Count.Should().Be(1);
            #endregion
        }


        [Fact]
        public void CreateCorrectPosition_WhenGivenCoordinates()
        {
            #region Arrange
            var x = RandomExtensions.GetNextUint();
            var y = RandomExtensions.GetNextUint();
            var expected = new Models.Position(x, y);
            #endregion

            #region Act
            Models.Position.TryParse($"{x} {y}", out var parsed);
            #endregion

            #region Assert
            parsed.Should().Be(expected);
            #endregion
        }
    }
}
