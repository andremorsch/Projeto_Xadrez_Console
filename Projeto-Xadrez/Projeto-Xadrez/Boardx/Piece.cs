namespace Projeto_Xadrez.Boardx
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int QuantityMoves { get; set; }
        public Board Board { get; set; }

        public Piece(Color color, Board board)
        {
            Position = null;
            Color = color;
            QuantityMoves = 0;
            Board = board;
        }

        public void IncreaseQuantityMovements()
        {
            QuantityMoves++;
        }

        public abstract bool[,] PossibleMovements();
    }
}
