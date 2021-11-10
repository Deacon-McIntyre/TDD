namespace TDD.Models.Units
{
  public class Wall : UnitBase
  {
    public Wall(int id) : base(id)
    {
      HitPoints = 10;
      Stationary = true;
    }

    public override string ToString()
    {
      // ■
      return "▧";
    }

    public override string Description()
    {
      return "A solid wall.";
    }
  }
}