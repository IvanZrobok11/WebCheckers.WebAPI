using Domain.Base.Classes;
using Domain.Base.Enums;

namespace Checkers.ConsoleClient
{
    internal static class BoardExtension
    {
        public static void PrintBoard(this Board board)
        {
            board.RevertTraversalField((height, width) =>
            {
                if (board.GetCellByIndex(height, width).Color == CellColor.White)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write("  ");
                }
                else if (board.GetCellByIndex(height, width).Color == CellColor.Black)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    var checker = board.GetCellByIndex(height, width).Checker;
                    if (checker != CellPlace.Empty)
                    {
                        if (checker == CellPlace.BlackChecker)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        else if (checker == CellPlace.WhiteChecker)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.Write("@ ");
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }
                Console.ResetColor();
            }, (height) => Console.WriteLine());
        }
        public static void PrintCordinateBoard(this Board board)
        {
            board.RevertTraversalField((height, width) =>
            {
                if (board.GetCellByIndex(height, width).Color == CellColor.White)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;

                }
                else if (board.GetCellByIndex(height, width).Color == CellColor.Black)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                var cell = board.GetCellByIndex(height, width);
                Console.Write($"{cell.Width}{cell.Height}");
                Console.ResetColor();
            },
            (height) =>
            {
                if (height == 4)
                {
                    Console.Write("\n" + new string('-', 16));
                }
                Console.WriteLine();
            });
        }
    }
}
