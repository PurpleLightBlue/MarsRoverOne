using System;
using System.Collections.Generic;
using System.Text;
using MarsRoverOne.Components;
using NSubstitute;
using Xunit;

namespace MarsRoverOne.Test.IntegrationTests
{
    public class RoverIntegrationTests
    {
        [Fact]
        public void GivenRoverCreated_WhenExecuteMovementCalled_ThenMovementExecuted()
        {
            var explorationArea = new ExplorationArea(5, 5);
            explorationArea.ConstructExplorationArea();
            var position = new Position {XAxis = 1, YAxis = 2, Direction = Direction.N};
            var rover = new Rover(1, explorationArea, position);
            rover.ExecuteMovement("LMLMLMLMM");
            Assert.Equal(1, rover.RoverPosition.XAxis);
            Assert.Equal(3, rover.RoverPosition.YAxis);
            Assert.Equal(Direction.N, rover.RoverPosition.Direction);
        }

        [Fact]
        public void GivenRoverCreated_WhenExecuteMovementCalledWithNull_ThenExceptionThrown()
        {
            var explorationArea = new ExplorationArea(5, 5);
            explorationArea.ConstructExplorationArea();
            var position = new Position {XAxis = 1, YAxis = 2, Direction = Direction.N};
            var rover = new Rover(0, explorationArea, position);
            Assert.Throws<ArgumentNullException>(() => rover.ExecuteMovement(null));
        }

        [Fact]
        public void GivenRoverCreated_WhenExecuteMovementCalledWithImpossibleCommand_ThenExceptionThrown()
        {
            var explorationArea = new ExplorationArea(5, 5);
            explorationArea.ConstructExplorationArea();
            var position = new Position {XAxis = 1, YAxis = 2, Direction = Direction.N};
            var rover = new Rover(1, explorationArea, position);
            Assert.Throws<OutOfBoundsException>(() => rover.ExecuteMovement("MMMMMMMMMMMMMMMMMMMM"));
        }

        [Fact]
        public void GivenRoverCreated_WhenReportPositionCalled_ThenStringReturned()
        {
            var explorationArea = new ExplorationArea(5, 5);
            var position = new Position {XAxis = 1, YAxis = 2, Direction = Direction.N};
            var rover = new Rover(1, explorationArea, position);
            Assert.Equal("12N", rover.ReportPosition());
        }
    }
}