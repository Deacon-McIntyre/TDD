using TDD.Models;

namespace TDD
{
  internal class ConsoleView
  {
    private readonly IBoard _board;
    private readonly IConsoleWrapper _console;

    public ConsoleView(IBoard board, IConsoleWrapper console)
    {
      _board = board;
      _console = console;
    }

    public void PrintBoard()
    {
      for (var x = 0; x < _board.Units.GetLength(0); x++)
      {
        var row = "";
        for (var y = 0; y < _board.Units.GetLength(1); y++)
        {
          row += $"[{(_board.Units[x, y] != null ? _board.Units[x, y] : ' ')}]";
        }
        _console.WriteLine(row);
      }
    }
  }
}