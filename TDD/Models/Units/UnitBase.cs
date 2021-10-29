namespace TDD.Models.Units
{
  public abstract class UnitBase
  {
    public int Id { get; }
    public int HitPoints { get; protected set; }
    public bool Stationary { get; protected set; }
    public virtual bool Solid { get; protected set; }
    public bool IsDead() => HitPoints <= 0;

    protected UnitBase(int id)
    {
      Id = id;
      Solid = true;
    }

    public virtual void Damage(int amount)
    {
      HitPoints -= amount;
    }

    public virtual void OnOverlap(Board board, UnitBase overlappingUnit)
    {
      // Do nothing by default
    }

    public abstract override string ToString();
  }
}