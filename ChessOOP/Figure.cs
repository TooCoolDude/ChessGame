using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

        private static Bitmap Images = new (Directory.GetCurrentDirectory() + "\\images\\chess.png");

        public Figure(Player player)
        { 
            this.player = player;
        }

        protected static Image GetImage(int figure, Player player)
        {
            var i = Images.Clone(new Rectangle(333 * figure, ((int)player - 1) * 333, 333, 333),
                        Images.PixelFormat);
            return new Bitmap(i, new Size(200, 200));
        }

        public virtual Image Img { get { return Images; } }

        protected virtual List<(int, int)> GetPossibleMoves(ChessField field)
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
            field[nextPosition.Item1, nextPosition.Item2] = field[CurrentPosition.Item1, CurrentPosition.Item2];
            field[CurrentPosition.Item1, CurrentPosition.Item2] = null;
            field[nextPosition.Item1, nextPosition.Item2].CurrentPosition = nextPosition;
            field.NextPlayer();
        }

        public List<(int,int)> GetMoves(ChessField chessField)
        {
            var moves = this.GetPossibleMoves(chessField);
            List<(int, int)> legalMoves = new();
            
            foreach (var move in moves)
            {
                var chessCopy = chessField.Copy();
                chessCopy[this.CurrentPosition.Item1, this.CurrentPosition.Item2].MakeMove(move, chessCopy);

                bool legalMove = true;

                for (var i = 0; i < 8; i++)
                {
                    for (var j = 0; j < 8; j++)
                    {
                        if (chessCopy[i, j] is { } figure)
                        {
                            var figureMoves = figure.GetPossibleMoves(chessCopy);

                            bool kingInDanger = figureMoves.Any(to =>
                            {
                                return chessCopy[to.Item1, to.Item2] is King && chessCopy[to.Item1, to.Item2].player != chessCopy.CurrentPlayer;
                            });

                            if (kingInDanger)
                            {
                                legalMove = false;
                                break;
                            }
                        }
                    }
                }
                if (legalMove)
                {
                    legalMoves.Add(move);
                }
            }
            return legalMoves;
        }

        public virtual Figure Copy() 
        {
            return null;
        }

        public virtual bool IsGameOver(ChessField chessField)
        {
            var chessCopy = chessField.Copy();
            int legalMovesCount = 0;
            bool legalMove = true;

            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    if (chessCopy[i, j] is { } figure && figure.player == chessField.CurrentPlayer)
                    {
                        var figureMoves = figure.GetMoves(chessCopy);

                        //bool kingInDanger = figureMoves.Any(to =>
                        //{
                        //    return chessCopy[to.Item1, to.Item2] is King && chessCopy[to.Item1, to.Item2].player == chessCopy.CurrentPlayer;
                        //});

                        if (figureMoves.Count == 0)
                        {
                            continue;
                        }
                        else legalMovesCount++;
                    }
                }
            }
            if (legalMovesCount == 0)
            {
                return true;
            }
            return false;
        }
    }
}
