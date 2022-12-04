using System.Collections.Generic;
using NSubstitute;
using ToyRobot;
using Xunit;

namespace ToyRobotTest
{
    public class GameTests
    {
        private readonly IGrid _grid;
        private readonly List<Robot> _robots;
        private readonly Game _game;
        private readonly IRobotMaker _robotMaker;

        public GameTests()
        {
            _grid = Substitute.For<IGrid>();
            _robotMaker = Substitute.For<IRobotMaker>();
            _game = new Game(_grid, _robotMaker);
            _robots = new List<Robot>();
        }

        [Fact]
        public void FirstCommand_GivenToRobot_MustBePlace()
        {
            var command = "MOVE";

            _game.ReadCommand(command);
            
            Assert.Null(_game.ActiveRobot);
        }

        [Fact]
        public void GivenCommand_ReadCommand_AsksActiveRobotToPerformMethod()
        {
            var robot = new Robot(_grid, "X") {Status = GridStatus.Ok};
            
            _robotMaker.CreateRobot(_grid, "1").Returns(robot);

            _robotMaker.Place(robot, Arg.Any<Coordinates>(), Arg.Any<Position>()).Returns(GridStatus.Ok);
            
            _game.ReadCommand("PLACE 0,0,NORTH");
            
            Assert.Equal(_game.ActiveRobot, robot);
        }
    }
}