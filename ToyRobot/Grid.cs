using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ToyRobot
{
    public class Grid : IGrid
    {
        private readonly List<List<string>> _board;
        private int _row;
        private int _column;
        private readonly string _square;

        public Grid(int column, int row, string square)
        {
            _row = row;
            _square = square;
            _column = column;
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
        
        public GridStatus UpdateBoard(Coordinates coordinates, string icon)
        {
            if (icon == _square)
            {
                return GridStatus.Error;
            }

            var status = CheckStatus(coordinates);

            if (status == GridStatus.Ok)
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

        private GridStatus CheckStatus(Coordinates coordinates)
        {
            string requestedSquare;
            try
            {
                requestedSquare = _board[coordinates.X][coordinates.Y];
            }
            catch (Exception e)
            {
                return GridStatus.Error;
            }
            
            return requestedSquare != _square ? GridStatus.Occupied : GridStatus.Ok;
        }
    }
}