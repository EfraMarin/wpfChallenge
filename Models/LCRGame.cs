using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfChallenge.Models
{
    public class LCRGame
    {
        public LCRGame(int numberOfPlayers = 3)
        {
            if (numberOfPlayers < 3)
                throw new Exception("The minimum number of required players is 3");

            this._numberOfPlayers = numberOfPlayers;
        }

        private int _numberOfPlayers;

        public int NumberOfPlayers
        {
            get { return _numberOfPlayers; }
            private set { _numberOfPlayers = value; }
        }



    }
}
