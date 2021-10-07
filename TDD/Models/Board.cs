using System;
using System.Collections.Generic;
using TDD.Models.Units;

namespace TDD.Models
{
  public class Board : IBoard
  {
    public int[,] UnitIds { get; }
    private readonly Dictionary<int, IUnit> _unitMap;

    public Board(int x = 4, int y = 4)
    {
      UnitIds = new int[x, y];
      _unitMap = new Dictionary<int, IUnit>();
    }

    public bool TryPlace(IUnit unit, int x, int y)
    {
      if (OutOfBoundsOrOccupied(x, y)) return false;
      UnitIds[x,y] = unit.Id;
      _unitMap.Add(unit.Id, unit);
      return true;
    }

    public bool TryMoveUnitTo(int unitId, int x, int y)
    {
      if (OutOfBoundsOrOccupied(x, y)) return false;
      RemoveUnitFromBoard(unitId);
      UnitIds[x, y] = unitId;
      return true;
    }

    private bool OutOfBoundsOrOccupied(int x, int y)
    {
      return x < 0 ||
             y < 0 ||
             x >= UnitIds.GetLength(0) ||
             y >= UnitIds.GetLength(1) ||
             UnitIds[x, y] != 0 ;
    }

    public IUnit LookupUnit(int unitId)
    {
      return _unitMap[unitId];
    }

    private void RemoveUnitFromBoard(int unitId)
    {
      var (x, y) = GetCoordsForUnitId(unitId);
      UnitIds[x, y] = 0;
    }

    private Tuple<int, int> GetCoordsForUnitId(int unitId)
    {
      for (var x = 0; x < UnitIds.GetLength(0); x++)
      {
        for (var y = 0; y < UnitIds.GetLength(1); y++)
        {
          if (UnitIds[x, y] == unitId) return new Tuple<int, int>(x, y);
        }
      }

      throw new KeyNotFoundException($"Unable to find unit id {unitId}");
    }
  }
}