namespace TDD.Models
{
  public class Board : IBoard
  {
    public IUnit[,] Units { get; }

    public Board()
    {
      Units = new IUnit[4, 4];
    }

    public void Place(Mage unit, int x, int y)
    {
      Units[x,y] = unit;
    }
  }
}