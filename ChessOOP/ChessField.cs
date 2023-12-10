using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessOOP
{
    public class ChessField
    {
        Figure?[,] field = new Figure?[8, 8]
        {
            { new Rook(Player.Black), new Knight(Player.Black), new Bishop(Player.Black), new Queen(Player.Black), new King(Player.Black), new Bishop(Player.Black), new Knight(Player.Black), new Rook(Player.Black) },
            { new Pawn(Player.Black), new Pawn(Player.Black), new Pawn(Player.Black), new Pawn(Player.Black), new Pawn(Player.Black), new Pawn(Player.Black), new Pawn(Player.Black), new Pawn(Player.Black) },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null, null },
            { new Pawn(Player.White), new Pawn(Player.White), new Pawn(Player.White), new Pawn(Player.White), new Pawn(Player.White), new Pawn(Player.White), new Pawn(Player.White), new Pawn(Player.White) },
            { new Rook(Player.White), new Knight(Player.White), new Bishop(Player.White), new Queen(Player.White), new King(Player.White), new Bishop(Player.White), new Knight(Player.White), new Rook(Player.White) },
        };

        public bool[,] WhiteKingDangerousCells = new bool[8, 8];

        public bool[,] BlackKingDangerousCells = new bool[8, 8];

        private Player currentPlayer = Player.White;

        public Player CurrentPlayer
        {
            get { return currentPlayer; }
            private set { currentPlayer = value; }
        }

        public void NextPlayer()
        {
            currentPlayer = currentPlayer == Player.White ? Player.Black : Player.White;
        }

        public Figure? this[int y, int x]
        {
            get { return field[y, x]; }
            set 
            {
                if (x < 0 || x > 7)
                    throw new ArgumentOutOfRangeException("X was out of bounds array: " + x);
                if (y < 0 || y > 7)
                    throw new ArgumentOutOfRangeException("Y was out of bounds array: " + y);
                field[y, x] = value; 
            }
        }

        public Figure? this[char x, int y]
        {
            get { return field[8 - y, x - 65]; }
            set 
            { 
                if (x - 65 > 'H' || x - 65 < 'A')
                this[8 - y, x - 65] = value; 
            }
        }

    }
}
