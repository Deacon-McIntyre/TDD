using System;

namespace TDD.Models.Units
{
  public interface IUnit
  {
    public int Id { get; }
    // I want to make inheritors implement toString() so might need to make this an abstract class...
  }
}