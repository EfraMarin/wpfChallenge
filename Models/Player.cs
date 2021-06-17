using System.Collections.Generic;
using wpfChallenge.Interfaces;

namespace wpfChallenge.Models
{
    public class Player : ILCRPlayerBase
    {
        int _chips = 3;
        public ILCRPlayerBase PlayerToLeft { get; set; }

        public ILCRPlayerBase PlayerToRight { get; set; }

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int RemainingChips { get => _chips; }

        public Player()
        {
        }
        
        public bool SitNextTo(ILCRPlayerBase player, bool toLeft = true)
        {
            /**
      *     1
      *   4   2
      *     3
      */
            if (toLeft)
            {
                if (player.PlayerToLeft != null) return false;
                this.PlayerToRight = player;
                player.PlayerToLeft = this;
            }
            else
            {
                if (player.PlayerToRight != null) return false;
                this.PlayerToLeft = player;
                player.PlayerToRight = this;
            }

            return true;
        }

        public List<DiceFaceType> RollDices(List<Dice> dices)
        {
            List<DiceFaceType> results = new List<DiceFaceType>(dices.Count);
            foreach (Dice d in dices)
                results.Add(d.Roll());

            return results;
        }

        public void PassChipsToPlayer(int chips, ILCRPlayerBase player)
        {
            this._chips--;

            if (player is null) return;

            player.IncreaseChips();

        }

        public int IncreaseChips(int count = 1)
        {
            this._chips += count;
            return RemainingChips;
        }
    }
}
