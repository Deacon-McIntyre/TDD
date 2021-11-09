using System;
using TDD.Models;
using TDD.Models.Enums;
using TDD.Models.Units;
using Void = TDD.Models.Units.Void;

namespace TDD
{
    class Program
    {
        private static int _idCounter = 1;
        private static ConsoleWrapper _consoleWrapper = new();
        private static readonly Board Board = new(6, 6);
        static void Main(string[] args)
        {
            PlaceNewWall(1, 1);
            PlaceNewWall(1, 2);
            PlaceNewWall(1, 3);
            PlaceNewWall(3, 3);
            PlaceNewWall(4, 3);

            for (var i = 0; i < 6; i++)
            {
                for (var j = 0; j < 6; j++)
                {
                    if (i == 0 || j == 0 || i == 5 || j == 5)
                    {
                        PlaceNewVoid(i, j);
                    }
                }
            }

            var mageId = GetId();
            var mage = new Mage(mageId);
            Board.TryPlace(mage, 3, 1);

            var view = new ConsoleView(Board, _consoleWrapper);

            view.PrintBoard();

            // Board.TryMoveUnitTo(mageId, 2, 4);
            // view.PrintBoard();
            // Board.TryMoveUnitTo(mageId, 1, 1);
            // view.PrintBoard();
            // Board.TryMoveUnitTo(mageId, 0, 0);
            // view.PrintBoard();


            while (true)
            {
                var input = _consoleWrapper.ReadKey();
                switch (input)
                {
                    case 'w':
                        Board.TryPush(mageId, Cardinal.North);
                        break;
                    case 's':
                        Board.TryPush(mageId, Cardinal.South);
                        break;
                    case 'd':
                        Board.TryPush(mageId, Cardinal.East);
                        break;
                    case 'a':
                        Board.TryPush(mageId, Cardinal.West);
                        break;
                }
                view.PrintBoard();
            }
        }

        private static void PlaceNewWall(int x, int y)
        {
            var wall = new Wall(GetId());

            Board.TryPlace(wall, x, y);
        }

        private static void PlaceNewVoid(int x, int y)
        {
            var voidTile = new Void(GetId());

            Board.TryPlace(voidTile, x, y);
        }

        private static int GetId()
        {
            _idCounter++;
            return _idCounter;
        }
    }
}