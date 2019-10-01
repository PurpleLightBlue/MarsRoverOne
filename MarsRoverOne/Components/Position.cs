namespace MarsRoverOne.Components
{
    public class Position : IPosition
    {
        public Direction Direction { get; set; }
        public int XAxis { get; set; }
        public int YAxis { get; set; }

        public string GetConcatenatedPositionString()
        {
            return $"{XAxis}{YAxis}{Direction}";
        }
    }
}