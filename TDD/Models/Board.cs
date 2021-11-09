using System;
using System.Collections.Generic;
using TDD.Models.Enums;
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
      PerformOverlapAction(unitBase, x, y);
      AddUnitToBoard(unitBase.Id, x, y);
      _unitMap.Add(unitBase.Id, unitBase);
      return true;
    }

    public bool TryMoveUnitTo(int unitId, int x, int y)
    {
      if (OutOfBoundsOrOccupied(x, y) || UnitIsStationary(unitId)) return false;
      if (PerformOverlapAction(LookupUnit(unitId), x, y))
      {
        // PerformOverlapAction should handle the moving or whatever
      }
      else
      {
        RemoveUnitFromBoard(unitId);
        AddUnitToBoard(unitId, x, y);
      }
      return true;
    }

    public bool TryPush(int unitId, Cardinal direction)
    {
      var (x, y) = GetCoordsForUnitId(unitId);
      return direction switch
      {
        Cardinal.North => TryMoveUnitTo(unitId, x, y - 1),
        Cardinal.South => TryMoveUnitTo(unitId, x, y + 1),
        Cardinal.East => TryMoveUnitTo(unitId, x + 1, y),
        Cardinal.West => TryMoveUnitTo(unitId, x - 1, y),
        _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, $"Don't know the direction {direction}")
      };
    }

    private bool PerformOverlapAction(UnitBase unitOnTop, int x, int y)
    {
      var existingUnitId = UnitIds[x, y];
      if (existingUnitId == 0) return false;
      var existingUnit = _unitMap[existingUnitId];
      if (!existingUnit.Solid)
      {
        return existingUnit.OnOverlap(this, unitOnTop);
      }
      return false;
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

    private void AddUnitToBoard(int unitId, int x, int y)
    {
      UnitIds[x, y] = unitId;
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