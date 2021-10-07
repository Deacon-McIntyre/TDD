using System;

namespace TDD.Models
{
  public interface IUnit
  {
    public Tuple<int, int> Coords { get; }
  }
}