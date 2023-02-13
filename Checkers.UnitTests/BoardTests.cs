using Domain.Base.Classes;
using Domain.Base.Enums;
using Domain.Base.Struct;
using FluentAssertions;
using Shouldly;

namespace Checkers.UnitTests
{
    public class BoardTests
    {
        public BoardTests()
        {

        }

        [Theory]
        [InlineData('A', 3, CellColor.Black, CellPlace.WhiteChecker)]
        [InlineData('A', 7, CellColor.Black, CellPlace.BlackChecker)]
        [InlineData('A', 5, CellColor.Black, CellPlace.Empty)]
        [InlineData('G', 2, CellColor.White, CellPlace.Empty)]
        public void GetCell_CellExist_ShouldBeReturnCell(char width, int height, CellColor cellColor, CellPlace cellPlace)
        {
            //Arrange
            var board = new Board();

            //Act
            //Assert

            board.GetCell(width, height).ShouldBe(new Cell(cellColor, cellPlace, width, height));
            board.GetCell(width, height).ShouldBe(new Cell(cellColor, cellPlace, width, height));
            board.GetCell(width, height).ShouldBe(new Cell(cellColor, cellPlace, width, height));
            board.GetCell(width, height).ShouldBe(new Cell(cellColor, cellPlace, width, height));
        }

        [Theory]
        [InlineData('A', 9)]
        [InlineData('W', 9)]
        [InlineData('W', 0)]
        [InlineData('A', -1)]
        public void GetCell_CellDoesNotExist_ShouldBeThrowException(char width, int height)
        {
            //Arrange
            var board = new Board();

            //Act
            var action = () => board.GetCell(width, height);

            //Assert
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Copy_ShouldBeReturnCopyBoard()
        {
            //Arrange
            var board = new Board();

            //Act
            var result = board.Copy();

            //Assert
            result.Should().BeEquivalentTo(board);

            result.Should().NotBeSameAs(board);
        }

        [Fact]
        public void GetAllCheckerPaths_GetPathsFromStartingPositionChecker_Success()
        {
            ////Arrange
            //var board = new Board();

            ////Act
            //var c = new Cell(CellColor.Black, CellPlace.WhiteQueen, 3, 'C');
            //board.GetCell(3, 'C').UpdateCell(CellPlace.WhiteQueen);

            //var result = board.GetAllCheckerPaths(c);

            ////Assert
            //result.Count.ShouldBe(2);
        }

        [Theory]
        [InlineData('G', 3)]
        [InlineData('A', 8)]
        [InlineData('H', 8)]
        [InlineData('G', 7)]
        [InlineData('A', 1)]
        [InlineData('H', 1)]
        [InlineData('G', 2)]
        [InlineData('G', 1)]
        public void ValidateCoordinate_CorrectCoordinate_ReturnTrue(char width, int height)
        {
            //Arrange

            //Act
            var result = Board.ValidateCoordinate(width, height);

            //Assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData('H', 0)]
        [InlineData('H', -1)]
        [InlineData('H', 9)]
        [InlineData('A', 0)]
        [InlineData('A', -1)]
        [InlineData('A', 9)]
        //
        [InlineData('I', 1)]
        [InlineData('I', 0)]
        [InlineData('I', 8)]
        [InlineData('I', -1)]
        //
        [InlineData('i', 1)]
        [InlineData('a', 1)]
        [InlineData('b', 1)]
        [InlineData('c', 1)]

        [InlineData('1', 1)]
        [InlineData('1', 0)]
        public void ValidateCoordinate_IncorrectCoordinate_ReturnFalse(char width, int height)
        {
            //Arrange

            //Act
            var result = Board.ValidateCoordinate(width, height);

            //Assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData('A', 3, CellPlace.Empty)]
        [InlineData('B', 4, CellPlace.WhiteChecker)]
        [InlineData('B', 6, CellPlace.Empty)]
        public void UpdateCell_CellExist_CellShouldBeUpdated(char width, int height, CellPlace cellPlace)
        {
            //Arrange
            var board = new Board();

            //Act
            var location = new CheckerLocation(width, height);
            board.UpdateCell(location, cellPlace);

            //Assert
            board.GetCell(location).Checker.Should().Be(cellPlace);
        }
    }
}