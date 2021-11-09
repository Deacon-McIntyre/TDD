namespace TDD.Models.Units
{
  public class Void : UnitBase
  {
    public Void(int id) : base(id)
    {
      Stationary = true;
      Solid = false;
    }

    public override void Damage(int amount)
    {
      // Void does not take damage
    }

    public override bool OnOverlap(Board board, UnitBase overlappingUnit)
    {
      board.DeleteUnit(overlappingUnit.Id);
      return true;
    }

    public override string ToString()
    {
      // " "
      return "•";
    }
  }
}