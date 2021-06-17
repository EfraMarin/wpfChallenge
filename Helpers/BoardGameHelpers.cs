using System.Collections.Generic;
using wpfChallenge.Models;
using System.Linq;
using wpfChallenge.Interfaces;
using System;

namespace wpfChallenge.Helpers
{
    public class BoardGameHelpers
    {
        /// <summary>
        /// Create the specified number of players
        /// </summary>
        /// <param name="count">Number of players to create</param>
        /// <returns>A list of players</returns>
        public static List<ILCRPlayerBase> CreateBoardGamePlayers(int count)
        {
            List<ILCRPlayerBase> result = new List<ILCRPlayerBase>(count);

            for (int i = 0; i < count; i++)
            {
                result.Add(new Player() { Id = i + 1 });
                if (result.Count > 1)
                    result.Last().SitNextTo(result[i - 1]);
            }

            result.Last().SitNextTo(result.First(), false);

            return result;

        }

        /// <summary>
        /// Create a default list of 3 dices
        /// </summary>
        /// <param name="randomGenerator">An object of type Random for generating random values</param>
        /// <returns>A list of dices</returns>
        public static List<Dice> CreateDefaultDices(Random randomGenerator = null)
        {
            Dice defaultDice = new Dice(new List<DiceFaceType>()
                {
                    DiceFaceType.Dot,
                    DiceFaceType.Dot,
                    DiceFaceType.Dot,
                    DiceFaceType.L,
                    DiceFaceType.C,
                    DiceFaceType.R,
                }, randomGenerator ?? new Random());

            return new List<Dice>()
            {
                (Dice)defaultDice.Clone(),
                (Dice)defaultDice.Clone(),
                (Dice)defaultDice.Clone(),
            };
        }

        /// <summary>
        /// Create a set of default rules to apply every time a user roll the dices
        /// </summary>
        /// <returns>A dictionary of actions identified by a dice face type</returns>
        public static Dictionary<DiceFaceType, Action<ILCRPlayerBase>> CreateDefaultRules()
        {
            return new Dictionary<DiceFaceType, Action<ILCRPlayerBase>>
            {
                [DiceFaceType.Dot] = (p) => { },

                [DiceFaceType.L] = (p) => p.PassChipsToPlayer(1, p.PlayerToLeft),

                [DiceFaceType.C] = (p) => p.PassChipsToPlayer(1, null),

                [DiceFaceType.R] = (p) => p.PassChipsToPlayer(1, p.PlayerToRight)
            };
        }

    }
}