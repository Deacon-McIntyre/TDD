using System;
using System.Collections.Generic;
using System.Linq;
using TDD.Models;
using TDD.Models.Enums;
using TDD.Models.Options;
using TDD.Models.Units;
using Void = TDD.Models.Units.Void;

namespace TDD
{
  public class Game
  {
    private readonly Board _board;
    private readonly IConsoleWrapper _console;
    private readonly ConsoleView _view;
    private readonly int _mageId;
    private int _idCounter = 1;
    private Tuple<int, int> _targetCoords;
    private State _state;
    private readonly List<StringOption> _options;
    private readonly List<UnitOption> _placeableUnits;

    public Game(IConsoleWrapper console)
    {
      _board = new Board(6, 6);
      _console = console;
      _view = new ConsoleView(_console);
      _targetCoords = new Tuple<int, int>(0, 0);
      _mageId = GetId();
      _state = State.SelectOption;
      _options = new List<StringOption>
      {
        new("Inspect tiles", State.InspectUnits, true),
        new("Place a unit", State.SelectUnit),
      };
      _placeableUnits = new List<UnitOption>
      {
        new(new Mage(GetId()), State.PlaceUnit, true),
        new(new Wall(GetId()), State.PlaceUnit)
      };
    }

    public void Run()
    {
      SetUp();

      while (true)
      {
        _view.PrintBoard(_board);
        PrintInfo();

        var input = _console.ReadKey();
        switch (input)
        {
          case ConsoleKey.Enter:
            HandleSubmit();
            break;
          case ConsoleKey.Q:
            HandleEscape();
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

    private void PrintInfo()
    {
      switch (_state)
      {
        case State.InspectUnits:
          var unitId = _board.UnitIds[_targetCoords.Item1, _targetCoords.Item2];
          _view.PrintUnitDetails(unitId == 0 ? null: _board.LookupUnit(unitId));
          break;
        case State.SelectUnit:
          _view.PrintSelectUnitInfo(_placeableUnits);
          break;
        case State.PlaceUnit:
          _view.PrintPlaceUnitInfo(new Mage(GetId()));
          break;
        default:
          _view.PrintOptions(_options);
          break;
      }
    }

    private void SetUp()
    {
      PutVoidAroundOutside();
      PlaceNewWall(1, 1);
      PlaceNewWall(1, 2);
      PlaceNewWall(1, 3);
      PlaceNewWall(3, 3);
      PlaceNewWall(4, 3);

      var mage = new Mage(_mageId);
      _board.TryPlace(mage, 3, 1);
    }

    private void HandleDirection(Cardinal direction)
    {
      switch (_state)
      {
        case State.SelectOption:
          MoveOption(_options, direction);
          break;
        case State.InspectUnits:
          MoveTarget(direction);
          break;
        case State.SelectUnit:
          MoveOption(_placeableUnits, direction);
          break;
        case State.PlaceUnit:
          MoveTarget(direction);
          break;
        case State.SimulateGame:
          _board.TryPush(_mageId, direction);
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private void HandleSubmit()
    {
      switch (_state)
      {
        case State.SelectOption:
          var nextState = _options.First(o => o.Selected).NextState;
          if (nextState is State.InspectUnits or State.PlaceUnit)
          {
            _view.Target = _targetCoords;
          }
          _state = nextState;
          break;
        case State.InspectUnits:
          _view.Target = null;
          _state = State.SelectOption;
          break;
        case State.PlaceUnit:
          _view.Target = null;
          _board.TryPlace(_placeableUnits.First(o => o.Selected).Unit, _targetCoords.Item1, _targetCoords.Item2);
          _state = State.SelectOption;
          break;
        case State.SimulateGame:
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private void HandleEscape()
    {
      _view.Target = null;
      _state = State.SelectOption;
    }

    private void MoveOption(List<Option> options, Cardinal direction)
    {
      var selectedOption = options.IndexOf(_options.Find(o => o.Selected));
      var increment = direction switch
      {
        Cardinal.North => -1,
        Cardinal.South => 1,
        _ => 0
      };
      var newSelected = Math.Clamp(selectedOption + increment, 0, _options.Count-1);
      for (var i = 0; i < options.Count; i++)
      {
        options[i].Selected = i == newSelected;
      }
    }

    private void MoveTarget(Cardinal direction)
    {
      switch (direction)
      {
        case Cardinal.North:
          _targetCoords = new Tuple<int, int>(_targetCoords.Item1, Math.Max(0, _targetCoords.Item2-1));
          break;
        case Cardinal.South:
          _targetCoords = new Tuple<int, int>(_targetCoords.Item1, Math.Min(_board.UnitIds.GetLength(1)-1, _targetCoords.Item2+1));
          break;
        case Cardinal.East:
          _targetCoords = new Tuple<int, int>(Math.Min(_board.UnitIds.GetLength(0)-1, _targetCoords.Item1+1), _targetCoords.Item2);
          break;
        case Cardinal.West:
          _targetCoords = new Tuple<int, int>(Math.Max(0, _targetCoords.Item1-1), _targetCoords.Item2);
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
      }
      _view.Target = _targetCoords;
    }

    private void PlaceNewWall(int x, int y)
    {
      var wall = new Wall(GetId());

      _board.TryPlace(wall, x, y);
    }

    private void PlaceNewVoid(int x, int y)
    {
      var voidTile = new Void(GetId());

      _board.TryPlace(voidTile, x, y);
    }

    private void PutVoidAroundOutside()
    {
      var xLimit = _board.UnitIds.GetLength(0);
      var yLimit = _board.UnitIds.GetLength(1);
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