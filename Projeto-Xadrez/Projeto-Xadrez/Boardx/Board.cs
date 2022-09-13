using Projeto_Xadrez.Boardx.Exceptions;
using System;
namespace Projeto_Xadrez.Boardx
{
    class Board
    {
        public int Lines { get; set; }
        public int Columns { get; set; }
        private Piece[,] Pieces;

        public Board(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            Pieces = new Piece[lines, columns];
        }

        public Piece ViewPiece(int line, int column)
        {
            return Pieces[line, column];
        }

        public Piece ViewPiece(Position position)
        {
            return Pieces[position.Line, position.Column];
        }

        public bool ExistPiece(Position position)
        {
            ValidadePosition(position);
            return ViewPiece(position) != null;
        }

        public void PutPiece(Piece piece, Position position)
        {
            if (ExistPiece(position))
            {
                throw new BoardException("There is already a piece in that position");
            }
            Pieces[position.Line, position.Column] = piece;
            piece.Position = position;
        }

        public Piece RemovePiece(Position position)
        {
            if (ViewPiece(position) == null)
            {
                return null;
            }
            Piece auxPiece = ViewPiece(position);
            auxPiece.Position = null;
            Pieces[position.Line, position.Column] = null;
            return auxPiece;
        }

        public bool PositionIsValid(Position position)
        {
            if (position.Line < 0 || position.Column < 0 || position.Line >= Lines || position.Column >= Columns)
            {
                return false;
            }
            return true;
        }

        public void ValidadePosition(Position position)
        {
            if (!PositionIsValid(position))
            {
                throw new BoardException("Position is not valid");
            }
        }
    }
}
