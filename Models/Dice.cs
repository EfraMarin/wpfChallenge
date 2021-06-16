using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfChallenge.Models
{
    public enum DiceFaceType
    {
        Dot,
        L,
        C,
        R
    }
    public class Dice: ICloneable
    {
        private Random _random;

        private List<DiceFaceType> _faces;

        public int SidesCount
        {
            get { return this._faces.Count; }
        }

        public Dice(List<DiceFaceType> faces, Random randomGenerator)
        {
            this._faces = faces;
            this._random = randomGenerator;
        }

        public DiceFaceType Roll()
        {
            return this._faces[this._random.Next(this._faces.Count)];
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
