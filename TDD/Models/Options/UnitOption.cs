using TDD.Models.Enums;
using TDD.Models.Units;

namespace TDD.Models.Options
{
  public class UnitOption : Option
  {
    public UnitBase Unit { get; }

    public UnitOption(UnitBase unit, State nextState, bool selected = false) : base(unit.ToString(), nextState, selected)
    {
      Unit = unit;
    }
  }
}