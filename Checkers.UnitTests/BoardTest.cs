using Domain.Base.Classes;
using Domain.Base.Enums;
using Domain.Base.Struct;
using FluentAssertions;
using Shouldly;

namespace Checkers.UnitTests
{
    public class BoardTest
    {
        public BoardTest()
        {

        }
        [Fact]
        public void GetCell_CellExist_ShouldBeReturnCell()
        {
            //Arrange
            var board = new Board();

            //Act
            //Assert

            board.GetCell(3, 'A').ShouldBe(new Cell(CellColour.Black, CellPlace.WhiteChecker, 'A', 3));
            board.GetCell(7, 'A').ShouldBe(new Cell(CellColour.Black, CellPlace.BlackChecker, 'A', 7));

            board.GetCell(2, 'A').ShouldBe(new Cell(CellColour.White, CellPlace.Empty, 'A', 5));
            board.GetCell(5, 'G').ShouldBe(new Cell(CellColour.Black, CellPlace.Empty, 'G', 2));
        }

        [Theory]
        [InlineData(9, 'A')]
        [InlineData(9, 'W')]
        [InlineData(0, 'W')]
        [InlineData(-1, 'A')]
        public void GetCell_CellDoesNotExist_ShouldBeThrowException(int height, char width)
        {
            //Arrange
            var board = new Board();

            //Act
            var action = () => board.GetCell(height, width);

            //Assert
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void MoveIsFoward_Success()
        {
            //Arrange
            var board = new Board();

            //Act

            //Assert
            board.MoveIsForward(new Cell(CellColour.Black, CellPlace.WhiteChecker, 'C', 3), new Cell(CellColour.Black, CellPlace.WhiteChecker, 'D', 4))
                .Should().BeTrue();
            board.MoveIsForward(new Cell(CellColour.Black, CellPlace.WhiteChecker, 'C', 3), new Cell(CellColour.Black, CellPlace.WhiteChecker, 'D', 2))
             .Should().BeFalse();
            board.MoveIsForward(new Cell(CellColour.Black, CellPlace.BlackChecker, 'B', 6), new Cell(CellColour.Black, CellPlace.BlackChecker, 'A', 5))
             .Should().BeTrue();
            board.MoveIsForward(new Cell(CellColour.Black, CellPlace.BlackChecker, 'A', 5), new Cell(CellColour.Black, CellPlace.BlackChecker, 'B', 6))
                .Should().BeFalse();
        }

        [Fact]
        public void GetAllCheckerPaths_GetPathsFromStartingPositionChecker_Success()
        {
            //Arrange
            var board = new Board();

            //Act
            var c = new Cell(CellColour.Black, CellPlace.WhiteQueen, 3, 'C');
            board.GetCell(3, 'C').UpdateCell(CellPlace.WhiteQueen);

            var result = board.GetAllCheckerPaths(c);

            //Assert
            result.Count.ShouldBe(2);
        }


    }
    public static class Ext
    {
        public static void B(this CellPlace cellPlace)
        {

        }
    }


}