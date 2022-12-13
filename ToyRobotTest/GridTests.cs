using System;
using ToyRobot;
using Xunit;

namespace ToyRobotTest
{
    public class GridTests
    {
        private readonly Grid _grid;

        public GridTests()
        {
            _grid = new Grid(5, 3, "◻️");
        }
        
        [Fact]
        public void GetGrid_ReturnsSpecifiedColumnAndRows()
        {
            var actual = _grid.GetBoard();
            
            Assert.Equal(5, actual.Count);

            foreach (var row in actual)
            {
                Assert.Equal(3, row.Count);
            }
        }
        
        [Fact]
        public void UpdateBoard_UpdatesBoardWithGivenIcon_WhenProvidedValidCoordinates()
        {
            var coordinates = new Coordinates
            {
                X = 0,
                Y = 0
            };
            
            _grid.UpdateBoard(coordinates, "🤖");
            
            Assert.Equal("🤖" ,_grid.GetBoard()[0][0]);
        }
        
        [Fact]
        public void UpdateBoard_ReturnsErrorStatus_WhenProvidedInValidCoordinates()
        {
            var coordinates = new Coordinates
            {
                X = 6,
                Y = 6
            };
            
            Assert.Equal(Status.Error, _grid.UpdateBoard(coordinates, "🤖"));
        }
        
        [Fact]
        public void UpdateBoard_ReturnsOccupiedStatus_WhenProvidedInValidCoordinates()
        {
            _grid.GetBoard()[1][1] = "🤖";
            var coordinates = new Coordinates
            {
                X = 1,
                Y = 1
            };
            
            Assert.Equal(Status.Occupied, _grid.UpdateBoard(coordinates, "🤖"));
        }
        
        [Fact]
        public void UpdateBoard_ReturnsErrorStatus_WhenProvidedIconSameAsSquareIcons()
        {
            _grid.GetBoard()[1][1] = "◻️";
            var coordinates = new Coordinates
            {
                X = 1,
                Y = 1
            };
            
            Assert.Equal(Status.Error, _grid.UpdateBoard(coordinates, "◻️"));
        }
    }
}