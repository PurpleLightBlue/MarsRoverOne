using System.Text;

namespace MarsRoverOne.Components
{
    public interface IMissionControl
    {
        void BeginMission();
        int GetNumberOfRovers();
        void CreateRovers(int numberOfRovers);
        StringBuilder ReportFinalPositions();
        bool IsPositionOccupied(IPosition position, int id);
    }
}