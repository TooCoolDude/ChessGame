using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessOOP
{
    public class Knight : Figure //Конь
    {
        public Knight(Player player) : base(player) { }

        public override Image Img
        {
            get
            {
                return player == Player.White ? GetWhiteImage : GetBlackImage;
            }
        }

        private static Image GetWhiteImage { get; } = GetImage(3, Player.White);

        private static Image GetBlackImage { get; } = GetImage(3, Player.Black);

        public override Figure Copy()
        {
            var c = new Knight(this.player);
            c.CurrentPosition = this.CurrentPosition;
            return c;
        }

        protected override List<(int, int)> GetPossibleMoves(ChessField field)
        {
            var y = CurrentPosition.Item1;
            var x = CurrentPosition.Item2;
            var list = new List<(int, int)>();
            int reverse = player == Player.White ? -1 : 1;
            var enemy = player == Player.White ? Player.Black : Player.White;

            var moves = new[] { (y + 2, x + 1), (y + 2, x - 1), (y + 1, x + 2), (y + 1, x - 2),
                (y - 2, x + 1), (y - 2, x - 1), (y - 1, x + 2), (y - 1, x - 2) };
            
            for (int i = 0; i < 8; i++)
            {
                var newY = moves[i].Item1;
                var newX = moves[i].Item2;
                if (ValidateMove(newY, newX))
                {
                    if ((field[newY, newX] != null && field[newY, newX].player == enemy) || field[newY, newX] == null)
                    {
                        list.Add((newY, newX));
                    }
                }
            } 
            return list;
        }
    }
}
