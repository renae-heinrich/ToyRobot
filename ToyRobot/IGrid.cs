using System.Collections.Generic;

namespace ToyRobot
{
    public interface IGrid
    {
        List<List<string>> GetBoard();
        GridStatus UpdateBoard(Coordinates coordinates, string icon);
    }
}