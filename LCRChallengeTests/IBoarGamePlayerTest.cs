using Microsoft.VisualStudio.TestTools.UnitTesting;
using wpfChallenge.Models;

namespace LCRChallengeTests
{
    [TestClass]
    public class IBoarGamePlayerTest
    {
        [TestMethod]
        public void PlayerCantSitInOccupiedPlace()
        {
            Player player = new Player(), 
                player1 = new Player(), 
                player2 = new Player();

            player1.SitNextTo(player);

            Assert.IsFalse(player2.SitNextTo(player));

        }
    }
}
