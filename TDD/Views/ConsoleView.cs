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
      for (var x = 0; x < _board.UnitIds.GetLength(0); x++)
      {
        var row = "";
        for (var y = 0; y < _board.UnitIds.GetLength(1); y++)
        {
          // Flip X and Y here to make visualization look as expected
          var unitId = _board.UnitIds[y, x];
          row += $"[{(unitId != 0 ? _board.LookupUnit(unitId) : ' ')}]";
        }
        _console.WriteLine(row);
      }
    }
  }
}