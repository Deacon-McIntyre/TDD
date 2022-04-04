using TDD.Models.Enums;

namespace TDD.Models.Options
{
  public abstract class Option
  {
    public State NextState { get; }
    public bool Selected { get; set; }

    private string _optionText;

    public Option(string optionText, State nextState, bool selected = false)
    {
      NextState = nextState;
      Selected = selected;
      _optionText = optionText;
    }

    public override string ToString()
    {
      return $"{(Selected ? '⮞' : " ")} {_optionText}";
    }
  }
}