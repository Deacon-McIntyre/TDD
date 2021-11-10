using System;
using TDD.Models;
using TDD.Models.Enums;
using TDD.Models.Units;
using Void = TDD.Models.Units.Void;

namespace TDD
{
  public class Game
  {
    private int _idCounter = 1;
    private Board Board;
    private IConsoleWrapper _console;
    private ConsoleView _view;
    private bool _targeting;
    private int _mageId;

    public Game(IConsoleWrapper console)
    {
      Board = new Board(6, 6);
      _console = console;
      _view = new ConsoleView(Board, _console);
      _targeting = false;
      _mageId = GetId();
    }

    public void Run()
    {
      PutVoidAroundOutside();
      PlaceNewWall(1, 1);
      PlaceNewWall(1, 2);
      PlaceNewWall(1, 3);
      PlaceNewWall(3, 3);
      PlaceNewWall(4, 3);

      var mage = new Mage(_mageId);
      Board.TryPlace(mage, 3, 1);

      while (true)
      {
        _view.PrintBoard();
        _view.PrintOptions(0);
        var input = _console.ReadKey();
        switch (input)
        {
          case ConsoleKey.Enter:
            ToggleTargeting();
            break;
          case ConsoleKey.W:
            HandleDirection(Cardinal.North);
            break;
          case ConsoleKey.S:
            HandleDirection(Cardinal.South);
            break;
          case ConsoleKey.D:
            HandleDirection(Cardinal.East);
            break;
          case ConsoleKey.A:
            HandleDirection(Cardinal.West);
            break;
        }
      }
    }

    private void HandleDirection(Cardinal direction)
    {
      if (_targeting)
      {
        _view.MoveTarget(direction);
      }
      // else if (_choosingOption)
      // {
      //
      // }
      else
      {
        Board.TryPush(_mageId, direction);
      }
    }

    private void ToggleTargeting()
    {
      if (_targeting)
      {
        _view.ClearTarget();
        _targeting = false;
      }
      else
      {
        _view.SetTarget(4, 4);
        _targeting = true;
      }
    }

    private void PlaceNewWall(int x, int y)
    {
      var wall = new Wall(GetId());

      Board.TryPlace(wall, x, y);
    }

    private void PlaceNewVoid(int x, int y)
    {
      var voidTile = new Void(GetId());

      Board.TryPlace(voidTile, x, y);
    }

    private void PutVoidAroundOutside()
    {
      var xLimit = Board.UnitIds.GetLength(0);
      var yLimit = Board.UnitIds.GetLength(1);
      for (var x = 0; x < xLimit; x++)
      {
        for (var y = 0; y < yLimit; y++)
        {
          if (x == 0 || y == 0 || x == xLimit-1 || y == yLimit-1)
          {
            PlaceNewVoid(x, y);
          }
        }
      }
    }

    private int GetId()
    {
      _idCounter++;
      return _idCounter;
    }
  }
}