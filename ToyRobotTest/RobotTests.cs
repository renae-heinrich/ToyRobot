using System;
using System.Collections.Generic;
using FluentAssertions;
using ToyRobot;
using NSubstitute;
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
        public void Place_ReturnsNull_WhenGridUpdateError()
        {
            var coordinates = new Coordinates {X = 0, Y = 1};
            
            _grid.UpdateBoard(Arg.Any<Coordinates>(), Arg.Any<string>()).Returns(GridStatus.Error);
            
            var result = _robot.Place(coordinates, Position.North);
            
            Assert.Null(result);
        }
        
        [Fact]
        public void Place_UpdatesRobotsCoordinates()
        {
            var coordinates = new Coordinates {X = 0, Y = 1};
            
            _grid.UpdateBoard(Arg.Any<Coordinates>(), Arg.Any<string>()).Returns(GridStatus.Ok);
            
            _robot.Place(coordinates, Position.North);
            
            Assert.Equal(0, _robot.GetCurrentCoordinates().X);
            Assert.Equal(1, _robot.GetCurrentCoordinates().Y);
        }
        
        [Fact]
        public void Place_UpdatesRobotsFacingPosition()
        {
            var coordinates = new Coordinates {X = 0, Y = 1};
            
            _grid.UpdateBoard(Arg.Any<Coordinates>(), Arg.Any<string>()).Returns(GridStatus.Ok);
            
            _robot.Place(coordinates, Position.North);
            
            Assert.Equal(Position.North, _robot.GetCurrenFacingPosition());
        }
        
        [Fact]
        public void Place_WillNotUpdateRobotsCoordinates_WhenUpdateBoardReturnsErrorStatus()
        {
            var coordinates = new Coordinates {X = 0, Y = 1};
            _grid.UpdateBoard(Arg.Any<Coordinates>(), Arg.Any<string>()).Returns(GridStatus.Error);
            
            _robot.Place(coordinates, Position.North);
            
            Assert.Null(_robot.GetCurrentCoordinates());
        }
        
        [Fact]
        public void Place_WillNotUpdateRobotsFacingPosition_WhenUpdateBoardReturnsErrorStatus()
        {
            var coordinates = new Coordinates {X = 0, Y = 1};
           
            _grid.UpdateBoard(Arg.Any<Coordinates>(), Arg.Any<string>()).Returns(GridStatus.Error);
            
            _robot.Place(coordinates, Position.South);

            Assert.Null(_robot.GetCurrenFacingPosition());
        }
        
        [Theory]
        [MemberData(nameof(GetMoveData))]
        public void Move_WillMoveTheRobotOneUnitForwardInDirectionItIsCurrentlyFacing(Position position, Coordinates expectedCoordinates)
        {
            var coordinates = new Coordinates {X = 2, Y = 2};
            
            _grid.UpdateBoard(Arg.Any<Coordinates>(), Arg.Any<string>()).Returns(GridStatus.Ok);

            _robot.Place(coordinates, position);
            _robot.Move();

            expectedCoordinates.Should().BeEquivalentTo(_robot.GetCurrentCoordinates());
        }
        
        [Fact]
        public void Move_WillBeIgnored_IfRobotNotOnBoard()
        {
            _robot.Move();
            
            Assert.Null(_robot.GetCurrentCoordinates());
        }
        
        [Theory]
        [MemberData(nameof(GetLeftFacingData))]
        public void Left_WillRotateTheRobot_LeftNinetyDegrees_WithoutChangingRobotsPosition(Position currentPosition, Position expectedNextPosition)
        {    
            var coordinates = new Coordinates {X = 0, Y = 0};
            
            _grid.UpdateBoard(Arg.Any<Coordinates>(), Arg.Any<string>()).Returns(GridStatus.Ok);
            
            _robot.Place(coordinates, currentPosition);
            _robot.Left();

            _robot.GetCurrentCoordinates().Should().BeEquivalentTo(coordinates);
            Assert.Equal(expectedNextPosition, _robot.GetCurrenFacingPosition());
        }
        
        [Fact]
        public void Left_WillBeIgnored_IfRobotNotOnBoard()
        {
            _robot.Left();
            
            Assert.Null(_robot.GetCurrenFacingPosition());
        }
        
        [Theory]
        [MemberData(nameof(GetRightFacingData))]
        public void Right_WillRotateTheRobot_RightNinetyDegrees_WithoutChangingRobotsPosition(Position currentPosition, Position expectedNextPosition)
        {    
            var coordinates = new Coordinates {X = 0, Y = 0};
            _grid.UpdateBoard(Arg.Any<Coordinates>(), Arg.Any<string>()).Returns(GridStatus.Ok);
            
            _robot.Place(coordinates, currentPosition);
            _robot.Right();

            _robot.GetCurrentCoordinates().Should().BeEquivalentTo(coordinates);
            Assert.Equal(expectedNextPosition, _robot.GetCurrenFacingPosition());
        }
        
        [Fact]
        public void Right_WillBeIgnored_IfRobotNotOnBoard()
        {
            _robot.Right();
            
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