using Projeto_Xadrez.Boardx;

namespace Projeto_Xadrez.Chess
{
    class Pawn : Piece
    {
        public Pawn(Board board, Color color) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "P";
        }

        private bool CanMove(Position position)
        {
            Piece piece = Board.ViewPiece(position);
            return piece == null || piece.Color != Color;
        }

        public bool HasOpponent(Position position)
        {
            Piece piece = Board.ViewPiece(position);
            return piece != null && piece.Color != Color;
        }

        private bool IsEmptySquare(Position position)
        {
            return Board.ViewPiece(position) == null;
        }

