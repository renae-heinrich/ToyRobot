namespace ToyRobot
{
    public class Robot : IRobot
    {
        private readonly IGrid _grid;

        private readonly string _icon;

        private Coordinates _currentCoordinates;
        private Position? _facingPosition;
        
        public Status Status { get; set; }
        
        public string Name { get; set; }

        public Robot(IGrid grid, string icon)
        {
            _grid = grid;
            _icon = icon;
        }
        
        public void Place(Coordinates coordinates, Position position)
        {
            var status = _grid.UpdateBoard(coordinates, _icon);
            if (status == Status.Ok)
            {
                _facingPosition = position;
                _currentCoordinates = new Coordinates
                {
                    X = coordinates.X,
                    Y = coordinates.Y
                };
                Status = Status.Ok;
            }
            else
            {
                Status = Status.Error;
            }
        }
        
        public void Move()
        {
            if (_currentCoordinates == null)
            {
                return;
            }
            var newCoordinates = AssignCoordinates(_facingPosition);

            var status = _grid.UpdateBoard(newCoordinates, _icon);

            if (status == Status.Ok)
            {
                _currentCoordinates = newCoordinates;
            }
        }

        private Coordinates AssignCoordinates(Position? facingPosition)
        {
            var coordinates = new Coordinates();
            switch (facingPosition)
            {
                case Position.North:
                    coordinates.X = _currentCoordinates.X + 1;
                    coordinates.Y = _currentCoordinates.Y;
                    break;
                case Position.South:
                    coordinates.X = _currentCoordinates.X - 1;
                    coordinates.Y = _currentCoordinates.Y;
                    break;
                case Position.East:
                    coordinates.X = _currentCoordinates.X;
                    coordinates.Y = _currentCoordinates.Y + 1;
                    break;
                case Position.West:
                    coordinates.X = _currentCoordinates.X;
                    coordinates.Y = _currentCoordinates.Y - 1;
                    break;
            }
            return coordinates;
        }

        public Coordinates GetCurrentCoordinates()
        {
            return _currentCoordinates;
        }
        
        public Position? GetCurrenFacingPosition()
        {
            return _facingPosition;
        }
        
        public string GetIcon()
        {
            return _icon;
        }

        public void Left()
        {
            if (_currentCoordinates == null)
            {
                return;
            }
            Position? newFacingPosition = null;

            switch (_facingPosition)
            {
                case Position.North:
                    newFacingPosition = Position.West;
                    break;
                case Position.West:
                    newFacingPosition = Position.South;
                    break;
                case Position.South:
                    newFacingPosition = Position.East;
                    break;
                case Position.East:
                    newFacingPosition = Position.North;
                    break;
            }
            _facingPosition = newFacingPosition;
        }

        public void Right()
        {
            if (_currentCoordinates == null)
            {
                return;
            }
            Position? newFacingPosition = null;

            switch (_facingPosition)
            {
                case Position.North:
                    newFacingPosition = Position.East;
                    break;
                case Position.West:
                    newFacingPosition = Position.North;
                    break;
                case Position.South:
                    newFacingPosition = Position.West;
                    break;
                case Position.East:
                    newFacingPosition = Position.South;
                    break;
            }
            _facingPosition = newFacingPosition;
        }

        public string Report()
        {
            if (_facingPosition == null || _currentCoordinates == null)
            {
                return  null;
            }
            return ($"{_currentCoordinates.X},{_currentCoordinates.Y},{_facingPosition.ToString()?.ToUpper()}");
        }
    }
}