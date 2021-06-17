using Microsoft.VisualStudio.TestTools.UnitTesting;
using wpfChallenge.Services;

namespace LCRChallengeTests
{
    [TestClass]
    public class BoardGameServiceTest
    {
        [TestMethod]
        public void BoardGameServiceCanCreateNewLCRGame()
        {
            BoardGameService service = new BoardGameService();

            var newGame = service.CreateNewLCRGame(3);

            Assert.IsNotNull(newGame);
        }

        [TestMethod]
        public void ThereMustBeAWinner()
        {
            BoardGameService service = new BoardGameService();
            var result = service.RunGame(service.CreateNewLCRGame(3));

            Assert.IsTrue(result.IsThereAWinner && result.Winner != null);

        }
    }
}
