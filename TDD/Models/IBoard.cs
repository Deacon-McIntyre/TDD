using TDD.Models.Units;

namespace TDD.Models
{
  public interface IBoard
  {
    public int[,] UnitIds { get; }
    public void Place(IUnit unit, int x, int y);
    bool MoveUnitTo(int unitId, int x, int y);
    IUnit LookupUnit(int unitId);
  }
}