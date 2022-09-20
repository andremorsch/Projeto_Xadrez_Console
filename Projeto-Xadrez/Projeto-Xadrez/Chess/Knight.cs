using Projeto_Xadrez.Boardx;

namespace Projeto_Xadrez.Chess
{
    class Knight : Piece
    {
        public Knight(Board board, Color color) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "N";
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

            position.SetValues(Position.Line - 1, Position.Column - 2);
            if (Board.PositionIsValid(position) && CanMove(position))
            {
                moves[position.Line, position.Column] = true;
            }

            position.SetValues(Position.Line - 2, Position.Column - 1);
            if (Board.PositionIsValid(position) && CanMove(position))
            {
                moves[position.Line, position.Column] = true;
            }

            position.SetValues(Position.Line - 2, Position.Column + 1);
            if (Board.PositionIsValid(position) && CanMove(position))
            {
                moves[position.Line, position.Column] = true;
            }

            position.SetValues(Position.Line - 1, Position.Column + 2);
            if (Board.PositionIsValid(position) && CanMove(position))
            {
                moves[position.Line, position.Column] = true;
            }

            position.SetValues(Position.Line + 1, Position.Column + 2);
            if (Board.PositionIsValid(position) && CanMove(position))
            {
                moves[position.Line, position.Column] = true;
            }

            position.SetValues(Position.Line + 2, Position.Column + 1);
            if (Board.PositionIsValid(position) && CanMove(position))
            {
                moves[position.Line, position.Column] = true;
            }

            position.SetValues(Position.Line + 2, Position.Column - 1);
            if (Board.PositionIsValid(position) && CanMove(position))
            {
                moves[position.Line, position.Column] = true;
            }

            position.SetValues(Position.Line + 1, Position.Column - 2);
            if (Board.PositionIsValid(position) && CanMove(position))
            {
                moves[position.Line, position.Column] = true;
            }

            return moves;
        }
    }
}
