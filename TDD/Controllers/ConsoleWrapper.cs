using System;

namespace TDD
{
    public class ConsoleWrapper : IConsoleWrapper
    {
        public ConsoleWrapper()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public char ReadKey()
        {
            return Console.ReadKey(false).KeyChar;
        }
        
        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}