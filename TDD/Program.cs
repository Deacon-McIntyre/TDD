using System;
using TDD.Models;
using TDD.Models.Units;

namespace TDD
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var board = new Board();

            var wall1 = new Wall(1);
            var wall2 = new Wall(2);
            var wall3 = new Wall(3);
            var wall4 = new Wall(4);

            board.TryPlace(wall1, 0, 0);
            board.TryPlace(wall2, 0, 1);
            board.TryPlace(wall3, 0, 2);
            board.TryPlace(wall4, 0, 3);

            var view = new ConsoleView(board, new ConsoleWrapper());

            view.PrintBoard();
        }
    }
}