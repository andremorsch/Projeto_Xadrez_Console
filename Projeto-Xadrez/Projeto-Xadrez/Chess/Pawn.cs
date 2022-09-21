using Projeto_Xadrez.Boardx;

namespace Projeto_Xadrez.Chess
{
    class Pawn : Piece
    {
        private ChessMatch Match;

        public Pawn(Board board, Color color, ChessMatch match) : base(color, board)
        {
            Match = match;
        }

        public override string ToString()
        {
            return "P";
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

                //Special Move En Passant
                if (Position.Line == 3)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    Piece opo = Board.ViewPiece(left);
                    bool ooo = HasOpponent(left);
                    bool asdsa = Board.PositionIsValid(left);

                    if (Board.PositionIsValid(left)
                        && HasOpponent(left)
                        && Board.ViewPiece(left) == Match.VulnerableEnPassant)
                    {
                        moves[left.Line - 1, left.Column] = true;
                    }

                    Position right = new Position(Position.Line, Position.Column + 1);

                    if (Board.PositionIsValid(right)
                        && HasOpponent(right)
                        && Board.ViewPiece(right) == Match.VulnerableEnPassant)
                    {
                        moves[right.Line - 1, right.Column] = true;
                    }
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
