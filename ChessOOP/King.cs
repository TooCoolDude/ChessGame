using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessOOP
{
    public class King : Figure //Король
    {
        public King(Player player) : base(player) { }

        public override Image Img
        {
            get
            {
                return base.GetImage(0);
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

            var dangerousMoves = GetDangerousMoves(field);
            for (int i = -1; i < 1; i++)
            {
                for (int j = -1; j < 1; j++)
                {
                    if (ValidateMove(y + i * reverse, x + j))
                    {
                        if (field[y + i * reverse, x + j] == null || field[y + i * reverse, x + j].player == enemy
                            && !dangerousMoves.Contains((y + i * reverse, x + j)))
                            list.Add((y + i * reverse, x + j));
                    }
                }
            }
            return list;
        }

        private List<(int,int)> GetDangerousMoves(ChessField field)
        {
            var list = new List<(int, int)>();
            var enemy = player == Player.White ? Player.Black : Player.White;
            
            for (int i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    if (field[i, j] != null && field[i, j].player == enemy && field[i, j] is not King)
                    {
                        list.AddRange(field[i, j].GetPossibleMoves(field));
                    }
                }
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
