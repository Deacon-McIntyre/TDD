using System;
using System.Text;
using TDD.Models;
using TDD.Models.Enums;

namespace TDD
{
  public class ConsoleView
  {
    private readonly IBoard _board;
    private readonly IConsoleWrapper _console;
    private const char EmptyTileCharacter = ' ';
    private Tuple<int, int> _target;

    public ConsoleView(IBoard board, IConsoleWrapper console)
    {
      _board = board;
      _console = console;
      _target = null;
    }

    public void PrintBoard()
    {
      _console.Clear();
      for (var x = 0; x < _board.UnitIds.GetLength(0); x++)
      {
        var builder = new StringBuilder();
        for (var y = 0; y < _board.UnitIds.GetLength(1); y++)
        {
          FormatCell(builder, x, y);
        }
        _console.WriteLine(builder.ToString());
      }
    }

    public void SetTarget(int x, int y)
    {
      _target = new Tuple<int, int>(x, y);
    }

    public void ClearTarget()
    {
      _target = null;
    }

    public void MoveTarget(Cardinal direction)
    {
      switch (direction)
      {
        case Cardinal.North:
          _target = new Tuple<int, int>(_target.Item1, Math.Max(0, _target.Item2-1));
          break;
        case Cardinal.South:
          _target = new Tuple<int, int>(_target.Item1, Math.Min(_board.UnitIds.GetLength(1)-1, _target.Item2+1));
          break;
        case Cardinal.East:
          _target = new Tuple<int, int>(Math.Min(_board.UnitIds.GetLength(0)-1, _target.Item1+1), _target.Item2);
          break;
        case Cardinal.West:
          _target = new Tuple<int, int>(Math.Max(0, _target.Item1-1), _target.Item2);
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
      }
    }

    private void FormatCell(StringBuilder builder, int x, int y)
    {
      // Flip X and Y in here to make visualization look as expected
      var unitId = _board.UnitIds[y, x];
      if (_target != null)
      {
        if (x == _target.Item2 && y == _target.Item1)
        {
          builder.Append($"[{(unitId != 0 ? _board.LookupUnit(unitId) : EmptyTileCharacter)}]");
          return;
        }

        float rise = y - _target.Item1;
        float run = x - _target.Item2;
        if (rise != 0 && run != 0)
        {
          float slope = rise / run;
          if (slope == 1)
          {
            builder.Append("◥■◣");
            return;
          }
          if (slope == -1)
          {
            builder.Append("◢■◤");
            return;
          }
        }
      }

      builder.Append($" {(unitId != 0 ? _board.LookupUnit(unitId) : EmptyTileCharacter)} ");
    }

    public void PrintOptions(int targetOption)
    {
      _console.WriteLine("=====================");
      _console.WriteLine($"{(targetOption == 0 ? '⮞' : " ")}Inspect a tile");
      _console.WriteLine($"{(targetOption == 1 ? '⮞' : " ")}Place a unit");
    }
  }
}