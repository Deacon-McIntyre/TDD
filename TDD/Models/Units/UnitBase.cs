namespace TDD.Models.Units
{
  public abstract class UnitBase
  {
    public int Id { get; }
    public int HitPoints { get; protected set; }

    protected UnitBase(int id)
    {
      Id = id;
    }

    public void Damage(int amount)
    {
      HitPoints -= amount;
    }

    public bool IsDead() => HitPoints <= 0;
    
    public abstract override string ToString();
  }
}