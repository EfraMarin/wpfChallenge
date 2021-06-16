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
        int _turnsTaken = 0;

        Queue<ILCRPlayerBase> _players;

        List<Dice> _dices;

        public ILCRPlayerBase Winner { get; private set; }

        public bool IsThereAWinner { get { return Winner != null; } private set { } }

        public int NumberOfPlayers
        {
            get { return _players.Count; }
        }

        public int TurnsTaken { get => _turnsTaken; }

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

            ApplyRules(results, player);

            this._players.Enqueue(player);

            this._turnsTaken++;
        }

        void ApplyRules(List<DiceFaceType> results, ILCRPlayerBase player)
        {

        }
    }
}
