using Moq;
using NUnit.Framework;

namespace TDD
{
    [TestFixture]
    public class GameControllerTests
    {
        private Game _game;
        private Mock<IConsoleWrapper> _consoleMock;

        [SetUp]
        public void Setup()
        {
            _consoleMock = new Mock<IConsoleWrapper>();
            _consoleMock.Setup(console => console.ReadLine()).Returns("user_input");
            _game = new Game(_consoleMock.Object);
        }
        
        // [Test]
        // public void CanCreateGame()
        // {
        //     Assert.NotNull(_game);
        // }
        //
        // [Test]
        // public void GameCanAskForInput()
        // {
        //     var input = _game.AskForString();
        //     Assert.AreEqual("user_input", input);
        // }
    }
}