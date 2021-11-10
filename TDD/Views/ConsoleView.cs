﻿using System;
using System.Collections.Generic;
using System.Text;
using TDD.Models;
using TDD.Models.Enums;
using TDD.Models.Units;

namespace TDD
{
  public class ConsoleView
  {
    private readonly IBoard _board;
    private readonly IConsoleWrapper _console;
    private const char EmptyTileCharacter = ' ';
    private const string Header = "=================\nWASD to move, Q to cancel, Enter to submit.";
    public Tuple<int, int> Target { get; set; }

    public ConsoleView(IBoard board, IConsoleWrapper console)
    {
      _board = board;
      _console = console;
      Target = null;
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

    private void FormatCell(StringBuilder builder, int x, int y)
    {
      // Flip X and Y in here to make visualization look as expected
      var unitId = _board.UnitIds[y, x];
      if (Target != null)
      {
        if (x == Target.Item2 && y == Target.Item1)
        {
          builder.Append($"[{(unitId != 0 ? _board.LookupUnit(unitId) : EmptyTileCharacter)}]");
          return;
        }

        float rise = y - Target.Item1;
        float run = x - Target.Item2;
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

    public void PrintOptions(List<Option> options)
    {
      _console.WriteLine(Header);
      foreach (var option in options)
      {
        _console.WriteLine(option.ToString());
      }
    }

    public void PrintUnitDetails(int unitId)
    {
      _console.WriteLine(Header);
      if (unitId == 0)
      {
        _console.WriteLine("This tile is not occupied.");
        return;
      }
      var unit = _board.LookupUnit(unitId);
      _console.WriteLine($"{unit.Description()} It has {unit.HitPoints} HP remaining");
    }

    public void PrintPlaceUnitInfo(UnitBase unit)
    {
      _console.WriteLine(Header);
      _console.WriteLine($"Placing unit: {unit.Description()}");
    }
  }
}