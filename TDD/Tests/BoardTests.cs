using NUnit.Framework;
using TDD.Models;

namespace TDD
{
    [TestFixture]
    public class BoardTests
    {
        private Board _board;

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
            var unit = new Mage();
            _board.Place(unit, 0, 0);
            
            Assert.That(_board.Units[0, 0], Is.EqualTo(unit));
        }
    }
}