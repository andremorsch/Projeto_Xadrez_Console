using Projeto_Xadrez.Boardx;

namespace Projeto_Xadrez.Chess
{
    class King : Piece
    {
        private ChessMatch Match;

        public King(Board board, Color color, ChessMatch match) : base(color, board)
        {
            Match = match;
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

        private bool TestRookForCastle(Position position)
        {
            Piece rook = Board.ViewPiece(position);
            return rook != null
                && rook is Rook
                && rook.Color == Color
                && rook.QuantityMoves == 0;
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

            // Special Moves ->

            if (QuantityMoves == 0 && !Match.Check)
            {
                // CastleKingside
                Position positionRook1 = new Position(Position.Line, Position.Column + 3);

                if (TestRookForCastle(positionRook1))
                {
                    Position pos1 = new Position(Position.Line, Position.Column + 1);
                    Position pos2 = new Position(Position.Line, Position.Column + 2);

                    if (Board.ViewPiece(pos1) == null && Board.ViewPiece(pos2) == null)
                    {
                        moves[Position.Line, Position.Column + 2] = true;
                    }
                }

                // CastleQueenside
                Position positionRook2 = new Position(Position.Line, Position.Column - 4);

                if (TestRookForCastle(positionRook2))
                {
                    Position pos1 = new Position(Position.Line, Position.Column - 1);
                    Position pos2 = new Position(Position.Line, Position.Column - 2);
                    Position pos3 = new Position(Position.Line, Position.Column - 3);

                    if (Board.ViewPiece(pos1) == null
                        && Board.ViewPiece(pos2) == null
                        && Board.ViewPiece(pos3) == null)
                    {
                        moves[Position.Line, Position.Column - 2] = true;
                    }
                }
            }

            return moves;
        }
    }
}
