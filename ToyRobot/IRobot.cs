namespace ToyRobot
{
    public interface IRobot
    {
        Coordinates GetCurrentCoordinates();
        Position? GetCurrenFacingPosition();
        GridStatus Place(Coordinates coordinates, Position position);
        void Report();

        public void Move();
        public void Left();
        public void Right();
    }
}