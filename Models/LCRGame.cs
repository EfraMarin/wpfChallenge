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

        Dictionary<DiceFaceType, Action<ILCRPlayerBase>> _rulesToApply;

        public ILCRPlayerBase Winner { get; private set; }

        public bool IsThereAWinner { get => Winner != null; }

        public int NumberOfPlayers
        {
            get { return _players.Count; }
        }

        public int TurnsTaken { get => _turnsTaken; }

        public LCRGame(List<ILCRPlayerBase> players, List<Dice> dices, Dictionary<DiceFaceType, Action<ILCRPlayerBase>> rulesDefinition)
        {
            if (players.Count < 3)
                throw new Exception("The minimum number of required players is 3");

            this._players = new Queue<ILCRPlayerBase>(players);

            this._dices = dices;

            this._rulesToApply = rulesDefinition;

        }

        ~LCRGame() { }

        /// <summary>
        /// Process the next turn in case that there is no winner yet.
        /// </summary>
        public void ProcessNextTurn()
        {
            ILCRPlayerBase player = this._players.Dequeue();

            var results = player.RollDices(this._dices.Take(player.RemainingChips).ToList());

            ApplyRules(results, player);

            this._players.Enqueue(player);

            this._turnsTaken++;
        }

        /// <summary>
        /// Check if there is an actual win and sets the property <see cref="Winner"/>
        /// </summary>
        public void CheckForWinner()
        {
            if (this._players.Where(p => p.RemainingChips > 0).Take(2).Count() == 1)
                this.Winner = this._players.Where(p => p.RemainingChips > 0).First();

        }

        /// <summary>
        /// Apply the rules according to the resulting faces after rolling the dices (see <seealso cref="Dice.Roll"/>)
        /// </summary>
        /// <param name="results"></param>
        /// <param name="player"></param>
        void ApplyRules(List<DiceFaceType> results, ILCRPlayerBase player)
        {
            foreach (var face in results)
                _rulesToApply[face](player);
        }
    }
}
