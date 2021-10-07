using NUnit.Framework;
using TDD.Models;

namespace TDD
{
    [TestFixture]
    public class BoardTests
    {
        private Board _board;
        private const int UnitId = 1; 

        [SetUp]
        public void Setup()
        {
            _board = new Board();
        }
        
        [Test]
        public void CanCreateBoard()
        {
            Assert.NotNull(_board);
        }

        [Test]
        public void CanPlaceUnitOnBoard()
        {
            var unit = new Mage(UnitId);
            _board.Place(unit, 0, 0);
            
            Assert.That(_board.UnitIds[0, 0], Is.EqualTo(UnitId));
        }

        [Test]
        public void CanMoveUnitOnBoard()
        {
            var unit = new Mage(UnitId);
            _board.Place(unit, 0, 0);
            _board.MoveUnitTo(UnitId, 2, 2);
            
            Assert.That(_board.UnitIds[2, 2], Is.EqualTo(UnitId));
            Assert.That(_board.UnitIds[0, 0], Is.Zero);
        }
    }
}