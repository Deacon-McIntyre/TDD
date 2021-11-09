namespace TDD
{
    public class GameController
    {
        private readonly IConsoleWrapper _console;

        public GameController(IConsoleWrapper console)
        {
            _console = console;
        }
        
        public string AskForInput()
        {
            var input = _console.ReadLine();
            return input;
        }
    }
}