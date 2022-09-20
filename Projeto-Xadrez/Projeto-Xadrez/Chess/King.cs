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

        private bool CanMove(Position position)
        {
            Piece piece = Board.ViewPiece(position);
            return piece == null || piece.Color != Color;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] moves = new bool[Board.Lines, Board.Columns];

            Position position = new Position(0, 0);

            // up
            position.SetValues(Position.Line - 1, Position.Column);
            if (Board.PositionIsValid(position) && CanMove(position))
            {
                moves[position.Line, position.Column] = true;
            }

            // ne
            position.SetValues(Position.Line - 1, Position.Column + 1);
            if (Board.PositionIsValid(position) && CanMove(position))
            {
                moves[position.Line, position.Column] = true;
            }

            // right
            position.SetValues(Position.Line, Position.Column + 1);
            if (Board.PositionIsValid(position) && CanMove(position))
            {
                moves[position.Line, position.Column] = true;
            }

            // se
            position.SetValues(Position.Line + 1, Position.Column + 1);
            if (Board.PositionIsValid(position) && CanMove(position))
            {
                moves[position.Line, position.Column] = true;
            }

            // down
            position.SetValues(Position.Line + 1, Position.Column);
            if (Board.PositionIsValid(position) && CanMove(position))
            {
                moves[position.Line, position.Column] = true;
            }

            // sw
            position.SetValues(Position.Line + 1, Position.Column - 1);
            if (Board.PositionIsValid(position) && CanMove(position))
            {
                moves[position.Line, position.Column] = true;
            }

            // left
            position.SetValues(Position.Line, Position.Column - 1);
            if (Board.PositionIsValid(position) && CanMove(position))
            {
                moves[position.Line, position.Column] = true;
            }

            // nw
            position.SetValues(Position.Line - 1, Position.Column - 1);
            if (Board.PositionIsValid(position) && CanMove(position))
            {
                moves[position.Line, position.Column] = true;
            }

            return moves;
        }
    }
}
