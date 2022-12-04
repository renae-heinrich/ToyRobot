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
        public IRobot ActiveRobot { get; set; }
        private IRobotController _robotController;

        public Game(IGrid grid, IRobotController robotController)
        {
            _grid = grid;
            _robots = new List<IRobot>();
            _robotController = robotController;
        }
        
        public void ReadCommand(string command)
        {
            var word = command.Split(" ").ToList();
            
            if (word.Count == 1 && ActiveRobot != null)
            {
                _robotController.Read(word[0], ActiveRobot);
            }

            if (word[0].ToUpper() == "PLACE" && word.Count == 2)
            {
               var robot = _robotController.CreateRobot(_grid, (_robots.Count + 1).ToString());
                
                var userInput = ConvertInput(word[1]);
                if (userInput != null)
                {
                    _robotController.Place(robot, userInput.Coordinates, userInput.Position);
                    
                    if (robot.Status == GridStatus.Ok)
                    {
                        ActiveRobot = robot;
                        _robots.Add(robot);
                    }
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
}