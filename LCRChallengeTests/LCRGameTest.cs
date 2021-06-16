using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using wpfChallenge.Helpers;
using wpfChallenge.Models;

namespace LCRChallengeTests
{
    [TestClass]
    public class LCRGameTest
    {
        [TestMethod]
        public void LessThanMinimumPlayserShouldThrowException()
        {
            Random random = new Random();
            Action instantiation = () =>
            {
                LCRGame game = new LCRGame(BoardGameHelpers.CreateBoardGamePlayers(2),
                    BoardGameHelpers.CreateDefaultDices(random),
                    BoardGameHelpers.CreateDefaultRules());

            };

            Assert.ThrowsException<Exception>(instantiation);
        }
    }
}
