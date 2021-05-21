using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Game
{
    public struct Coord : IComparable<Coord>
    {
        public readonly int file;
        public readonly int rank;

        public Coord(int file, int rank)
        {
            this.file = file;
            this.rank = rank;
        }

        public bool IsLightSquare()
        {
            return (this.file + this.rank) % 2 != 0;
        }

        public int CompareTo(Coord other)
        {
            return (this.file == other.file && this.rank == other.rank) ? 0 : 1;
        }
    }
}
