using System.Collections.Generic;

namespace ToyRobot
{
    public interface IGrid
    {
        List<List<string>> GetBoard();
        Status UpdateBoard(Coordinates coordinates, string icon);
    }
}