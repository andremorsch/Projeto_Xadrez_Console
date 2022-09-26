using Projeto_Xadrez.Boardx;

namespace Projeto_Xadrez.Chess
{
    class Bishop : Piece
    {
        public Bishop(Board board, Color color) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "B";
        }

        private bool CanMove(Position position)
        {
            Piece piece = Board.ViewPiece(position);
            return piece == null || piece.Color != Color;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] moves = new bool[Board.Lines, Board.Columns];

            Position position = new Position(0, 0);

            // NW
            position.SetValues(Position.Line - 1, Position.Column - 1);
            while (Board.PositionIsValid(position) && CanMove(position))
            {
                moves[position.Line, position.Column] = true;
                if (Board.ViewPiece(position) != null && Board.ViewPiece(position).Color != Color)
                {
                    break;
                }

                position.SetValues(position.Line - 1, position.Column - 1);
            }

            // NE
            position.SetValues(Position.Line - 1, Position.Column + 1);
            while (Board.PositionIsValid(position) && CanMove(position))
            {
                moves[position.Line, position.Column] = true;
                if (Board.ViewPiece(position) != null && Board.ViewPiece(position).Color != Color)
                {
                    break;
                }

                position.SetValues(position.Line - 1, position.Column + 1);
            }

            // SE
            position.SetValues(Position.Line + 1, Position.Column + 1);
            while (Board.PositionIsValid(position) && CanMove(position))
            {
                moves[position.Line, position.Column] = true;
                if (Board.ViewPiece(position) != null && Board.ViewPiece(position).Color != Color)
                {
                    break;
                }

                position.SetValues(position.Line + 1, position.Column + 1);
            }

            // SW
            position.SetValues(Position.Line + 1, Position.Column - 1);
            while (Board.PositionIsValid(position) && CanMove(position))
            {
                moves[position.Line, position.Column] = true;
                if (Board.ViewPiece(position) != null && Board.ViewPiece(position).Color != Color)
                {
                    break;
                }

                position.SetValues(position.Line + 1, position.Column - 1);
            }
              
            return moves;
        }
    }
}
