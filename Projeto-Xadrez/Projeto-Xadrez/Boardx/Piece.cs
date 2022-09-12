using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto_Xadrez.Boardx
{
    class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int QuantityMoves { get; set; }
        public Board Board { get; set; }

        public Piece(Position position, Color color, Board board)
        {
            Position = position;
            Color = color;
            QuantityMoves = 0;
            Board = board;
        }
    }
}
