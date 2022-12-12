using System;

namespace ToyRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            var grid = new Grid(5, 5, "◻️");
            
            var robotController = new RobotController();
            
            var game = new Game(grid, robotController);
            
            string command;

            do
            {
                command = Console.ReadLine();
                game.ReadCommand(command); 
            } while (command.ToUpper() != "STOP");
        }
    }
}