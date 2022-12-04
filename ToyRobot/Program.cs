using System;
using System.Collections.Generic;
using System.Linq;

namespace ToyRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            var grid = new Grid(5, 5, "◻️");

            var robot1 = new Robot(grid, "X");
            
            // //ensure  Y is the first input based on the requirements
            // robot1.Place(new Coordinates
            // {
            //     Y=1,
            //     X=2,
            // }, Position.East);
            // robot1.Move();
            // robot1.Move();
            // robot1.Left();
            // robot1.Move();
            // robot1.Report();

            var robotMaker = new RobotMaker();
            
            var game = new Game(grid, robotMaker);
            
            
            //PLACE will add a new robot to the table with incrementing number identifier
            
            game.ReadCommand("PLACE 1,2,EAST");
            
            
            game.ReadCommand("MOVE");
            game.ReadCommand("MOVE");
            game.ReadCommand("LEFT");
            game.ReadCommand("MOVE");
            game.ReadCommand("REPORT");
            
        }
    }
}