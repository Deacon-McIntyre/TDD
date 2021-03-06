using System;

namespace TDD
{
    public interface IConsoleWrapper
    {
        public string ReadLine();
        public ConsoleKey ReadKey();
        public void WriteLine(string line);
        public void Clear();
    }
}