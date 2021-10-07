namespace TDD.Models
{
  public interface IBoard
  {
    public IUnit[,] Units { get; }
    public void Place(Mage unit, int x, int y);
  }
}