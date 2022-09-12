using Projeto_Xadrez.Boardx;

namespace Projeto_Xadrez.Chess
{
    class King : Piece
    {


        public King(Board board, Color color) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "K";
        }
    }
}
