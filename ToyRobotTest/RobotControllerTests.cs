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
            var actual =_robotController.CreateRobot(_grid, "X");
            
            actual.Should().BeEquivalentTo(new Robot(_grid, "X") {Name = "Robot X"});
        }
        
        [Fact]
        public void Place_ShouldCallRobotPlace()
        {
            var coordinates = new Coordinates {X = 1, Y = 1};
           
            _robotController.Place(_robot, coordinates, Position.East);
            
            _robot.Received().Place(Arg.Any<Coordinates>(), Arg.Any<Position>());
        }
        
        [Fact]
        public void Read_ShouldCallRobotMove_WhenCommandMove()
        {
            _robotController.Read("move", _robot, new List<IRobot>());

            _robot.Received().Move();
        }
        
        [Fact]
        public void Read_ShouldCallRobotLeft_WhenCommandLeft()
        {
            _robotController.Read("left", _robot, new List<IRobot>());

            _robot.Received().Left();
        }
        
        [Fact]
        public void Read_ShouldCallRobotRight_WhenCommandRight()
        {
            _robotController.Read("right", _robot, new List<IRobot>());

            _robot.Received().Right();
        }
        
        [Fact]
        public void Report_ShouldReportHowManyRobotsPresent()
        {
            var report = _robotController.Report(_robot, new List<IRobot>{_robot});

            Assert.Contains("1 present", report);
        }
        
        [Fact]
        public void Report_ShouldReportWhichRobotIsActiveRobot()
        {
            var activeRobot = new Robot(_grid, "Active") {Name = "Robot Active"};

            var report = _robotController.Report(activeRobot, new List<IRobot>{_robot, activeRobot});

            Assert.Contains(activeRobot.Name, report);
        }
        
        [Fact]
        public void Report_ShouldReportCallRobotReport()
        {
            _robotController.Report(_robot, new List<IRobot>{_robot, _robot});

            _robot.Received().Report();
        }
    }
}