namespace MarsRoverOne.Components
{
    public interface IPosition
    {
        Direction Direction { get; set; }
        int XAxis { get; set; }
        int YAxis { get; set; }
        string GetConcatenatedPositionString();
    }
}