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

        public override bool[,] PossibleMovements()
        {
            bool[,] moves = new bool[Board.Lines, Board.Columns];

            Position position = new Position(0, 0);

            if (Color == Color.White)
            {
                position.SetValues(Position.Line - 1, Position.Column);
                if (Board.PositionIsValid(position) && IsEmptySquare(position))
                {
                    moves[position.Line, position.Column] = true;
                }

                position.SetValues(Position.Line - 2, Position.Column);
                if (Board.PositionIsValid(position) && IsEmptySquare(position) && QuantityMoves == 0)
                {
                    moves[position.Line, position.Column] = true;
                }

                position.SetValues(Position.Line - 1, Position.Column + 1);
                if (Board.PositionIsValid(position) && HasOpponent(position))
                {
                    moves[position.Line, position.Column] = true;
                }

                position.SetValues(Position.Line - 1, Position.Column - 1);
                if (Board.PositionIsValid(position) && HasOpponent(position))
                {
                    moves[position.Line, position.Column] = true;
                }
            }
            else
            {
                position.SetValues(Position.Line + 1, Position.Column);
                if (Board.PositionIsValid(position) && IsEmptySquare(position))
                {
                    moves[position.Line, position.Column] = true;
                }

                position.SetValues(Position.Line + 2, Position.Column);
                if (Board.PositionIsValid(position) && IsEmptySquare(position) && QuantityMoves == 0)
                {
                    moves[position.Line, position.Column] = true;
                }

                position.SetValues(Position.Line + 1, Position.Column + 1);
                if (Board.PositionIsValid(position) && HasOpponent(position))
                {
                    moves[position.Line, position.Column] = true;
                }

                position.SetValues(Position.Line + 1, Position.Column - 1);
                if (Board.PositionIsValid(position) && HasOpponent(position))
                {
                    moves[position.Line, position.Column] = true;
                }
            }

            return moves;
        }
    }
}
