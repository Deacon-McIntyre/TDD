namespace TDD
{
    public interface IConsoleWrapper
    {
        public string ReadLine();
        public char ReadKey();
        public void WriteLine(string line);
        public void Clear();
    }
}