using System;
using System.Collections.Generic;
using ToyRobot.Enum;
using ToyRobot.Interfaces;

namespace ToyRobot
{
    public class RobotController : IRobotController
    {
        public Robot CreateRobot(IGrid grid, string icon)
        {
            var robot = new Robot(grid, icon) {Name = $"Robot {icon}"};
            return robot;
        }

        public Status Place(IRobot robot, Coordinates coordinates, Position position)
        {
            robot.Place(coordinates, position);

            return robot.Status;
        }

        public string Report(IRobot activeRobot, IList<IRobot> robots)
        {
            return ( $"{robots.Count} present. Active Robot: {activeRobot.Name}.\nCurrent Position: {activeRobot.Report()}") ;
        }
        
        public void Read(string command, IRobot robot, List<IRobot> robots)
        {
            switch (command.ToUpper())
            {
                case "MOVE":
                    robot.Move();
                    break;
                case "LEFT":
                    robot.Left();
                    break;
                case "RIGHT":
                    robot.Right();
                    break;
                case "REPORT":
                    Console.WriteLine(Report(robot, robots));
                    break;
            }
        }
    }
}