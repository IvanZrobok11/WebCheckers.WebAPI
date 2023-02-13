using Domain.Base.Enums;
using Domain.Base.Struct;
using Domain.Extension;

namespace Domain.Base.Classes
{
    public class Game
    {
        protected Board _board;
        private readonly CheckersPlayer player1;
        private readonly CheckersPlayer player2;
        public CellColor Movable { get; private set; }
        public int MovablePlayerId { get; private set; }
        private bool IsGameStarted;
        public bool IsGameOver = false;
        public Game(CheckersPlayer player1, CheckersPlayer player2)
        {
            _board = new Board();
            this.player1 = player1;
            this.player2 = player2;

            if (player1.Color == player2.Color)
            {
                throw new Exception("Players has the same color");
            }

            MovablePlayerId = player1.Color == CellColor.White ? player1.Id : player2.Id;
        }


        public void MakeMove(int playerId, CheckerLocation with, int moveId)
        {
            var player = GetPlayer(playerId);
            if (!IsMovable(player))
            {
                throw new ArgumentException($"Player with id: {player.Id}, can not make a move!");
            }
            var withCell = _board.GetCell(with);

            if (withCell.Checker == CellPlace.Empty)
            {
                throw new ArgumentException($"Player with id: {player.Id}, can not make a move!");
            }
            var moves = GetAllAvailableCheckerMoves(withCell);
            var move = moves.SingleOrDefault(move => move.Id == moveId);

            if (move is null || move.Path.Count() <= 1)
            {
                throw new ArgumentException($"Invalid move id - {moveId}! Move does not exist or empty!");
            }

            MakeMove(move);
        }

        private void MakeMove(Move move)
        {
            var beatens = new List<Cell>();
            var start = move.Path.First();
            var end = move.Path.Last();

            if (!end.Checker.IsEmpty())
            {
                throw new Exception("Invalid path! Ended cell must be empty!");
            }
            bool lastMoveWasBeating = false;
            foreach (var cell in move.Path)
            {
                if (cell.LocationEquels(start))
                {
                    _board.UpdateCell(new CheckerLocation(cell.Width, cell.Height), CellPlace.Empty);
                    continue;
                }
                else if (cell.LocationEquels(end))
                {
                    _board.UpdateCell(new CheckerLocation(cell.Width, cell.Height), start.Checker);
                    break;
                }
                else if (!cell.Checker.SameFiguresColor(start.Checker))
                {
                    _board.UpdateCell(new CheckerLocation(cell.Width, cell.Height), CellPlace.Empty);
                    beatens.Add(_board.GetCell(new CheckerLocation(cell.Width, cell.Height)));
                }
                else if (lastMoveWasBeating)
                {
                    _board.UpdateCell(new CheckerLocation(cell.Width, cell.Height), start.Checker);
                    if (Board.OnEndLine(cell))
                    {
                        _board.UpdateCell(new CheckerLocation(cell.Width, cell.Height), start.Checker.ToQueen());
                    }
                }
            }
        }
        public IEnumerable<Move> GetAllAvailableCheckerMoves(CheckerLocation location)
        {
            var cell = _board.GetCell(location);
            return GetAllAvailableCheckerMoves(cell);
        }

        public IEnumerable<Move> GetAllAvailableCheckerMoves(Cell start)
        {
            var paths = GetAvailableCheckerPaths(start);
            return paths.Select(path => new Move(path));
        }

        private List<LinkedList<Cell>> GetAvailableCheckerPaths(Cell start)
        {
            var paths = new List<LinkedList<Cell>>();

            bool startIsQueen = start.Checker.IsQueen();

            var queue = new Queue<Cell>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                var adjCells = _board.GetAllAdjacentAcrossCells(node)
                    .SkipWhere(nextNode => nextNode.Checker.SameFiguresColor(start.Checker)) // Skip all where move is blocked same color checker
                    .SkipWhere(nextNode => nextNode.Checker.SameFiguresColor(node.Checker))

                    .SkipWhere(nextNode => MoveIsBack(start, nextNode) && !startIsQueen)
                    .SkipWhere(nextNode => node.Checker == CellPlace.Empty && nextNode.Checker == CellPlace.Empty && !startIsQueen)
                    .SkipWhere(nextNode => startIsQueen && !Board.OnSameDiagonal(nextNode, node))
                    .ToList();

                foreach (var adj in adjCells)
                {
                    var lastPath = paths.FirstOrDefault(x => x.Last.Value.Equals(node));
                    if (lastPath == null)
                    {
                        var path = new LinkedList<Cell>();
                        path.AddFirst(node);
                        paths.Add(path);
                        lastPath = path;
                    }

                    if (adj.Checker == CellPlace.Empty || CanBeat(start, adj))
                    {
                        lastPath.AddLast(adj);
                    }
                }
            }

            return paths;
        }

        private bool CanBeat(Cell start, Cell current)
        {
            if (!start.Checker.SameFiguresColor(current.Checker))
            {
                var nextCell = _board.GetAllAdjacentAcrossCells(current).Where(x => Board.OnSameDiagonal(start, x));
                if (nextCell.Any() && nextCell.First().Checker == CellPlace.Empty)
                {
                    return true;
                }
            }
            return false;
        }

        //TODO:
        private List<List<Cell>> GetAvailableCheckerPaths_Old(Cell start)
        {
            if (start.Checker == CellPlace.Empty)
            {
                throw new ArgumentException("Start checker place can not be empty!");
            }
            //track to Cell - key, from Cell - value
            var track = new Dictionary<Cell, Cell?>();
            track[start] = null;
            bool startIsQueen = start.Checker.IsQueen();
            var queue = new Queue<Cell>();
            queue.Enqueue(start);

            var endPaths = new List<Cell>();
            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                var adjCells = _board.GetAllAdjacentAcrossCells(node)
                    .Where(nextNode => nextNode.Checker.SameFiguresColor(start.Checker)) // Skip all where move is blocked same color checker
                    .SkipWhere(nextNode => node.Checker.SameFiguresColor(nextNode.Checker))
                    .SkipWhere(nextNode => node.Checker == CellPlace.Empty && nextNode.Checker == CellPlace.Empty/* && !startIsQueen*/)
                    .SkipWhere(nextNode => (MoveIsBack(start, nextNode) || !startIsQueen))
                    .ToList();

                if (!adjCells.Any()) endPaths.Add(node);

                foreach (var nextNode in adjCells)
                {
                    //todo if next current and prev on the same line its ok else no
                    //need to find with dict tracker
                    //if ()
                    //{//this maked in up algorithm 
                    //}
                    //or transfer this to after or 
                    //if (!MoveIsForward(node, nextNode) && node.Checker != CellPlace.Queen)
                    //{
                    //    //TODO: checker can bit to back but cannot move to back
                    //}
                    track[nextNode] = node;
                    queue.Enqueue(nextNode);
                }
            }
            var paths = new List<List<Cell>>();
            foreach (var endPath in endPaths)
            {
                var path = new List<Cell>();
                path.Add(endPath);
                Cell? end = endPath;
                if (end == null) continue;

                while (true)
                {
                    var prevCell = track[end.Value];
                    if (prevCell == null) break;

                    path.Add(prevCell.Value);
                    end = prevCell;
                }

                paths.Add(path);
            }
            return paths;
        }

        public static bool MoveIsForward(Cell with, Cell to) => with.Checker switch
        {
            CellPlace.WhiteChecker => Board.WHITE_IS_DOWN && with.Height < to.Height,
            CellPlace.WhiteQueen => Board.WHITE_IS_DOWN && with.Height < to.Height,
            CellPlace.BlackChecker => Board.WHITE_IS_DOWN && with.Height > to.Height,
            CellPlace.BlackQueen => Board.WHITE_IS_DOWN && with.Height > to.Height,
            _ => throw new ArgumentException(),
        };
        public static bool MoveIsBack(Cell with, Cell to) => !MoveIsForward(with, to);
        private bool IsBeatMove(Cell with, Cell to)
        {
            var difference = with.Height - to.Height;
            if (difference > 2 || difference < 2)
            {
                return true;
            }
            return false;
        }

        public bool IsMovable(CheckersPlayer player) => MovablePlayerId == player.Id;
        private CheckersPlayer GetPlayer(int playerId)
        {
            if (player1.Id != playerId && player2.Id != playerId)
            {
                throw new ArgumentException("");
            }
            return player1.Id == playerId ? player1 : player2;
        }
        public Board DuplicateBoard => _board.Copy();
    }
}
