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

        public override bool[,] PossibleMovements()
        {
            bool[,] moves = new bool[Board.Lines, Board.Columns];

            Position position = new Position(0, 0);

            // up
            position.SetValues(Position.Line - 1, Position.Column);
            while (Board.PositionIsValid(position) && CanMove(position))
            {
                moves[position.Line, position.Column] = true;
                if (Board.ViewPiece(position) != null && Board.ViewPiece(position).Color != Color)
                {
                    break;
                }
                position.SetValues(Position.Line - 1, Position.Column);
            }

            // down
            position.SetValues(Position.Line + 1, Position.Column);
            while (Board.PositionIsValid(position) && CanMove(position))
            {
                moves[position.Line, position.Column] = true;
                if (Board.ViewPiece(position) != null && Board.ViewPiece(position).Color != Color)
                {
                    break;
                }
                position.SetValues(Position.Line + 1, Position.Column);
            }

            // right
            position.SetValues(Position.Line, Position.Column + 1);
            while (Board.PositionIsValid(position) && CanMove(position))
            {
                moves[position.Line, position.Column] = true;
                if (Board.ViewPiece(position) != null && Board.ViewPiece(position).Color != Color)
                {
                    break;
                }
                position.SetValues(Position.Line, Position.Column + 1);
            }

            // left
            position.SetValues(Position.Line, Position.Column - 1);
            while (Board.PositionIsValid(position) && CanMove(position))
            {
                moves[position.Line, position.Column] = true;
                if (Board.ViewPiece(position) != null && Board.ViewPiece(position).Color != Color)
                {
                    break;
                }
                position.SetValues(Position.Line, Position.Column - 1);
            }

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
