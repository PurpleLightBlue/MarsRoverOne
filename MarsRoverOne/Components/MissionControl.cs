using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsRoverOne.Components
{
    public sealed class MissionControl : IMissionControl
    {
        public Dictionary<int, IPosition> RoverPositionDictionary { get; private set; }
        public List<IRover> RoverList { get; private set; }
        public IExplorationArea ExplorationArea { get; set; }
        public int XAxisMaximum { get; private set; }
        public int YAxisMaximum { get; private set; }
        public int NumberOfRovers { get; private set; }

        private static readonly Lazy<MissionControl> Lazy = new Lazy<MissionControl>(() => new MissionControl());

        public static MissionControl Instance => Lazy.Value;

        private MissionControl()
        {
            RoverPositionDictionary = new Dictionary<int, IPosition>();
            RoverList = new List<IRover>();
        }

        public void BeginMission()
        {
            Console.WriteLine("Welcome to Mars Rover Mission Control");
            while (true)
            {
                Console.WriteLine("Please provide the Exploration Area maximum X axis value");
                var xAxisString = Console.ReadLine()?.Trim();
                if (!int.TryParse(xAxisString, out var xAxisInt))
                {
                    continue;
                }

                XAxisMaximum = xAxisInt;
                break;
            }

            while (true)
            {
                Console.WriteLine("Please provide the Exploration Area maximum Y axis value");
                var yAxisString = Console.ReadLine()?.Trim();
                if (!int.TryParse(yAxisString, out var yAxisInt))
                {
                    continue;
                }

                YAxisMaximum = yAxisInt;
                break;
            }

            ExplorationArea = new ExplorationArea(XAxisMaximum, YAxisMaximum);
            ExplorationArea.ConstructExplorationArea();

            Console.WriteLine("Now please indicate how many Rovers to deploy?");

            while (true)
            {
                var numberOfRovers = Console.ReadLine()?.Trim();
                if (!int.TryParse(numberOfRovers, out var numberOfRoversInt))
                {
                    Console.WriteLine(
                        "Invalid character entered, please enter a valid number of Rovers to be deployed, the maximum is 10 and minimum is 1");
                    continue;
                }

                if (numberOfRoversInt <= 0 || numberOfRoversInt >= 10)
                {
                    Console.WriteLine(
                        "Please enter a valid number of Rovers to be deployed, the maximum is 10 and minimum is 1");
                    continue;
                }

                NumberOfRovers = numberOfRoversInt;
                break;
            }

            CreateRovers(NumberOfRovers);

            var roverCounter = 0;
            foreach (var rover in RoverList)
            {
                IPosition tempPosition;
                while (true)
                {
                    Console.WriteLine(
                        $"For Rover {roverCounter} please provide starting co-ordinates and cardinal direction facing.");
                    var roverStartPosition = Console.ReadLine()?.Trim();
                    if (roverStartPosition == null)
                    {
                        Console.WriteLine($"Input Characters cannot be null.");
                        continue;
                    }

                    if (roverStartPosition == string.Empty)
                    {
                        Console.WriteLine($"Input Characters cannot be blank.");
                        continue;
                    }

                    if (!roverStartPosition.Contains(' '))
                    {
                        Console.WriteLine($"Input Characters must be separated by a space character.");
                        continue;
                    }

                    var roverStartPositionStringArray = roverStartPosition.Split(' ');
                    if (roverStartPositionStringArray.Length > 3)
                    {
                        Console.WriteLine(
                            $"Too many position values provided. The expected format is '<XAxis> <YAxis> <Cardinal Direction>'");
                        continue;
                    }

                    if (roverStartPositionStringArray.Length > 3)
                    {
                        Console.WriteLine(
                            "Too few values provided. The expected format is '<XAxis> <YAxis> <Cardinal Direction>'");
                        continue;
                    }

                    if (!int.TryParse(roverStartPositionStringArray[0], out var tempXAxis))
                    {
                        Console.WriteLine("XAxis Value is not a valid integer");
                        continue;
                    }

                    if (!int.TryParse(roverStartPositionStringArray[1], out var tempYAxis))
                    {
                        Console.WriteLine("YAxis Value is not a valid integer");
                        continue;
                    }

                    if (!Enum.TryParse(typeof(Direction), roverStartPositionStringArray[2], true, out var result))
                    {
                        Console.WriteLine("Cardinal Direction provided is not valid.");
                        continue;
                    }

                    tempPosition = new Position
                    {
                        XAxis = Convert.ToInt32(roverStartPositionStringArray[0]),
                        YAxis = Convert.ToInt32(roverStartPositionStringArray[1]),
                        Direction = (Direction) Enum.Parse(typeof(Direction),
                            roverStartPositionStringArray[2].ToString())
                    };
                    if (IsPositionOccupied(tempPosition, rover.Id))
                    {
                        Console.WriteLine(
                            $"Rover {roverCounter} cannot start from here, these coordinates are already taken by another rover.");
                        continue;
                    }

                    break;
                }

                rover.RoverPosition = tempPosition;
                RoverPositionDictionary.Add(rover.Id, tempPosition);

                string roverCommand;
                while (true)
                {
                    Console.WriteLine($"For Rover {roverCounter} please provide movement instructions");
                    roverCommand = Console.ReadLine()?.Trim();
                    if (roverCommand == null)
                    {
                        Console.WriteLine($"Command cannot be null.");
                        continue;
                    }

                    if (roverCommand == string.Empty)
                    {
                        Console.WriteLine($"Command cannot be empty.");
                        continue;
                    }

                    var roverCommandCharArray = roverCommand.ToCharArray();
                    var commandValid = true;
                    foreach (var command in roverCommandCharArray)
                    {
                        if (char.IsDigit(command))
                        {
                            Console.WriteLine($"Command cannot be a number: {command}");
                            commandValid = false;
                            continue;
                        }

                        if (!Enum.TryParse(typeof(Command), command.ToString(), true, out var result))
                        {
                            Console.WriteLine($"Invalid command character provided: {command}");
                            commandValid = false;
                            continue;
                        }
                    }

                    if (!commandValid)
                    {
                        continue;
                    }

                    break;
                }

                roverCounter++;
                rover.CommandString = roverCommand;
            }

            Console.WriteLine($"Start rovers moving?");
            Console.ReadKey();

            var roverToMoveCounter = 0;
            foreach (var roverToMove in RoverList)
            {
                Console.WriteLine($"Rover {roverToMoveCounter} is moving.");
                var theoreticalPositionOfWhereRoverMovesTo =
                    roverToMove.TheoreticalMovementExecution(roverToMove.CommandString);
                if (IsPositionOccupied(theoreticalPositionOfWhereRoverMovesTo, roverToMove.Id))
                {
                    Console.WriteLine(
                        $"Proposed movement would put rover {roverToMoveCounter} into a final position occupied by another rover. Movement aborted.");
                    continue;
                }

                roverToMove.ExecuteMovement();
                RoverPositionDictionary[roverToMove.Id] = roverToMove.RoverPosition;
                Console.WriteLine($"Rover {roverToMoveCounter} final position {roverToMove.ReportPosition()}");
                roverToMoveCounter++;
            }

            Console.WriteLine(ReportFinalPositions());
        }

        public int GetNumberOfRovers()
        {
            return RoverList.Count();
        }

        public void CreateRovers(int numberOfRovers)
        {
            for (var roverCounter = 0; roverCounter < numberOfRovers; roverCounter++)
            {
                var rover = new Rover(roverCounter, ExplorationArea);
                RoverList.Add(rover);
            }
        }

        public bool IsPositionOccupied(IPosition position, int roverId)
        {
            var trimmedDictionary = RoverPositionDictionary.Where(dict => dict.Key != roverId);
            var matches = trimmedDictionary.Where(dict =>
                dict.Value.YAxis == position.YAxis && dict.Value.XAxis == position.XAxis);
            return matches.Any();
        }

        public StringBuilder ReportFinalPositions()
        {
            var resultStringBuilder = new StringBuilder();
            resultStringBuilder.Append("Final positions of all rovers: ");
            resultStringBuilder.Append(Environment.NewLine);

            foreach (var rover in RoverList)
            {
                resultStringBuilder.Append(rover.ReportPosition());
                resultStringBuilder.Append(Environment.NewLine);
            }

            return resultStringBuilder;
        }
    }
}