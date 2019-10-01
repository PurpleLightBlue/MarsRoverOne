using MarsRoverOne.Components;
using Xunit;

namespace MarsRoverOne.Test.UnitTests
{
    public class ExplorationAreaTests
    {
        [Fact]
        public void GivenExplorationAreaCreated_WhenConstructExplorationAreaCalled_ThenExplorationAreaCreated()
        {
            var explorationArea = new ExplorationArea(5, 5);
            explorationArea.ConstructExplorationArea();
            Assert.Equal((5 + 1) * (5 + 1), explorationArea.PositionList.Count);
        }
    }
}