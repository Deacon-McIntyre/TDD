using System.Collections.Generic;
using NUnit.Framework;
using TDD.Models;
using TDD.Models.Units;

namespace TDD.Tests
{
  [TestFixture]
  public class VoidTests
  {
    private const int VoidId = 1;
    private const int OtherId = 2;
    private Board _board;

    [SetUp]
    public void Setup()
    {
      _board = new Board();
    }

    [Test]
    public void OnOverlap_OverlappingUnitIsDeleted()
    {
      var voidUnit = new Void(VoidId);
      _board.TryPlace(voidUnit, 0, 0);

      var otherUnit = new Mage(OtherId);
      _board.TryPlace(otherUnit, 1, 0);

      _board.TryMoveUnitTo(OtherId, 0, 0);

      Assert.Throws<KeyNotFoundException>(() => _board.LookupUnit(OtherId));
    }
  }
}