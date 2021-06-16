using System.Collections.Generic;
using wpfChallenge.Models;
using System.Linq;
using wpfChallenge.Interfaces;

namespace wpfChallenge.Helpers
{
    public class BoardGameHelpers
    {
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

        public static List<Dice> CreateDefaultDices()
        {
            Dice defaultDice = new Dice(new List<DiceFaceType>()
                {
                    DiceFaceType.Dot,
                    DiceFaceType.Dot,
                    DiceFaceType.Dot,
                    DiceFaceType.L,
                    DiceFaceType.C,
                    DiceFaceType.R,
                });

            return new List<Dice>()
            {
                (Dice)defaultDice.Clone(),
                (Dice)defaultDice.Clone(),
                (Dice)defaultDice.Clone(),
            };
        }
    }
}
