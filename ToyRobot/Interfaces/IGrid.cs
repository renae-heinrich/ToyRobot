using System.Collections.Generic;
using ToyRobot.Enum;

namespace ToyRobot.Interfaces
{
    public interface IGrid
    {
        List<List<string>> GetBoard();
        Status UpdateBoard(Coordinates coordinates, string icon);
    }
}