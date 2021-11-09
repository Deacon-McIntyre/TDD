using Moq;
using NUnit.Framework;

namespace TDD
{
    [TestFixture]
    public class GameControllerTests
    {
        private GameController _controller;
        private Mock<IConsoleWrapper> _consoleMock;

        [SetUp]
        public void Setup()
        {
            _consoleMock = new Mock<IConsoleWrapper>();
            _consoleMock.Setup(console => console.ReadLine()).Returns("user_input");
            _controller = new GameController(_consoleMock.Object);
        }
        
        [Test]
        public void CanCreateGame()
        {
            Assert.NotNull(_controller);
        }
        
        [Test]
        public void GameCanAskForInput()
        {
            var input = _controller.AskForInput();
            Assert.AreEqual("user_input", input);
        }
    }
}