using System;
using System.Collections.Generic;
using TDD.Models.Units;

namespace TDD.Models
{
  public class Board : IBoard
  {
    public int[,] UnitIds { get; }
    private readonly Dictionary<int, UnitBase> _unitMap;

    public Board(int x = 4, int y = 4)
    {
      UnitIds = new int[x, y];
      _unitMap = new Dictionary<int, UnitBase>();
    }

    public bool TryPlace(UnitBase unitBase, int x, int y)
    {
      if (OutOfBoundsOrOccupied(x, y)) return false;
      UnitIds[x,y] = unitBase.Id;
      _unitMap.Add(unitBase.Id, unitBase);
      return true;
    }

    public bool TryMoveUnitTo(int unitId, int x, int y)
    {
      if (OutOfBoundsOrOccupied(x, y) || UnitIsStationary(unitId)) return false;
      RemoveUnitFromBoard(unitId);
      UnitIds[x, y] = unitId;
      return true;
    }

    private bool OutOfBoundsOrOccupied(int x, int y)
    {
      if (x < 0 || y < 0 ||
          x >= UnitIds.GetLength(0) ||
          y >= UnitIds.GetLength(1))
        return true;

      // Only occupied if there a solid unit in the way
      return UnitIds[x, y] != 0 && _unitMap[UnitIds[x, y]].Solid;
    }

    private bool UnitIsStationary(int unitId)
    {
      var unit = LookupUnit(unitId);
      return unit.Stationary;
    }

    public UnitBase LookupUnit(int unitId)
    {
      return _unitMap[unitId];
    }

    public void DeleteUnit(int unitId)
    {
      if (_unitMap.ContainsKey(unitId))
      {
        _unitMap.Remove(unitId);
        RemoveUnitFromBoard(unitId);
        return;
      }
      throw new KeyNotFoundException($"Unable to delete unit id {unitId}, not found");
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

      throw new KeyNotFoundException($"Unable to get coords for unit id {unitId}, unit not found");
    }
  }
}