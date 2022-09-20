using Projeto_Xadrez.Boardx;

namespace Projeto_Xadrez.Chess
{
    class Queen : Piece
    {
        public Queen(Board board, Color color) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "Q";
        }

        private bool CanMove(Position position)
        {
            Piece piece = Board.ViewPiece(position);
            return piece == null || piece.Color != Color;
        }

