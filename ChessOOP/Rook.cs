using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessOOP
{
    public class Rook : Figure //Ладья
    {
        public Rook(Player player) : base(player) { }

        public override Image Img
        {
            get
            {
                return base.GetImage(4);
            }
        }

        public override List<(int, int)> GetPossibleMoves(ChessField field)
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
                if (!blocked1 && ValidateMove(y + i, x)) //1
                {
                    if (field[y + i, x] != null)
                    {
                        if (field[y + i, x].player == enemy)
                        {
                            blocked1 = true;
                            list.Add((y + i, x));
                        }
                        if (field[y + i, x].player == player)
                        {
                            blocked1 = true;
                        }
                    }
                    if (field[y + i, x] == null)
                        list.Add((y + i, x));
                }
                if (!blocked2 && ValidateMove(y, x - i)) //2
                {
                    if (field[y, x - i] != null)
                    {
                        if (field[y, x - i].player == enemy)
                        {
                            blocked2 = true;
                            list.Add((y, x - i));
                        }
                        if (field[y, x - i].player == player)
                        {
                            blocked2 = true;
                        }
                    }
                    if (field[y, x - i] == null)
                        list.Add((y, x - i));
                }
                if (!blocked3 && ValidateMove(y - i, x)) //3
                {
                    if (field[y - i, x] != null)
                    {
                        if (field[y - i, x].player == enemy)
                        {
                            blocked3 = true;
                            list.Add((y - i, x));
                        }
                        if (field[y - i, x].player == player)
                        {
                            blocked3 = true;
                        }
                    }
                    if (field[y - i, x] == null)
                        list.Add((y - i, x));
                }
                if (!blocked4 && ValidateMove(y, x + i)) //4
                {
                    if (field[y, x + i] != null)
                    {
                        if (field[y, x + i].player == enemy)
                        {
                            blocked4 = true;
                            list.Add((y, x + i));
                        }
                        if (field[y, x + i].player == player)
                        {
                            blocked4 = true;
                        }
                    }
                    if (field[y, x + i] == null)
                        list.Add((y, x + i));
                }
            }
            return list;
        }
    }
}
