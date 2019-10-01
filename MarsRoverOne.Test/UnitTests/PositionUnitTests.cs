using System;
using MarsRoverOne.Components;
using Xunit;

namespace MarsRoverOne.Test.UnitTests
{
    public class PositionUnitTests
    {
        [Fact]
        public void GivenPositionCreated_WhenDirectionPropertySetWithIncorrectCharacter_ThenExceptionThrown()
        {
            Assert.Throws<ArgumentException>(() =>
                new Position {Direction = (Direction) Enum.Parse(typeof(Direction), "X")});
        }
    }
}