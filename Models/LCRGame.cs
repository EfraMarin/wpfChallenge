using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfChallenge.Interfaces;

namespace wpfChallenge.Models
{
    public class LCRGame : IBoardGame
    {

        Queue<ILCRPlayerBase> _players;

        List<Dice> _dices;

        public ILCRPlayerBase Winner { get; private set; }

        public bool IsThereAWinner { get { return Winner != null; } private set { } }

        public int NumberOfPlayers
        {
            get { return _players.Count; }
        }

        public LCRGame(List<ILCRPlayerBase> players, List<Dice> dices)
        {
            if (players.Count < 3)
                throw new Exception("The minimum number of required players is 3");

            this._players = new Queue<ILCRPlayerBase>(players);

            this._dices = dices;

        }

        public void ProcessNextTurn()
        {
            ILCRPlayerBase player = this._players.Dequeue();

            var results = player.RollDices(this._dices);

            this._players.Enqueue(player);
        }

        void ApplyRules(List<DiceFaceType> results)
        {

        }
    }
}
