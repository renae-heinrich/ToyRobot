using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ToyRobot
{
    public class Game
    {
        private IGrid _grid;
        private List<IRobot> _robots;
        private IRobot _activeRobot;
        private IRobotMaker _robotMaker;
        private IRobot _robot;

        public Game(IGrid grid, IRobotMaker robotMaker)
        {
            _grid = grid;
            _robots = new List<IRobot>();
            _activeRobot = null;
            _robotMaker = robotMaker;
            _robot = null;
        }
        
        public void ReadCommand(string command)
        {
            var word = command.Split(" ").ToList();
            
            //need to ensure place first command
            if (word.Count == 1)
            {
                switch (word[0].ToUpper())
                {
                    case "MOVE":
                        _activeRobot.Move();
                        break;
                    case "LEFT":
                        _activeRobot.Left();
                        break;
                    case "RIGHT":
                        _activeRobot.Right();
                        break;
                    case "REPORT":
                        _activeRobot.Report();
                        break;
                }
            }

            if (word[0].ToUpper() == "PLACE" && word.Count == 2)
            {
                //need to ask someone to 
                //1. create a new robot
                
                
                // _robot = _robotMaker.CreateRobot(_grid, (_robots.Count + 1).ToString());
                
                var userInput = ConvertInput(word[1]);
                if (userInput != null)
                {
                    var robot = _robotMaker.Place(userInput.Coordinates, userInput.Position, _grid,
                        (_robots.Count + 1).ToString());
                    // var status = _robot.Place(userInput.Coordinates, userInput.Position);

                    if (robot != null)
                    {
                        _activeRobot = robot;
                        _robots.Add(robot);
                    }
                    
                    // if (status == GridStatus.Ok)
                    // {
                    //     _activeRobot = _robot;
                    //     _robots.Add(_robot);
                    // }
                }  
            }
        }
        
        private static Input ConvertInput(string command)
        {
            var listCommand = command.Split(",").ToList();
            var coordinates = GetCoordinates(listCommand);

            var facingPosition = ToTitleCase(listCommand[2]);

            if (Enum.TryParse(facingPosition, out Position position))
            {
                return new Input
                {
                    Position = position,
                    Coordinates = coordinates
                };
            }
            return null;
        }

        private static Coordinates GetCoordinates(List<string> listCommand)
        {
            var coords = listCommand.GetRange(0, 2).Select(s => int.Parse(s, System.Globalization.CultureInfo.InvariantCulture))
                .ToArray();

            var coordinates = new Coordinates
            {
                Y = coords[0],
                X = coords[1]
            };
            return coordinates;
        }

        private static string ToTitleCase(string word)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(word.ToLower());
        }
    }

    public class RobotMaker : IRobotMaker
    {
        
        public Robot CreateRobot(IGrid grid, string icon)
        {
            return new Robot(grid, icon);
        }

        public Robot Place(Coordinates coordinates, Position position, IGrid grid, string icon)
        {
            var robot = CreateRobot(grid, icon);

            var status = robot.Place(coordinates, position);

            return status == GridStatus.Ok ? robot : null;
        }
    }

    public interface IRobotMaker
    {
        public Robot CreateRobot(IGrid grid, string icon);
        public Robot Place(Coordinates coordinates, Position position, IGrid grid, string icon);
    }
}