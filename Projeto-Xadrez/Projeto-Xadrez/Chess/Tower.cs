using Projeto_Xadrez.Boardx;

namespace Projeto_Xadrez.Chess
{
    class Tower : Piece
    {


        public Tower(Board board, Color color) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "T";
        }
    }
}
