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
            //Act
            var actual = _grid.GetBoard();
            //Assert
            Assert.Equal(5, actual.Count);
            foreach (var row in actual)
            {
                Assert.Equal(3, row.Count);
            }
        }
        
        [Fact]
        public void UpdateBoard_UpdatesBoardWithGivenIcon_WhenProvidedValidCoordinates()
        {
            //Arrange
            var coordinates = new Coordinates
            {
                X = 0,
                Y = 0
            };
            //Act
            _grid.UpdateBoard(coordinates, "🤖");
            //Assert
            Assert.Equal("🤖" ,_grid.GetBoard()[0][0]);
        }
        
        [Fact]
        public void UpdateBoard_ReturnsErrorStatus_WhenProvidedInValidCoordinates()
        {
            //Arrange
            var coordinates = new Coordinates
            {
                X = 6,
                Y = 6
            };
            //Act and Assert
            Assert.Equal(Status.Error, _grid.UpdateBoard(coordinates, "🤖"));
        }
        
        [Fact]
        public void UpdateBoard_ReturnsOccupiedStatus_WhenProvidedInValidCoordinates()
        {
            //Arrange
            _grid.GetBoard()[1][1] = "🤖";
            var coordinates = new Coordinates
            {
                X = 1,
                Y = 1
            };
            //Act and Assert
            Assert.Equal(Status.Occupied, _grid.UpdateBoard(coordinates, "🤖"));
        }
        
        [Fact]
        public void UpdateBoard_ReturnsErrorStatus_WhenProvidedIconSameAsSquareIcons()
        {
            //Arrange
            _grid.GetBoard()[1][1] = "◻️";
            var coordinates = new Coordinates
            {
                X = 1,
                Y = 1
            };
            //Act and Assert
            Assert.Equal(Status.Error, _grid.UpdateBoard(coordinates, "◻️"));
        }
        
        [Fact]
        public void UpdateBoard_ResetsSquareToGridSquare_WhenRobotMovesToOtherLocation()
        {
            //Arrange
            var coordinates = new Coordinates
            {
                X = 0,
                Y = 0
            };
            _grid.UpdateBoard(coordinates, "🤖");
            //Act
            _grid.UpdateBoard(new Coordinates
            {
                X = 1,
                Y = 0
            }, "🤖");
            //Assert
            Assert.Equal("◻️" ,_grid.GetBoard()[0][0]);
        }
    }
}