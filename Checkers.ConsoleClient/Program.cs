// See https://aka.ms/new-console-template for more information
using Checkers.ConsoleClient;
using Domain.Base.Classes;
using Domain.Base.Enums;
using Domain.Base.Struct;
using System.Text;


while (true)
{
    //ViewLaunchMenu();

    var player = new CheckersPlayer(1, CellColor.Black);
    var bot = new Bot(2, CellColor.White);
    var game = new Game(player, bot);

    while (!game.IsGameOver)
    {
        game.DuplicateBoard.PrintCordinateBoard();
        Console.WriteLine();
        var board = game.DuplicateBoard;
        board.PrintBoard();

        if (game.IsMovable(player))
        {
            Console.WriteLine("Your move");
            Console.Write("Take checker, enter coordinate: ");
            if (!TryReadCoordinate(out var width, out var height))
            {
                Console.WriteLine("Cannot read coordinate!");
                continue;
            }

            Console.Write("Get available move, please choose move index: ");

            var moves = game.GetAllAvailableCheckerMoves(new CheckerLocation(width.Value, height.Value));

            if (!moves.Any())
            {
                Console.WriteLine("Please try again! This checker can not has available moves!");
                continue;
            }

            foreach (var move in moves)
            {
                Console.WriteLine("Move id: " + move.Id);
                Console.WriteLine(move.ToString());
            }
        }
        else if (game.IsMovable(bot))
        {
            Console.WriteLine("Bot move");
            bot.ChooseMove(default);
        }
        Console.WriteLine();
    }
}
bool TryReadCoordinate(out char? width, out int? height)
{
    width = default;
    height = default;
    var coordinate = Console.ReadLine()?.Trim();

    if (string.IsNullOrWhiteSpace(coordinate)) return false;

    if (!TryCoordinateParse(coordinate, out width, out height)) return false;

    return true;
}

bool TryCoordinateParse(string str, out char? width, out int? height)
{
    width = null;
    height = null;
    if (str.Length != 2)
    {
        return false;
    }
    else
    {
        width = str[0];
        var h = str[1].ToString();
        height = Convert.ToInt32(h);

        if (!Board.ValidateCoordinate(width.Value, height.Value)) return false;
    }

    return true;
}

#region Loading

void ViewLaunchMenu()
{
    while (true)
    {
        DisplayProgressBar(10);
        Console.WriteLine("If you want to enter to the game - press any key...");
        if (Console.ReadKey().Key == ConsoleKey.Spacebar) return;
    }
}

void DisplayProgressBar(int timeout)
{
    Console.WriteLine();
    Console.SetCursorPosition(0, Console.CursorTop);
    var firstCursorePosition = Console.CursorTop;
    var strBuilder = new StringBuilder(new string('_', 100), 100);
    for (int i = 1; i <= 100; i++)
    {
        strBuilder[i - 1] = '#';
        Console.WriteLine($"[ {strBuilder} ]");
        Console.Write($" {i}% ");

        Thread.Sleep(timeout);
        Console.SetCursorPosition(0, firstCursorePosition);
    }
    Console.WriteLine();
}

#endregion