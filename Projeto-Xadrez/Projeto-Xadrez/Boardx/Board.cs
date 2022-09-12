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
    }
}
