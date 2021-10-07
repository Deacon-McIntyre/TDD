using System;

namespace TDD.Models
{
  public class Mage : IUnit
  {
    public Tuple<int, int> Coords { get; }

    public override string ToString()
    {
      return "M";
    }
  }
}