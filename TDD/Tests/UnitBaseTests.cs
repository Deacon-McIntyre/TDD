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
      var originalHitPoints = unit.HitPoints;
      unit.Damage(amount);
      
      Assert.That(unit.HitPoints, Is.EqualTo(originalHitPoints-amount));
    }

    [Test]
    public void Damage_DealZeroDamage_DoesNotChangeHitPoints()
    {
      var unit = new UnitBaseTestClass(UnitId);
      var originalHitPoints = unit.HitPoints;
      unit.Damage(0);
      
      Assert.That(unit.HitPoints, Is.EqualTo(originalHitPoints));
    }

    [TestCase(-1)]
    [TestCase(-5)]
    public void Damage_DealNegativeDamage_IncreasesHitPoints(int amount)
    {
      var unit = new UnitBaseTestClass(UnitId);
      var originalHitPoints = unit.HitPoints;
      unit.Damage(amount);
      
      Assert.That(unit.HitPoints, Is.EqualTo(originalHitPoints-amount));
    }

    #endregion
  }

  internal class UnitBaseTestClass : UnitBase
  {
    public UnitBaseTestClass(int id) : base(id)
    {
    }
    
    public override string ToString()
    {
      return "Test";
    }
  }
}