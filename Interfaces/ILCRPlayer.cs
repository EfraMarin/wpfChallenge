using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfChallenge.Models;

namespace wpfChallenge.Interfaces
{
    public interface ILCRPlayer
    {
        /// <summary>
        /// Simulates a player rolling the dices
        /// </summary>
        /// <param name="dices">A list of dices to roll</param>
        /// <returns>A list of resulting dice faces</returns>
        List<DiceFaceType> RollDices(List<Dice> dices);

        /// <summary>
        /// Simulates transfering chips from a player to another
        /// </summary>
        /// <param name="chips">Number of chips to transfer to another player</param>
        /// <param name="player">The player to transfer chips</param>
        void PassChipsToPlayer(int chips, ILCRPlayerBase player);

        /// <summary>
        /// Increases the number of chips the user have
        /// </summary>
        /// <param name="count">Number of chips to increase</param>
        /// <returns>The nomber of the player remaining chips</returns>
        int IncreaseChips(int count = 1);

        int RemainingChips { get; }

    }
}
