using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TDD.Models;
using TDD.Models.Units;

namespace TDD
{
  [TestFixture]
  public class ConsoleViewTests
  {
    private ConsoleView _view;
    private Mock<IBoard> _boardMock;
    private Mock<IConsoleWrapper> _consoleMock;
    
    private List<string> _consoleMessages;

    [SetUp]
    public void Setup()
    {
      _boardMock = new Mock<IBoard>();
      _consoleMock = new Mock<IConsoleWrapper>();
      _consoleMock.Setup(c => c.WriteLine(It.IsAny<string>())).Callback(
        (string s) =>
        {
          _consoleMessages.Add(s);
        }
      );
      _view = new ConsoleView(_consoleMock.Object);

      _consoleMessages = new List<string>();
    }
      
    [Test]
    public void CanDisplayEmptyBoard()
    {
      SetupBoard(4, 4);
      _view.PrintBoard(_boardMock.Object);
      
      _consoleMock.Verify(c => c.WriteLine(It.IsAny<string>()), Times.Exactly(4));
      Assert.That(_consoleMessages[0], Is.EqualTo("            "));
      Assert.That(_consoleMessages[1], Is.EqualTo("            "));
      Assert.That(_consoleMessages[2], Is.EqualTo("            "));
      Assert.That(_consoleMessages[3], Is.EqualTo("            "));
    }

    [Test]
    public void CanDisplayBoardWithUnits()
    {
      var unitCoords = new List<Tuple<int, int>>
      {
        new(0, 0),
        new(1, 2)
      };
      SetupBoard(4, 4, unitCoords);
      _view.PrintBoard(_boardMock.Object);
      
      _consoleMock.Verify(c => c.WriteLine(It.IsAny<string>()), Times.Exactly(4));
      Assert.That(_consoleMessages[0], Is.EqualTo(" M          "));
      Assert.That(_consoleMessages[1], Is.EqualTo("            "));
      Assert.That(_consoleMessages[2], Is.EqualTo("    M       "));
      Assert.That(_consoleMessages[3], Is.EqualTo("            "));
    }

    private void SetupBoard(int x, int y, List<Tuple<int, int>> unitCoords = null)
    {
      var (units, unitMap) = GetTestBoard(4, 4, unitCoords);
      _boardMock.SetupGet(b => b.UnitIds).Returns(units);
      _boardMock.Setup(b => b.LookupUnit(It.IsAny<int>())).Returns<int>(id => unitMap[id]);
    }

    private Tuple<int[,], Dictionary<int, UnitBase>> GetTestBoard(int x, int y, List<Tuple<int, int>> unitCoords = null)
    {
      var units = new int[x, y];
      var unitMap = new Dictionary<int, UnitBase>();
      var idCounter = 1;
      
      if (unitCoords != null)
      {
        foreach (var (unitX, unitY) in unitCoords)
        {
          units[unitX, unitY] = idCounter;
          unitMap.Add(idCounter, new Mage(idCounter));
          idCounter++;
        }
      }

      return new Tuple<int[,], Dictionary<int, UnitBase>>(units, unitMap);
    }
  }
}