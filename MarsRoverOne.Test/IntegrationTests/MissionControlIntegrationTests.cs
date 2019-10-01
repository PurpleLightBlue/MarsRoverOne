using MarsRoverOne.Components;
using Xunit;

namespace MarsRoverOne.Test.IntegrationTests
{
    public class MissionControlIntegrationTests
    {
        [Fact]
        public void GivenMissionControlCreated_WhenIsPositionOccupiedCalledAndPositionIsOccupied_ThenTrueReturned()
        {
            var position = new Position {YAxis = 1, XAxis = 2, Direction = Direction.N};

            var missionControl = MissionControl.Instance;
            missionControl.RoverPositionDictionary.Add(1, position);

            Assert.True(missionControl.IsPositionOccupied(position, 2));
            missionControl.RoverPositionDictionary.Clear();
        }

        [Fact]
        public void GivenMissionControlCreated_WhenIsPositionOccupiedCalledAndPositionNotOccupied_ThenFalseReturned()
        {
            var position = new Position {YAxis = 1, XAxis = 2, Direction = Direction.N};

            var missionControl = MissionControl.Instance;
            missionControl.RoverPositionDictionary.Add(1, position);

            var newPosition = new Position {YAxis = 1, XAxis = 3, Direction = Direction.S};
            Assert.False(missionControl.IsPositionOccupied(newPosition, 2));
            missionControl.RoverPositionDictionary.Clear();
        }
    }
}