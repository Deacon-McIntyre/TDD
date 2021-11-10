using NUnit.Framework;
using TDD.Models.Units;

namespace TDD.Tests
{
  [TestFixture]
  public class UnitBaseTests
  {
    private const int UnitId = 1;
    
    [SetUp]
    public void Setup()
    {
    }

    #region Create
      
    [Test]
    public void CreateUnit_GivenId_HasId()
    {
      var unit = new UnitBaseTestClass(UnitId);
      Assert.That(unit.Id, Is.EqualTo(UnitId));
    }

    #endregion
    
    // ======================================================
      
    #region Damage

    [TestCase(1)]
    [TestCase(5)]
    public void Damage_DealPositiveDamage_ReducesHitPoints(int amount)
    {
      var unit = new UnitBaseTestClass(UnitId);
      unit.Damage(amount);
      
      Assert.That(unit.HitPoints, Is.EqualTo(5-amount));
    }

    [Test]
    public void Damage_DealZeroDamage_DoesNotChangeHitPoints()
    {
      var unit = new UnitBaseTestClass(UnitId);
      unit.Damage(0);
      
      Assert.That(unit.HitPoints, Is.EqualTo(5));
    }

    [TestCase(-1)]
    [TestCase(-5)]
    public void Damage_DealNegativeDamage_IncreasesHitPoints(int amount)
    {
      var unit = new UnitBaseTestClass(UnitId);
      unit.Damage(amount);
      
      Assert.That(unit.HitPoints, Is.EqualTo(5-amount));
    }

    #endregion
    
    // ======================================================
      
    #region IsDead

    [Test]
    public void IsDead_UnitHasPositiveHitPoints_ReturnsFalse()
    {
      var unit = new UnitBaseTestClass(UnitId);
      
      Assert.That(unit.IsDead, Is.False);
    }

    [Test]
    public void IsDead_UnitHasZeroHitPoints_ReturnsTrue()
    {
      var unit = new UnitBaseTestClass(UnitId);
      unit.Damage(5);
      
      Assert.That(unit.IsDead, Is.True);
    }

    [Test]
    public void IsDead_UnitHasNegativeHitPoints_ReturnsTrue()
    {
      var unit = new UnitBaseTestClass(UnitId);
      unit.Damage(9);
      
      Assert.That(unit.IsDead, Is.True);
    }

    #endregion
  }

  internal class UnitBaseTestClass : UnitBase
  {
    public UnitBaseTestClass(int id) : base(id)
    {
      HitPoints = 5;
    }
    
    public override string ToString()
    {
      return "Test";
    }

    public override string Description()
    {
      return "A test unit.";
    }
  }
}