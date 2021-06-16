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

            var newGame = service.CreateNewLCRGame(1000);

            service.RunGame(newGame);

            Assert.IsNotNull(newGame);
        }
    }
}
