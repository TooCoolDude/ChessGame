using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessOOP
{
    public enum Player : int
    {
        White = 1,
        Black = 2
    }

    public abstract class Figure
    {
        public readonly Player player;

        public (int, int) CurrentPosition;

        private static Bitmap Images = new ("E:\\dev\\ChessOOP\\ChessOOP\\images\\chess.png");

        public Figure(Player player)
        { 
            this.player = player;
        }

        protected Image GetImage(int figure)
        {
            var i = Images.Clone(new Rectangle(333 * figure, ((int)player - 1) * 333, 333, 333),
                        Images.PixelFormat);
            return new Bitmap(i, new Size(200, 200));
        }

        public virtual Image Img { get { return Images; } }

        public virtual List<(int, int)> GetPossibleMoves(ChessField field)
        {
            return new List<(int, int)>();
        }

        public static bool ValidateMove(int y, int x)
        {
            if (y < 8 && y >= 0 && x < 8 && x >= 0)
                return true;
            return false;
       }

        public virtual void MakeMove((int, int) nextPosition, ChessField field)
        {
            if (GetPossibleMoves(field).Contains(nextPosition))
            {
                field[nextPosition.Item1, nextPosition.Item2] = field[CurrentPosition.Item1, CurrentPosition.Item2];
                field[CurrentPosition.Item1, CurrentPosition.Item2] = null;
                field.NextPlayer();
            }
        }

    }
}
