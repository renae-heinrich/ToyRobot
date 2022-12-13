using System;
using System.Collections.Generic;
using System.Linq;

namespace ToyRobot
{
    public class Grid : IGrid
    {
        private readonly List<List<string>> _board;
        private readonly string _square;

        public Grid(int column, int row, string square)
        {
            _square = square;
            _board = new List<List<string>>();

            for (int i = 0; i < column; i++)
            {
                List<string> list1 = new List<string>();
                _board.Add(list1);

                for (int j = 0; j < row; j++)
                {
                    _board[i].Add(square);
                }
            }
        }

        public List<List<string>> GetBoard()
        {
            return  _board;
        }
        
        public Status UpdateBoard(Coordinates coordinates, string icon)
        {
            if (icon == _square)
            {
                return Status.Error;
            }

            var status = CheckStatus(coordinates);

            if (status == Status.Ok)
            {
                var playerIconList = GetBoard().FirstOrDefault(x => x.Contains(icon));
                if (playerIconList != null)
                {
                    var index = playerIconList.FindIndex(x => x.Contains(icon));
                    playerIconList[index] = _square;
                }
                GetBoard()[coordinates.X][coordinates.Y] = icon;
            }
            return status;
        }

        private Status CheckStatus(Coordinates coordinates)
        {
            string requestedSquare;
            try
            {
                requestedSquare = _board[coordinates.X][coordinates.Y];
            }
            catch (Exception e)
            {
                return Status.Error;
            }
            
            return requestedSquare != _square ? Status.Occupied : Status.Ok;
        }
    }
}