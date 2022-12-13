using System.Collections.Generic;
using FluentAssertions;
using ToyRobot;
using NSubstitute;
using ToyRobot.Enum;
using ToyRobot.Interfaces;
using Xunit;

namespace ToyRobotTest
{
    public class RobotTests
    {
        private readonly Robot _robot;
        private readonly IGrid _grid;
        
        public RobotTests()
        {
            _grid = Substitute.For<IGrid>();
            _robot = new Robot(_grid, "🤖");
        }

        [Fact]
        public void Report_ReturnsStringCurrentCoordinates_AndFacingPosition()
        {
            //Arrange
            var coordinates = new Coordinates {X = 0, Y = 1};
            _grid.UpdateBoard(Arg.Any<Coordinates>(), Arg.Any<string>()).Returns(Status.Ok);
            _robot.Place(coordinates, Position.North);
            //Act and Assert
            Assert.Contains("0,1,NORTH", _robot.Report());
        }
        
        [Fact]
        public void Report_ReturnsNull_WhenFacingPosition_Null()
        {
            //Arrange
            var coordinates = new Coordinates {X = 0, Y = 1};
            _grid.UpdateBoard(Arg.Any<Coordinates>(), Arg.Any<string>()).Returns(Status.Ok);
            _robot.Place(coordinates, null);
            //Act and Assert
            Assert.Null(_robot.Report());
        }
        
        [Fact]
        public void Report_ReturnsNull_WhenCurrentCoordinates_Null()
        {
            //Arrange
            var coordinates = new Coordinates {X = 0, Y = 1};
            _grid.UpdateBoard(Arg.Any<Coordinates>(), Arg.Any<string>()).Returns(Status.Ok);
            _robot.Place(coordinates, null);
            //Act and Assert
            Assert.Null(_robot.Report());
        }
        
        [Fact]
        public void Place_UpdatesRobotStatusToError_WhenGridUpdateError()
        {
            //Arrange
            var coordinates = new Coordinates {X = 0, Y = 1};
            _grid.UpdateBoard(Arg.Any<Coordinates>(), Arg.Any<string>()).Returns(Status.Error);
            //Act
            _robot.Place(coordinates, Position.North);
            //Assert
            Assert.Equal(Status.Error, _robot.Status);
        }
        
        [Fact]
        public void Place_UpdatesRobotStatusToOk_WhenGridUpdateSuccess()
        {
            //Arrange
            var coordinates = new Coordinates {X = 0, Y = 1};
            _grid.UpdateBoard(Arg.Any<Coordinates>(), Arg.Any<string>()).Returns(Status.Ok);
            //Act
            _robot.Place(coordinates, Position.North);
            //Assert
            Assert.Equal(Status.Ok, _robot.Status);
        }
        
        [Fact]
        public void Place_UpdatesRobotsCoordinates()
        {
            //Arrange
            var coordinates = new Coordinates {X = 0, Y = 1};
            _grid.UpdateBoard(Arg.Any<Coordinates>(), Arg.Any<string>()).Returns(Status.Ok);
            //Act
            _robot.Place(coordinates, Position.North);
            //Assert
            Assert.Equal(0, _robot.GetCurrentCoordinates().X);
            Assert.Equal(1, _robot.GetCurrentCoordinates().Y);
        }
        
        [Fact]
        public void Place_UpdatesRobotsFacingPosition()
        {
            //Arrange
            var coordinates = new Coordinates {X = 0, Y = 1};
            _grid.UpdateBoard(Arg.Any<Coordinates>(), Arg.Any<string>()).Returns(Status.Ok);
            //Act
            _robot.Place(coordinates, Position.North);
            //Assert
            Assert.Equal(Position.North, _robot.GetCurrenFacingPosition());
        }
        
        [Fact]
        public void Place_WillNotUpdateRobotsCoordinates_WhenUpdateBoardReturnsErrorStatus()
        {
            //Arrange
            var coordinates = new Coordinates {X = 0, Y = 1};
            _grid.UpdateBoard(Arg.Any<Coordinates>(), Arg.Any<string>()).Returns(Status.Error);
            //Act
            _robot.Place(coordinates, Position.North);
            //Assert
            Assert.Null(_robot.GetCurrentCoordinates());
        }
        
        [Fact]
        public void Place_WillNotUpdateRobotsFacingPosition_WhenUpdateBoardReturnsErrorStatus()
        {
            //Arrange
            var coordinates = new Coordinates {X = 0, Y = 1};
            _grid.UpdateBoard(Arg.Any<Coordinates>(), Arg.Any<string>()).Returns(Status.Error);
            //Act
            _robot.Place(coordinates, Position.South);
            //Assert
            Assert.Null(_robot.GetCurrenFacingPosition());
        }
        
        [Theory]
        [MemberData(nameof(GetMoveData))]
        public void Move_WillMoveTheRobotOneUnitForwardInDirectionItIsCurrentlyFacing(Position position, Coordinates expectedCoordinates)
        {
            //Arrange
            var coordinates = new Coordinates {X = 2, Y = 2};
            _grid.UpdateBoard(Arg.Any<Coordinates>(), Arg.Any<string>()).Returns(Status.Ok);
            _robot.Place(coordinates, position);
            //Act
            _robot.Move();
            //Assert
            expectedCoordinates.Should().BeEquivalentTo(_robot.GetCurrentCoordinates());
        }
        
        [Fact]
        public void Move_WillBeIgnored_IfRobotNotOnBoard()
        {
            //Act
            _robot.Move();
            //Assert
            Assert.Null(_robot.GetCurrentCoordinates());
        }
        
        [Theory]
        [MemberData(nameof(GetLeftFacingData))]
        public void Left_WillRotateTheRobot_LeftNinetyDegrees_WithoutChangingRobotsPosition(Position currentPosition, Position expectedNextPosition)
        {    
            //Arrange
            var coordinates = new Coordinates {X = 0, Y = 0};
            _grid.UpdateBoard(Arg.Any<Coordinates>(), Arg.Any<string>()).Returns(Status.Ok);
            _robot.Place(coordinates, currentPosition);
            //Act
            _robot.Left();
            //Assert
            _robot.GetCurrentCoordinates().Should().BeEquivalentTo(coordinates);
            Assert.Equal(expectedNextPosition, _robot.GetCurrenFacingPosition());
        }
        
        [Fact]
        public void Left_WillBeIgnored_IfRobotNotOnBoard()
        {
            //Act
            _robot.Left();
            //Assert
            Assert.Null(_robot.GetCurrenFacingPosition());
        }
        
        [Theory]
        [MemberData(nameof(GetRightFacingData))]
        public void Right_WillRotateTheRobot_RightNinetyDegrees_WithoutChangingRobotsPosition(Position currentPosition, Position expectedNextPosition)
        {    
            //Arrange
            var coordinates = new Coordinates {X = 0, Y = 0};
            _grid.UpdateBoard(Arg.Any<Coordinates>(), Arg.Any<string>()).Returns(Status.Ok);
            _robot.Place(coordinates, currentPosition);
            //Act
            _robot.Right();
            //Assert
            _robot.GetCurrentCoordinates().Should().BeEquivalentTo(coordinates);
            Assert.Equal(expectedNextPosition, _robot.GetCurrenFacingPosition());
        }
        
        [Fact]
        public void Right_WillBeIgnored_IfRobotNotOnBoard()
        {
            //Act
            _robot.Right();
            //Assert
            Assert.Null(_robot.GetCurrenFacingPosition());
        }

        public static IEnumerable<object[]> GetLeftFacingData()
        {
            return new List<object[]>
            {
                new object[] {Position.North, Position.West},
                new object[] {Position.West, Position.South},
                new object[] {Position.South, Position.East},
                new object[] {Position.East, Position.North},
            };
        }

        public static IEnumerable<object[]> GetRightFacingData()
        {
            return new List<object[]>
            {
                new object[] {Position.North, Position.East},
                new object[] {Position.East, Position.South},
                new object[] {Position.South, Position.West},
                new object[] {Position.West, Position.North},
            };
        }
        
        public static IEnumerable<object[]> GetMoveData()
        {
            return new List<object[]>
            {
                new object[] {Position.North, new Coordinates {X = 3, Y = 2}},
                new object[] {Position.East, new Coordinates {X = 2, Y = 3}},
                new object[] {Position.South, new Coordinates {X = 1, Y = 2}},
                new object[] {Position.West, new Coordinates {X = 2, Y = 1}},
            };
        }
    }
}