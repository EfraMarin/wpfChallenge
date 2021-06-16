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
        List<DiceFaceType> RollDices(List<Dice> dices);

        void PassChipsToPlayer(int chips, ILCRPlayerBase player);

        int IncreaseChips(int count = 1);

        int RemainingChips { get; }

    }
}
