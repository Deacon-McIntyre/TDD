using TDD.Models.Enums;

namespace TDD.Models.Options
{
  public class StringOption : Option
  {
    public StringOption(string optionText, State nextState, bool selected = false) : base(optionText, nextState, selected)
    {
    }
  }
}