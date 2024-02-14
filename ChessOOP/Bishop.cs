using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessOOP
{
    public class Bishop : Figure //Слон
    {
        public Bishop(Player player) : base(player) { }

        public override Image Img
        {
            get
            {
                return player == Player.White ? GetWhiteImage : GetBlackImage;
            }
        }

        private static Image GetWhiteImage { get; } = GetImage(2, Player.White);

        private static Image GetBlackImage { get; } = GetImage(2, Player.Black);

        public override Figure Copy()
        {
            var c = new Bishop(this.player);
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

            bool blocked1 = false;
            bool blocked2 = false;
            bool blocked3 = false;
            bool blocked4 = false;
            
            for (int i = 1; i < 8; i++)
            {
                if (!blocked1 && ValidateMove(y + i, x + i)) //1
                {
                    if (field[y + i, x + i] != null)
                    {
                        if (field[y + i, x + i].player == enemy)
                        {
                            blocked1 = true;
                            list.Add((y + i, x + i));
                        }
                        if (field[y + i, x + i].player == player)
                        {
                            blocked1 = true;
                        }
                    }
                    if (field[y + i, x + i] == null)
                        list.Add((y + i, x + i));
                }
                if (!blocked2 && ValidateMove(y + i, x - i)) //2
                {
                    if (field[y + i, x - i] != null)
                    {
                        if (field[y + i, x - i].player == enemy)
                        {
                            blocked2 = true;
                            list.Add((y + i, x - i));
                        }
                        if (field[y + i, x - i].player == player)
                        {
                            blocked2 = true;
                        }
                    }
                    if (field[y + i, x - i] == null)
                        list.Add((y + i, x - i));
                }
                if (!blocked3 && ValidateMove(y - i, x - i)) //3
                {
                    if (field[y - i, x - i] != null)
                    {
                        if (field[y - i, x - i].player == enemy)
                        {
                            blocked3 = true;
                            list.Add((y - i, x - i));
                        }
                        if (field[y - i, x - i].player == player)
                        {
                            blocked3 = true;
                        }
                    }
                    if (field[y - i, x - i] == null)
                        list.Add((y - i, x - i));
                }
                if (!blocked4 && ValidateMove(y - i, x + i)) //4
                {
                    if (field[y - i, x + i] != null)
                    {
                        if (field[y - i, x + i].player == enemy)
                        {
                            blocked4 = true;
                            list.Add((y - i, x + i));
                        }
                        if (field[y - i, x + i].player == player)
                        {
                            blocked4 = true;
                        }
                    }
                    if (field[y - i, x + i] == null)
                        list.Add((y - i, x + i));
                }
            }
            return list;
        }
    }
}
