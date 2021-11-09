using System;
using TDD.Models;
using TDD.Models.Enums;

namespace TDD
{
  internal class ConsoleView
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
        var row = "";
        for (var y = 0; y < _board.UnitIds.GetLength(1); y++)
        {
          // Flip X and Y in here to make visualization look as expected
          var unitId = _board.UnitIds[y, x];
          if (_target != null)
          {
            if (x == _target.Item2 && y == _target.Item1)
            {
              row += $" {(unitId != 0 ? _board.LookupUnit(unitId) : EmptyTileCharacter)} ";
              continue;
            }

            if (x - _target.Item2 == y - _target.Item1)
            {
              row += " \\ ";
              continue;
            }

            // if (x == _target.Item2)
            // {
            //   if (y == _target.Item1 - 1)
            //   {
            //     row += " ᐅ ";
            //   }
            //   else if (y == _target.Item1 + 1)
            //   {
            //     row += " ᐊ ";
            //   }
            //   else
            //   {
            //     row += "═══";
            //   }
            //   continue;
            // }
            //
            // if (y == _target.Item1)
            // {
            //   if (x == _target.Item2 - 1)
            //   {
            //     row += " ᐁ ";
            //   }
            //   else if (x == _target.Item2 + 1)
            //   {
            //     row += " ᐃ ";
            //   }
            //   else
            //   {
            //     row += " ║ ";
            //   }
            //   continue;
            // }
          }
          row += $"[{(unitId != 0 ? _board.LookupUnit(unitId) : EmptyTileCharacter)}]";
        }
        _console.WriteLine(row);
      }
    }

    public void SetTarget(int x, int y)
    {
      _target = new Tuple<int, int>(x, y);
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
  }
}