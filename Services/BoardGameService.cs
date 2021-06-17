using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfChallenge.Helpers;
using wpfChallenge.Interfaces;
using wpfChallenge.Models;

namespace wpfChallenge.Services
{
    public class BoardGameService
    {/// <summary>
     /// Create the dependencies needed to create a complete LCR game
     /// </summary>
     /// <param name="numberOfPlayers">Number of players in the game</param>
     /// <param name="randomGenerator"></param>
     /// <returns></returns>
        public LCRGame CreateNewLCRGame(int numberOfPlayers = 3, Random randomGenerator = null)
        {

            return new LCRGame(BoardGameHelpers.CreateBoardGamePlayers(numberOfPlayers),
                BoardGameHelpers.CreateDefaultDices(randomGenerator ?? new Random()),
                BoardGameHelpers.CreateDefaultRules());
        }

        /// <summary>
        /// Starts the given game and processes turns until there is a Winner
        /// </summary>
        /// <param name="game">The game to run</param>
        /// <returns></returns>
        public LCRGame RunGame(LCRGame game)
        {
            while (!game.IsThereAWinner)
            {
                game.ProcessNextTurn();
                game.CheckForWinner();
            }
#if DEBUG
            Console.WriteLine($"After {game.TurnsTaken} turns, the winner is player {game.Winner.Id}");
#endif
            return game;
        }

        public async Task<LCRGame> RunGameAsync(LCRGame game)
        {
            await Task.Run(() =>
            {
                RunGame(game);
            });

            return game;
        }
    }
}
