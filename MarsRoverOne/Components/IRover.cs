using System;

namespace MarsRoverOne.Components
{
    public interface IRover
    {
        int Id { get; set; }
        IExplorationArea ExplorationArea { get; }
        IPosition RoverPosition { get; set; }
        string CommandString { get; set; }
        IPosition TheoreticalMovementExecution(string instructionsToBeTested);
        void ExecuteMovement();
        void ExecuteMovement(string movementInput);
        void CheckForOtherRovers();
        string ReportPosition();
    }
}