using System;
using MarsRoverOne.Components;
using NSubstitute;
using Xunit;

namespace MarsRoverOne.Test.UnitTests
{
    public class MissionControlTests
    {
        [Fact]
        public void GivenMissionControlCreated_WhenReportFinalPositionsCalled_ThenStringReturned()
        {
            var missionControl = MissionControl.Instance;
            missionControl.ReportFinalPositions();
            Assert.Equal("Final positions of all rovers: \r\n", missionControl.ReportFinalPositions().ToString());
        }

        [Fact]
        public void GivenMissionControlCreated_WhenIsPositionOccupiedCalledAndPositionIsOccupied_ThenTrueReturned()
        {
            var position = Substitute.For<IPosition>();
            position.YAxis = 1;
            position.XAxis = 2;
            position.Direction = Direction.N;

            var missionControl = MissionControl.Instance;
            missionControl.RoverPositionDictionary.Add(1, position);

            Assert.True(missionControl.IsPositionOccupied(position, 2));
            missionControl.RoverPositionDictionary.Clear();
        }

        [Fact]
        public void GivenMissionControlCreated_WhenIsPositionOccupiedCalledAndPositionNotOccupied_ThenFalseReturned()
        {
            var position = Substitute.For<IPosition>();
            position.YAxis = 1;
            position.XAxis = 2;
            position.Direction = Direction.N;

            var missionControl = MissionControl.Instance;
            missionControl.RoverPositionDictionary.Add(1, position);

            var newPosition = Substitute.For<IPosition>();
            newPosition.YAxis = 1;
            newPosition.XAxis = 3;
            newPosition.Direction = Direction.S;
            Assert.False(missionControl.IsPositionOccupied(newPosition, 2));
            missionControl.RoverPositionDictionary.Clear();
        }

        [Fact]
        public void GivenMissionControlCreated_WhenIsPositionOccupiedCalledWithNullPosition_ThenExceptionThrown()
        {
            var position = Substitute.For<IPosition>();
            position.YAxis = 1;
            position.XAxis = 2;
            position.Direction = Direction.N;

            var missionControl = MissionControl.Instance;
            missionControl.RoverPositionDictionary.Add(1, position);

            Assert.Throws<ArgumentNullException>(() => missionControl.IsPositionOccupied(null, 2));
            missionControl.RoverPositionDictionary.Clear();
        }
    }
}