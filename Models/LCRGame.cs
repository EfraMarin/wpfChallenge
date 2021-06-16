using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfChallenge.Interfaces;

namespace wpfChallenge.Models
{
    public class LCRGame
    {

        List<IBoarGamePlayer> _players;
        
        List<Dice> _dices;

        public IBoarGamePlayer Winner { get; private set; }

        public LCRGame(List<IBoarGamePlayer> players, List<Dice> dices)
        {
            if (players.Count < 3)
                throw new Exception("The minimum number of required players is 3");
            
            this._players = players;
            
            this._dices = dices;

        }

        public int NumberOfPlayers
        {
            get { return _players.Count; }
        }



    }
}
