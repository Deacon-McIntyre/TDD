using System;

namespace TDD
{
    public class ConsoleWrapper : IConsoleWrapper
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
        
        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }
    }
}