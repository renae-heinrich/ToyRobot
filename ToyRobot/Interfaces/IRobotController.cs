using System.Collections.Generic;
using ToyRobot.Enum;

namespace ToyRobot.Interfaces
{
    public interface IRobotController
    {
        public Robot CreateRobot(IGrid grid, string icon);
        public Status Place(IRobot robot, Coordinates coordinates, Position position);
        public void Read(string command, IRobot robot, List<IRobot> robots);
        public string Report(IRobot activeRobot, IList<IRobot> robots);
    }
}