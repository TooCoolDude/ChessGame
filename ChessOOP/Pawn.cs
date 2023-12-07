using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessOOP
{
    public class Pawn : Figure //Пешка
    {
        public Pawn(Player player) : base(player) { }

        public override Image Img 
        {
            get
            {
                return base.GetImage(5);
            }
        }
        
        bool isFirstMove = true;

       public override List<(int, int)> GetPossibleMoves(ChessField field)
       {
            var y = CurrentPosition.Item1;
            var x = CurrentPosition.Item2;
            var list = new List<(int, int)>();
            int reverse = player == Player.White ? -1 : 1;
            var enemy = player == Player.White ? Player.Black : Player.White;

            if (ValidateMove(y + 1 * reverse, x))
            {
                bool blocked = field[y + 1 * reverse, x] != null;
                if (!blocked)
                {
                    list.Add((y + 1 * reverse, x));
                }
                if (!blocked && ValidateMove(y + 2 * reverse, x))
                {
                    bool blockedFirstMove = field[y + 2 * reverse, x] != null;
                    if (!blockedFirstMove && isFirstMove)
                    {
                        list.Add((y + 2 * reverse, x));
                    }
                }
            }
            if (ValidateMove(y + 1 * reverse, x + 1) && field[y + 1 * reverse, x + 1] != null && field[y + 1 * reverse, x + 1].player == enemy)
            {
                list.Add((y + 1 * reverse, x + 1));
            }
            if (ValidateMove(y + 1 * reverse, x - 1) && field[y + 1 * reverse, x - 1] != null && field[y + 1 * reverse, x - 1].player == enemy)
            {
                list.Add((y + 1 * reverse, x - 1));
            }
            return list;
       }

        public override void MakeMove((int, int) nextPosition, ChessField field)
        {
            if (GetPossibleMoves(field).Contains(nextPosition))
            {
                field[nextPosition.Item1, nextPosition.Item2] = field[CurrentPosition.Item1, CurrentPosition.Item2];
                field[CurrentPosition.Item1, CurrentPosition.Item2] = null;
                field.NextPlayer();
            }
            isFirstMove = false;
        }

    }
}
