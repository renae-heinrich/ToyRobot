namespace ToyRobot
{
    public class RobotController : IRobotController
    {
        
        public Robot CreateRobot(IGrid grid, string icon)
        {
            return new Robot(grid, icon);
        }

        public GridStatus Place(IRobot robot, Coordinates coordinates, Position position)
        {
            robot.Place(coordinates, position);

            return robot.Status;
        }
        
        public void Read(string command, IRobot robot)
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
                    robot.Report();
                    break;
            }
        }
    }
}