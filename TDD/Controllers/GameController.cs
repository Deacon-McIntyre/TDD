namespace TDD
{
    public class Game
    {
        private readonly IConsoleWrapper _console;

        public Game(IConsoleWrapper console)
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