namespace TDD.Models.Units
{
  public class Mage : UnitBase
  {
    public Mage(int id) : base(id)
    {
      HitPoints = 2;
      Stationary = false;
    }

    public override string ToString()
    {
      return "M";
    }

    public override string Description()
    {
      return "A Mage.";
    }
  }
}