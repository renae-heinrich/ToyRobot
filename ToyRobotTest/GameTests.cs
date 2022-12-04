using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using ToyRobot;
using Xunit;

namespace ToyRobotTest
{
    public class GameTests
    {
        private readonly IGrid _grid;
        private readonly IRobot _activeRobot;
        private readonly List<Robot> _robots;
        private readonly Game _game;
        private readonly IRobotMaker _robotMaker;
        private readonly IRobot _robot;

        public GameTests()
        {
            _grid = Substitute.For<IGrid>();
            _activeRobot = Substitute.For<IRobot>();
            _robotMaker = Substitute.For<IRobotMaker>();
            _game = new Game(_grid, _robotMaker);
            _robots = new List<Robot>();
          
            _robot = Substitute.For<IRobot>();
        }

        [Fact]
        public void FirstCommand_GivenToRobot_MustBePlace()
        {
            var command = "MOVE";

            _game.ReadCommand(command);
            
            _activeRobot.DidNotReceive().Move();
        }

        [Fact]
        public void GivenCommand_ReadCommand_AsksActiveRobotToPerformMethod()
        {

            var robot = new Robot(_grid, "X");

            _robotMaker.CreateRobot(Arg.Any<Grid>(), Arg.Any<string>()).Returns(robot);


            _robotMaker.Place(Arg.Any<Coordinates>(), Arg.Any<Position>(), _grid, "X").Returns(robot);

            
            _game.ReadCommand("PLACE 0,0,NORTH");

            robot.Received(1).Place(new Coordinates {X = 0, Y = 0}, Position.North);


        }
        
    }
}