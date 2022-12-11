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
            var robot = new Robot(_grid, "X") {Status = Status.Ok};
            
            _robotController.CreateRobot(_grid, "1").Returns(robot);

            _robotController.Place(robot, Arg.Any<Coordinates>(), Arg.Any<Position>()).Returns(Status.Ok);
            
            _game.ReadCommand("PLACE 0,0,NORTH");
            
            Assert.Equal(_game.ActiveRobot, robot);
        }
        
        [Fact]
        public void GivenInValidCommand_ReadCommand_ActiveRobotNull()
        {
            var robot = new Robot(_grid, "X") {};
            
            _robotController.CreateRobot(_grid, "1").Returns(robot);

            _robotController.Place(robot, Arg.Any<Coordinates>(), Arg.Any<Position>()).Returns(Status.Error);
            
            _game.ReadCommand("PLACE 0,0,NORTH");
            
            Assert.Null(_game.ActiveRobot);
        }
        
        [Fact]
        public void GivenValidCommand_ReadCommand_CallsRobotController()
        {
            //Arrange
            var robot = new Robot(_grid, "X") {Status = Status.Ok, Name = "Robot 1"};
            var command = "MOVE";
            _robotController.CreateRobot(_grid, "1").Returns(robot);
            _robotController.Place(robot, Arg.Any<Coordinates>(), Arg.Any<Position>()).Returns(Status.Ok);
            _game.ReadCommand("PLACE 0,0,NORTH");
            
            //Act
            _game.ReadCommand(command);

            //Assert
            _robotController.Received(1).Read(command, robot, _game.Robots);
        }
        
        [Fact]
        public void GivenExistingRobot_ReadCommand_MakesRequestedRobotActive()
        {
            //Arrange
            var robot1 = new Robot(_grid, "X") {Status = Status.Ok, Name = "Robot 1"};
            var robot2 = new Robot(_grid, "X") {Status = Status.Ok, Name = "Robot 2"};
            _robotController.CreateRobot(_grid, "1").Returns(robot1);
            _robotController.CreateRobot(_grid, "2").Returns(robot2);
            _robotController.Place(robot1, Arg.Any<Coordinates>(), Arg.Any<Position>()).Returns(Status.Ok);
            _robotController.Place(robot2, Arg.Any<Coordinates>(), Arg.Any<Position>()).Returns(Status.Ok);

            _game.ReadCommand("PLACE 0,0,NORTH");
            _game.ReadCommand("PLACE 1,1,SOUTH");
            
            //Act
            _game.ReadCommand("ROBOT 1");

            //Assert
           Assert.Equal("Robot 1",_game.ActiveRobot.Name);
        }
        
        [Fact]
        public void GivenNonExistingRobot_ReadCommand_IgnoresActiveRobotRequest()
        {
            //Arrange
            var robot1 = new Robot(_grid, "X") {Status = Status.Ok, Name = "Robot 1"};
            var robot2 = new Robot(_grid, "X") {Status = Status.Ok, Name = "Robot 2"};
            _robotController.CreateRobot(_grid, "1").Returns(robot1);
            _robotController.CreateRobot(_grid, "2").Returns(robot2);
            _robotController.Place(robot1, Arg.Any<Coordinates>(), Arg.Any<Position>()).Returns(Status.Ok);
            _robotController.Place(robot2, Arg.Any<Coordinates>(), Arg.Any<Position>()).Returns(Status.Ok);

            _game.ReadCommand("PLACE 0,0,NORTH");
            _game.ReadCommand("PLACE 1,1,SOUTH");
            
            //Act
            _game.ReadCommand("ROBOT 3");

            //Assert
            Assert.Equal("Robot 2",_game.ActiveRobot.Name);
        }
    }
}