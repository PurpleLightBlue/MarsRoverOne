using System;
using MarsRoverOne.Components;
using NSubstitute;
using Xunit;

namespace MarsRoverOne.Test.UnitTests
{
    public class RoverUnitTests
    {
        [Fact]
        public void GivenRoverCreated_WhenExecuteMovementCalled_ThenMovementExecuted()
        {
            var explorationArea = Substitute.For<IExplorationArea>();
            explorationArea.XAxisMaxValue.Returns(5);
            explorationArea.YAxisMaxValue.Returns(5);
            explorationArea.ConstructExplorationArea();
            var position = Substitute.For<IPosition>();
            position.XAxis = 1;
            position.YAxis = 2;
            position.Direction = Direction.N;
            var rover = new Rover(1, explorationArea, position);
            rover.ExecuteMovement("LMLMLMLMM");
            Assert.Equal(1, rover.RoverPosition.XAxis);
            Assert.Equal(3, rover.RoverPosition.YAxis);
            Assert.Equal(Direction.N, rover.RoverPosition.Direction);
        }

        [Fact]
        public void GivenRoverCreated_WhenExecuteMovementCalledWithNull_ThenExceptionThrown()
        {
            var explorationArea = Substitute.For<IExplorationArea>();
            explorationArea.XAxisMaxValue.Returns(5);
            explorationArea.YAxisMaxValue.Returns(5);
            explorationArea.ConstructExplorationArea();
            var position = Substitute.For<IPosition>();
            position.XAxis = 1;
            position.YAxis = 2;
            position.Direction = Direction.N;
            var rover = new Rover(0, explorationArea, position);
            Assert.Throws<ArgumentNullException>(() => rover.ExecuteMovement(null));
        }

        [Fact]
        public void GivenRoverCreated_WhenExecuteMovementCalledWithImpossibleCommand_ThenExceptionThrown()
        {
            var explorationArea = Substitute.For<IExplorationArea>();
            explorationArea.XAxisMaxValue.Returns(5);
            explorationArea.YAxisMaxValue.Returns(5);
            explorationArea.ConstructExplorationArea();

            var position = Substitute.For<IPosition>();
            position.XAxis = 1;
            position.YAxis = 2;
            position.Direction = Direction.N;
            var rover = new Rover(1, explorationArea, position);
            Assert.Throws<Exception>(() => rover.ExecuteMovement("MMMMMMMMMMMMMMMMMMMM"));
        }

        [Fact]
        public void GivenRoverCreated_WhenReportPositionCalled_ThenStringReturned()
        {
            var explorationArea = Substitute.For<IExplorationArea>();
            var position = Substitute.For<IPosition>();
            position.GetConcatenatedPositionString().Returns("12N");
            var rover = new Rover(1, explorationArea, position);
            Assert.Equal("12N", rover.ReportPosition());
        }

        [Fact]
        public void GivenRoverCreated_WhenExecuteMovementCalledWithIncorrectCharacters_ThenExceptionThrown()
        {
            var explorationArea = Substitute.For<IExplorationArea>();
            explorationArea.XAxisMaxValue.Returns(5);
            explorationArea.YAxisMaxValue.Returns(5);
            explorationArea.ConstructExplorationArea();

            var position = Substitute.For<IPosition>();
            position.XAxis = 1;
            position.YAxis = 2;
            position.Direction = Direction.N;
            var rover = new Rover(1, explorationArea, position);
            Assert.Throws<ArgumentException>(() => rover.ExecuteMovement("XXXXXXXXXX"));
        }

        [Fact]
        public void GivenNullPosition_WhenRoverConstructorCalled_ThenExceptionThrown()
        {
            var explorationArea = Substitute.For<IExplorationArea>();
            explorationArea.XAxisMaxValue.Returns(5);
            explorationArea.YAxisMaxValue.Returns(5);
            explorationArea.ConstructExplorationArea();
            Assert.Throws<ArgumentNullException>(() => new Rover(1, explorationArea, null));
        }

        [Fact]
        public void GivenNullExplorationArea_WhenRoverConstructorCalled_ThenExceptionThrown()
        {
            var position = Substitute.For<IPosition>();
            position.XAxis = 1;
            position.YAxis = 2;
            position.Direction = Direction.N;
            Assert.Throws<ArgumentNullException>(() => new Rover(1, null, position));
        }
    }
}