using TDD.Models.Units;

namespace TDD.Models
{
  public interface IBoard
  {
    public int[,] UnitIds { get; }
    public bool TryPlace(UnitBase unitBase, int x, int y);
    public bool TryMoveUnitTo(int unitId, int x, int y);
    public UnitBase LookupUnit(int unitId);
  }
}