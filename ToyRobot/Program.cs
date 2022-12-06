namespace ToyRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            var grid = new Grid(5, 5, "◻️");
            
            var robotController = new RobotController();
            
            var game = new Game(grid, robotController);
            
            game.ReadCommand("PLACE 1,2,EAST");
            game.ReadCommand("PLACE 1,1,EAST");
            game.ReadCommand("PLACE 0,0,EAST");
            game.ReadCommand("PLACE 0,0,EAST");
            game.ReadCommand("MOVE");
            game.ReadCommand("MOVE");
            game.ReadCommand("LEFT");
            game.ReadCommand("MOVE");
            game.ReadCommand("REPORT");
        }
    }
}