namespace TDD.Models.Units
{
  public class Mage : UnitBase
  {
    public Mage(int id) : base(id)
    {
      HitPoints = 2;
    }

    public override string ToString()
    {
      return "M";
    }
  }
}