using Domain.Base.Classes;
using Domain.Base.Enums;
using Domain.Base.Struct;
using FluentAssertions;

namespace Checkers.UnitTests
{
    public class TestGame : Game
    {
        public TestGame(CheckersPlayer player1, CheckersPlayer player2) : base(player1, player2)
        {
        }

        public void SetBoard(Board board)
        {
            _board = board;
        }
    }
    public class GameTests
    {
        public GameTests()
        {

        }

        [Theory]
        [InlineData('A', 3, 'B', 4, true)]
        [InlineData('B', 4, 'A', 2, false)]
        public void MoveIsForward_WhiteChecher_ShouldBeExpected(char with1, int with2, char to1, int to2, bool expected)
        {
            //Arrange
            var board = new Board();

            //Act
            var result = Game.MoveIsForward(new Cell(CellColor.Black, CellPlace.WhiteChecker, with1, with2),
                new Cell(CellColor.Black, CellPlace.Empty, to1, to2));

            //Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData('B', 6, 'A', 5, true)]
        [InlineData('A', 5, 'B', 6, false)]
        public void MoveIsForward_BlackChecker_ShouldBeExpected(char with1, int with2, char to1, int to2, bool expected)
        {
            //Arrange
            var board = new Board();

            //Act
            var result = Game.MoveIsForward(new Cell(CellColor.Black, CellPlace.BlackChecker, with1, with2),
                new Cell(CellColor.Black, CellPlace.Empty, to1, to2));

            //Assert
            result.Should().Be(expected);
        }

        [Fact]
        public void GetAllAvailableCheckerMoves_StartPositionAndCheckerCanMoves_ReturnAvailablePaths()
        {
            //Arrange
            var player1 = new CheckersPlayer(1, CellColor.White);
            var player2 = new CheckersPlayer(2, CellColor.Black);
            var game = new Game(player1, player2);

            //Act
            var lockationStart = new CheckerLocation('C', 3);
            var result = game.GetAllAvailableCheckerMoves(lockationStart).ToList();

            //Assert
            result.Count().Should().Be(2);

            var firstExpected = new List<Cell>
            {
                new Cell(CellColor.Black, CellPlace.WhiteChecker, lockationStart),
                new Cell(CellColor.Black, CellPlace.Empty, new CheckerLocation('B', 4)),
            };
            result[0].Path.Should().BeEquivalentTo(firstExpected);

            var secondExpected = new List<Cell>
            {
                new Cell(CellColor.Black, CellPlace.WhiteChecker, lockationStart),
                new Cell(CellColor.Black, CellPlace.Empty, new CheckerLocation('D', 4)),
            };
            result[1].Path.Should().BeEquivalentTo(secondExpected);
        }

        [Fact]
        public void GetAllAvailableCheckerMoves_WhiteCheckerIsBlockedByWhiteChecker_ReturnEmptyList()
        {
            //Arrange
            var player1 = new CheckersPlayer(1, CellColor.White);
            var player2 = new CheckersPlayer(2, CellColor.Black);
            var game = new Game(player1, player2);

            var firstMoveLocation = new CheckerLocation('C', 3);
            var firstMove = game.GetAllAvailableCheckerMoves(firstMoveLocation).First(x => x.Path.Last.Value.Width == 'B');
            game.MakeMove(1, firstMoveLocation, firstMove.Id);

            //Act
            var lockationStart = new CheckerLocation('A', 3);
            var result = game.GetAllAvailableCheckerMoves(lockationStart).ToList();

            //Assert
            result.Count.Should().Be(0);
            result.Should().BeEmpty();
        }

        [Theory]
        [InlineData('A', 5, CellPlace.WhiteChecker)]
        public void GetAllAvailableCheckerMoves_WhiteCheckerIsBlockedByBlackChecker_ReturnEmptyList(char width, int height, CellPlace cellPlace)
        {
            //Arrange
            var player1 = new CheckersPlayer(1, CellColor.White);
            var player2 = new CheckersPlayer(2, CellColor.Black);
            var game = new TestGame(player1, player2);

            //that is getting a pointer to board
            var board = game.DuplicateBoard;
            game.SetBoard(board);

            board.UpdateCell(width, height, cellPlace);

            //Act
            var result = game.GetAllAvailableCheckerMoves(new CheckerLocation(width, height));

            //Assert
            result.Count().Should().Be(0);
        }

        [Fact]
        public void GetAllAvailableCheckerMoves_CheckerCanBeat_ReturnOnlyAvailableBeatMoves()
        {
            //Arrange
            var player1 = new CheckersPlayer(1, CellColor.White);
            var player2 = new CheckersPlayer(2, CellColor.Black);
            var game = new TestGame(player1, player2);

            var board = game.DuplicateBoard;
            game.SetBoard(board);

            board.UpdateCell('C', 3, CellPlace.Empty);
            board.UpdateCell('B', 4, CellPlace.WhiteChecker);

            board.UpdateCell('D', 6, CellPlace.Empty);
            board.UpdateCell('C', 5, CellPlace.BlackChecker);

            //Act
            var result = game.GetAllAvailableCheckerMoves(new CheckerLocation('B', 4));

            //Assert
            result.Count().Should().Be(1);
        }

    }
}
