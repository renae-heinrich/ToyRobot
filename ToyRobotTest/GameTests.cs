using System.Collections.Generic;
using NSubstitute;
using ToyRobot;
using Xunit;

namespace ToyRobotTest
{
    public class GameTests
    {
        private readonly IGrid _grid;
        private readonly Game _game;
        private readonly IRobotController _robotController;

        public GameTests()
        {
            _grid = Substitute.For<IGrid>();
            _robotController = Substitute.For<IRobotController>();
            _game = new Game(_grid, _robotController);
        }
        
        [Fact]
        public void FirstCommand_GivenToRobot_MustBePlace()
        {
            var command = "MOVE";

            _game.ReadCommand(command);
            
            Assert.Null(_game.ActiveRobot);
        }

        [Fact]
        public void GivenValidCommand_ReadCommand_AsksActiveRobotToPerformMethod()
        {
            var robot = new Robot(_grid, "X") {Status = GridStatus.Ok};
            
            _robotController.CreateRobot(_grid, "1").Returns(robot);

            _robotController.Place(robot, Arg.Any<Coordinates>(), Arg.Any<Position>()).Returns(GridStatus.Ok);
            
            _game.ReadCommand("PLACE 0,0,NORTH");
            
            Assert.Equal(_game.ActiveRobot, robot);
        }
        
        [Fact]
        public void GivenInValidCommand_ReadCommand_ActiveRobotNull()
        {
            var robot = new Robot(_grid, "X") {};
            
            _robotController.CreateRobot(_grid, "1").Returns(robot);

            _robotController.Place(robot, Arg.Any<Coordinates>(), Arg.Any<Position>()).Returns(GridStatus.Error);
            
            _game.ReadCommand("PLACE 0,0,NORTH");
            
            Assert.Null(_game.ActiveRobot);
        }
        
        [Fact]
        public void GivenValidCommand_ReadCommand_CallsRobotController()
        {
            var robot = new Robot(_grid, "X") {Status = GridStatus.Ok};

            var command = "MOVE";
            
            _robotController.CreateRobot(_grid, "1").Returns(robot);

            _robotController.Place(robot, Arg.Any<Coordinates>(), Arg.Any<Position>()).Returns(GridStatus.Ok);
            
            _game.ReadCommand("PLACE 0,0,NORTH");
            
            _game.ReadCommand(command);
            
            _robotController.Received(1).Read(command, robot);
        }
    }
}