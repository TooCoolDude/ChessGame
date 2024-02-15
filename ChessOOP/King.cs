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
                return player == Player.White ? GetWhiteImage : GetBlackImage;
            }
        }

        private static Image GetWhiteImage { get; } = GetImage(0, Player.White);

        private static Image GetBlackImage { get; } = GetImage(0, Player.Black);

        public override Figure Copy()
        {
            var c = new King(this.player);
            c.CurrentPosition = this.CurrentPosition;
            c.isCheckingMoves = this.isCheckingMoves;
            c.isFirstMove = this.isFirstMove;
            return c;
        }

        bool isFirstMove = true;
        bool isCheckingMoves = false;

        protected override List<(int, int)> GetPossibleMoves(ChessField field)
        {
            var y = CurrentPosition.Item1;
            var x = CurrentPosition.Item2;
            var list = new List<(int, int)>();
            int reverse = player == Player.White ? -1 : 1;
            var enemy = player == Player.White ? Player.Black : Player.White;
            
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (ValidateMove(y + i * reverse, x + j))
                    {
                        if ((field[y + i * reverse, x + j] == null || field[y + i * reverse, x + j].player == enemy)
                            )
                            list.Add((y + i * reverse, x + j));
                    }
                }
            }
            return list;
        }

        public override void MakeMove((int, int) nextPosition, ChessField field)
        {
            field[nextPosition.Item1, nextPosition.Item2] = field[CurrentPosition.Item1, CurrentPosition.Item2];
            field[CurrentPosition.Item1, CurrentPosition.Item2] = null;
            field[nextPosition.Item1, nextPosition.Item2].CurrentPosition = nextPosition;
            field.NextPlayer();
            isFirstMove = false;
        }
    }
}
