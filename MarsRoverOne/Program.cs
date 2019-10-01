using MarsRoverOne.Components;

namespace MarsRoverOne
{
    class Program
    {
        static void Main(string[] args)
        {
            var missionControl = MissionControl.Instance;
            missionControl.BeginMission();
        }
    }
}