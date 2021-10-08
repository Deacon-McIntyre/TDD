using System.Collections.Generic;
using NUnit.Framework;
using TDD.Models;
using TDD.Models.Units;

namespace TDD.Tests
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

        #region Create
        
        [Test]
        public void CanCreateBoard()
        {
            Assert.NotNull(_board);
        }
        
        #endregion

        // ======================================================
        
        #region TryPlace
        
        [Test]
        public void TryPlace_BaseCase_PlacesUnit()
        {
            var unit = new Mage(UnitId);
            _board.TryPlace(unit, 0, 0);
            
            Assert.That(_board.UnitIds[0, 0], Is.EqualTo(UnitId));
        }

        [Test]
        public void TryPlace_UnitInTheWay_DoesNotPlaceReturnsFalse()
        {
            const int unitId2 = UnitId + 1;
            var unit = new Mage(UnitId);
            var unit2 = new Mage(unitId2);
            _board.TryPlace(unit, 0, 0);
            var success = _board.TryPlace(unit2, 0, 0);
            
            Assert.That(success, Is.False);
            Assert.That(_board.UnitIds[0, 0], Is.EqualTo(UnitId));
        }

        [TestCase(-1, -1)]
        [TestCase(2, -1)]
        [TestCase(-1, 2)]
        [TestCase(4, 4)]
        [TestCase(2, 4)]
        [TestCase(4, 2)]
        public void TryPlace_OutOfBounds_DoesNotPlaceReturnsFalse(int x, int y)
        {
            _board = new Board(4, 4);
            var unit = new Mage(UnitId);
            var success = _board.TryPlace(unit, x, y);
            
            Assert.That(success, Is.False);
        }
        #endregion
        
        // ======================================================

        #region TryMoveUnitTo
        
        [Test]
        public void TryMoveUnitTo_BaseCase_MovesUnitReturnsTrue()
        {
            var unit = new Mage(UnitId);
            _board.TryPlace(unit, 0, 0);
            var success = _board.TryMoveUnitTo(UnitId, 2, 2);
            
            Assert.That(success, Is.True);
            Assert.That(_board.UnitIds[2, 2], Is.EqualTo(UnitId));
            Assert.That(_board.UnitIds[0, 0], Is.Zero);
        }

        [Test]
        public void TryMoveUnitTo_MoveToSameLocation_HasNoEffectReturnsFalse()
        {
            var unit = new Mage(UnitId);
            _board.TryPlace(unit, 0, 0);
            var success = _board.TryMoveUnitTo(UnitId, 0, 0);
            
            Assert.That(success, Is.False);
            Assert.That(_board.UnitIds[0, 0], Is.EqualTo(UnitId));
        }

        [Test]
        public void TryMoveUnitTo_UnitIsNotOnTheBoard_ThrowsException()
        {
            Assert.Throws<KeyNotFoundException>(() => _board.TryMoveUnitTo(UnitId, 2, 2));
        }

        [Test]
        public void TryMoveUnitTo_UnitInTheWay_DoesNotMoveAndReturnsFalse()
        {
            const int unitId2 = UnitId + 1;
            var unit = new Mage(UnitId);
            var unit2 = new Mage(unitId2);
            _board.TryPlace(unit, 0, 0);
            _board.TryPlace(unit2, 2, 2);
            var success = _board.TryMoveUnitTo(UnitId, 2, 2);
            
            Assert.That(success, Is.False);
            Assert.That(_board.UnitIds[0, 0], Is.EqualTo(UnitId));
            Assert.That(_board.UnitIds[2, 2], Is.EqualTo(unitId2));
        }

        [TestCase(-1, -1)]
        [TestCase(2, -1)]
        [TestCase(-1, 2)]
        [TestCase(4, 4)]
        [TestCase(2, 4)]
        [TestCase(4, 2)]
        public void TryMoveUnitTo_OutOfBounds_DoesNotPlaceAndReturnsFalse(int x, int y)
        {
            var unit = new Mage(UnitId);
            _board.TryPlace(unit, 0, 0);
            var success = _board.TryMoveUnitTo(UnitId, x, y);
            
            Assert.That(success, Is.False);
        }

        #endregion
        
        // ======================================================
        
        #region MyRegion

        [Test]
        public void LookupUnit_UnitExists_ReturnsUnit()
        {
            var unit = new Mage(UnitId);
            _board.TryPlace(unit, 0, 0);
            var result = _board.LookupUnit(UnitId);
            
            Assert.That(result, Is.EqualTo(unit));
        }

        [Test]
        public void LookupUnit_UnitDoesNotExist_ThrowsException()
        {
            Assert.Throws<KeyNotFoundException>(() => _board.LookupUnit(0));
        }

        #endregion
        
        // ======================================================

        #region MyRegion

        [Test]
        public void DeleteUnit_UnitExists_DeletesUnit()
        {
            var unit = new Mage(UnitId);
            _board.TryPlace(unit, 0, 0);
            _board.DeleteUnit(UnitId);
            
            Assert.That(_board.UnitIds[0, 0], Is.Zero);
            Assert.Throws<KeyNotFoundException>(() => _board.LookupUnit(UnitId));
        }
        
        [Test]
        public void DeleteUnit_UnitDoesNotExist_ThrowsException()
        {
            Assert.Throws<KeyNotFoundException>(() => _board.DeleteUnit(UnitId));
        }

        #endregion
    }
}