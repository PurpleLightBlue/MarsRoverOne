using System;
using System.Linq;

namespace MarsRoverOne.Components
{
    public class Rover : IRover
    {
        public int Id { get; set; }
        public IExplorationArea ExplorationArea { get; }
        public IPosition RoverPosition { get; set; }
        public string CommandString { get; set; }

        public Rover(int id, IExplorationArea explorationArea, IPosition position)
        {
            Id = id;
            ExplorationArea = explorationArea ?? throw new ArgumentNullException(nameof(explorationArea));
            RoverPosition = position ?? throw new ArgumentNullException(nameof(position));
        }

        public Rover(int id, IExplorationArea explorationArea)
        {
            Id = id;
            ExplorationArea = explorationArea ?? throw new ArgumentNullException(nameof(explorationArea));
        }

        public IPosition TheoreticalMovementExecution(string instructionsToBeTested)
        {
            if (instructionsToBeTested == null)
            {
                throw new ArgumentNullException(nameof(instructionsToBeTested));
            }

            var arrayOfCommands = instructionsToBeTested.ToCharArray();
            var commandList = arrayOfCommands
                .Select(commandChar => (Command) Enum.Parse(typeof(Command), commandChar.ToString()))
                .ToList();
            var theoreticalPosition = new Position
            {
                XAxis = RoverPosition.XAxis, YAxis = RoverPosition.YAxis, Direction = RoverPosition.Direction
            };
            foreach (var command in commandList)
            {
                ExecuteCommand(command, theoreticalPosition);
            }

            return theoreticalPosition;
        }

        public void ExecuteMovement()
        {
            ExecuteMovement(CommandString);
        }

        public void ExecuteMovement(string movementInput)
        {
            if (movementInput == null)
            {
                throw new ArgumentNullException(nameof(movementInput));
            }

            var arrayOfCommands = movementInput.ToCharArray();
            var commandList = arrayOfCommands
                .Select(commandChar => (Command) Enum.Parse(typeof(Command), commandChar.ToString()))
                .ToList();

            foreach (var command in commandList)
            {
                ExecuteCommand(command, RoverPosition);
            }
        }

        public void CheckForOtherRovers()
        {
            throw new NotImplementedException();
        }

        public string ReportPosition()
        {
            return RoverPosition.GetConcatenatedPositionString();
        }

        private void ExecuteCommand(Command command, IPosition position)
        {
            //first check if command is a M - move or R/L - turn command
            if (command == Command.M)
            {
                var tempXAxisValue = RoverPosition.XAxis;
                var tempYAxisValue = RoverPosition.YAxis;
                //check direction facing in order to know where we are moving to and update the position reference accordingly
                switch (RoverPosition.Direction)
                {
                    case Direction.N:
                        if ((tempYAxisValue += 1) > ExplorationArea.YAxisMaxValue)
                        {
                            throw new Exception("Movement outside of exploration area");
                        }

                        position.YAxis += 1;
                        break;
                    case Direction.S:
                        if ((tempYAxisValue -= 1) < 0)
                        {
                            throw new Exception("Movement outside of exploration area");
                        }

                        position.YAxis -= 1;
                        break;
                    case Direction.E:
                        if ((tempXAxisValue += 1) > ExplorationArea.XAxisMaxValue)
                        {
                            throw new Exception("Movement outside of exploration area");
                        }

                        position.XAxis += 1;
                        break;
                    case Direction.W:
                        if ((tempXAxisValue -= 1) < 0)
                        {
                            throw new Exception("Movement outside of exploration area");
                        }

                        position.XAxis -= 1;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                switch (RoverPosition.Direction)
                {
                    case Direction.N:
                        position.Direction = command == Command.L ? Direction.W : Direction.E;
                        break;
                    case Direction.S:
                        position.Direction = command == Command.L ? Direction.E : Direction.W;
                        break;
                    case Direction.E:
                        position.Direction = command == Command.L ? Direction.N : Direction.S;
                        break;
                    case Direction.W:
                        position.Direction = command == Command.L ? Direction.S : Direction.N;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}