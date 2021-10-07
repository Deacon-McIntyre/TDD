using System;
using TDD.Models.Units;

namespace TDD.Models
{
  public class Mage : IUnit
  {
    public int Id { get; }

    public Mage(int id)
    {
      Id = id;
    }

    public override string ToString()
    {
      return "M";
    }
  }
}