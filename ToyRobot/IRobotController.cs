namespace ToyRobot
{
    public interface IRobotController
    {
        public Robot CreateRobot(IGrid grid, string icon);
        public GridStatus Place(IRobot robot, Coordinates coordinates, Position position);
        public void Read(string command, IRobot robot);
    }
}