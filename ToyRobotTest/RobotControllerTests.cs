using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using ToyRobot;
using Xunit;

namespace ToyRobotTest
{
    public class RobotControllerTests
    {
        private readonly RobotController _robotController;
        private readonly IGrid _grid;
        private readonly IRobot _robot;
        
        public RobotControllerTests()
        {
            _robotController = new RobotController();
            _robot = Substitute.For<IRobot>();
            _grid = Substitute.For<IGrid>();
        }

        [Fact]
        public void CreateRobot_ReturnsNewRobot_WithNameOfRobotPlusProvidedIcon()
        {
            //Act
            var actual =_robotController.CreateRobot(_grid, "X");
            //Assert
            actual.Should().BeEquivalentTo(new Robot(_grid, "X") {Name = "Robot X"});
        }
        
        [Fact]
        public void Place_ShouldCallRobotPlace()
        {
            //Arrange
            var coordinates = new Coordinates {X = 1, Y = 1};
           //Act
            _robotController.Place(_robot, coordinates, Position.East);
            //Assert
            _robot.Received().Place(Arg.Any<Coordinates>(), Arg.Any<Position>());
        }
        
        [Fact]
        public void Read_ShouldCallRobotMove_WhenCommandMove()
        {
            //Act
            _robotController.Read("move", _robot, new List<IRobot>());
            //Assert
            _robot.Received().Move();
        }
        
        [Fact]
        public void Read_ShouldCallRobotLeft_WhenCommandLeft()
        {
            //Act
            _robotController.Read("left", _robot, new List<IRobot>());
            //Assert
            _robot.Received().Left();
        }
        
        [Fact]
        public void Read_ShouldCallRobotRight_WhenCommandRight()
        {
            //Act
            _robotController.Read("right", _robot, new List<IRobot>());
            //Assert
            _robot.Received().Right();
        }
        
        [Fact]
        public void Report_ShouldReportHowManyRobotsPresent()
        {
            //Act
            var report = _robotController.Report(_robot, new List<IRobot>{_robot});
            //Assert
            Assert.Contains("1 present", report);
        }
        
        [Fact]
        public void Report_ShouldReportWhichRobotIsActiveRobot()
        {
            //Arrange
            var activeRobot = new Robot(_grid, "Active") {Name = "Robot Active"};
            //Act
            var report = _robotController.Report(activeRobot, new List<IRobot>{_robot, activeRobot});
            //Assert
            Assert.Contains(activeRobot.Name, report);
        }
        
        [Fact]
        public void Report_ShouldReportCallRobotReport()
        {
            //Act
            _robotController.Report(_robot, new List<IRobot>{_robot, _robot});
            //Assert
            _robot.Received().Report();
        }
    }
}