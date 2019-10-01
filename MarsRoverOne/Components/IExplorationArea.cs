namespace MarsRoverOne.Components
{
    public interface IExplorationArea
    {
        int XAxisMaxValue { get; }
        int YAxisMaxValue { get; }
        void ConstructExplorationArea();
    }
}