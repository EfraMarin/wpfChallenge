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
    {
        public LCRGame CreateNewLCRGame(int numberOfPlayers = 3)
        {
            return new LCRGame(BoardGameHelpers.CreateBoardGamePlayers(numberOfPlayers),
                BoardGameHelpers.CreateDefaultDices(),
                BoardGameHelpers.CreateDefaultRules());
        }

        public void RunGame(LCRGame game)
        {
            while (!game.IsThereAWinner)
            {
                game.ProcessNextTurn();
                game.CheckForWinner();
            }

            Console.WriteLine($"After {game.TurnsTaken} turns, the winner is player {game.Winner.Id}");
        }

        public async Task RunGameAsync(LCRGame game)
        {
            await Task.Run(() =>
            {
                RunGame(game);
            });
        }
    }
}
