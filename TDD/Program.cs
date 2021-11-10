namespace TDD
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game(new ConsoleWrapper());
            game.Run();
        }
    }
}