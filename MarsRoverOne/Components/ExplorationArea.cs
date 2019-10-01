using System.Collections.Generic;

namespace MarsRoverOne.Components
{
    public class ExplorationArea : IExplorationArea
    {
        public List<Position> PositionList { get; }
        public int XAxisMaxValue { get; }
        public int YAxisMaxValue { get; }

        public ExplorationArea(int xAxis, int yAxis)
        {
            XAxisMaxValue = xAxis;
            YAxisMaxValue = yAxis;
            PositionList = new List<Position>();
        }

        public void ConstructExplorationArea()
        {
            for (var xAxisCounter = 0; xAxisCounter <= XAxisMaxValue; xAxisCounter++)
            {
                for (var yAxisCounter = 0; yAxisCounter <= YAxisMaxValue; yAxisCounter++)
                {
                    var temporaryPosition = new Position {XAxis = xAxisCounter, YAxis = yAxisCounter};
                    PositionList.Add(temporaryPosition);
                }
            }
        }
    }
}