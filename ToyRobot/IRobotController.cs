using System.Collections.Generic;

namespace ToyRobot
{
    public interface IRobotController
    {
        public Robot CreateRobot(IGrid grid, string icon);
        public Status Place(IRobot robot, Coordinates coordinates, Position position);
        public void Read(string command, IRobot robot, List<IRobot> robots);

        public void Report(IRobot activeRobot, IList<IRobot> robots);
    }
}