using System.Collections.Generic;

namespace ToyRobot
{
    public interface IRobot
    {
        Coordinates GetCurrentCoordinates();
        Position? GetCurrenFacingPosition();
        void Place(Coordinates coordinates, Position?  position);
        string Report();

        public void Move();
        public void Left();
        public void Right();

        public Status Status { get; set; }
        public string Name { get; set; }
    }
}