using System.Collections.Generic;
using Projeto_Xadrez.Boardx;
using Projeto_Xadrez.Boardx.Exceptions;

namespace Projeto_Xadrez.Chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public  Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }
        private HashSet<Piece> TotalPieces;
        private HashSet<Piece> CapturedPieces;
        public bool Check { get; private set; }
        public Piece VulnerableEnPassant { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            TotalPieces = new HashSet<Piece>();
            CapturedPieces = new HashSet<Piece>();
            Check = false;
            VulnerableEnPassant = null;
            PutPieces();
        }

        public Piece MakeMovement(Position origin, Position target)
        {
            Piece piece = Board.RemovePiece(origin);
            piece.IncreaseQuantityMovements();
            Piece capturedPiece = Board.RemovePiece(target);
            Board.PutPiece(piece, target);
            if (capturedPiece != null)
            {
                CapturedPieces.Add(capturedPiece);
            }

            // Special Move CastleKingside
            if (piece is King
                && target.Column == origin.Column + 2)
            {
                Position originRook = new Position(origin.Line, origin.Column + 3);
                Position targetRook = new Position(origin.Line, origin.Column + 1);
                Piece rook = Board.RemovePiece(originRook);
                rook.IncreaseQuantityMovements();
                Board.PutPiece(rook, targetRook);
            }

            // Special Move CastleQueenside
            if (piece is King
                && target.Column == origin.Column - 2)
            {
                Position originRook = new Position(origin.Line, origin.Column - 4);
                Position targetRook = new Position(origin.Line, origin.Column - 1);
                Piece rook = Board.RemovePiece(originRook);
                rook.IncreaseQuantityMovements();
                Board.PutPiece(rook, targetRook);
            }

            // Special Move En Passant

            if (piece is Pawn)
            {
                if (origin.Column != target.Column
                && capturedPiece == null)
                {
                    Position capturedPawn;
                    if (piece.Color == Color.White)
                    {
                        capturedPawn = new Position(target.Line + 1, target.Column);
                    }
                    else
                    {
                        capturedPawn = new Position(target.Line - 1, target.Column);
                    }
                    capturedPiece = Board.RemovePiece(capturedPawn);
                    CapturedPieces.Add(capturedPiece);
                }
            }

            return capturedPiece;
        }

        public void UndoMovement(Position origin, Position target, Piece capturedPiece)
        {
            Piece piece = Board.RemovePiece(target);
            piece.DecrementQuantityMovements();

            if (capturedPiece != null)
            {
                Board.PutPiece(capturedPiece, target);
                CapturedPieces.Remove(capturedPiece);
            }

            // Special Move CastleKingside
            if (piece is King
                && target.Column == origin.Column + 2)
            {
                Position originRook = new Position(origin.Line, origin.Column + 3);
                Position targetRook = new Position(origin.Line, origin.Column + 1);
                Piece rook = Board.RemovePiece(targetRook);
                rook.DecrementQuantityMovements();
                Board.PutPiece(rook, originRook);
            }

            // Special Move CastleQueenside
            if (piece is King
                && target.Column == origin.Column - 2)
            {
                Position originRook = new Position(origin.Line, origin.Column - 4);
                Position targetRook = new Position(origin.Line, origin.Column - 1);
                Piece rook = Board.RemovePiece(targetRook);
                rook.DecrementQuantityMovements();
                Board.PutPiece(rook, originRook);
            }

            // Special Move En Passant

            if (piece is Pawn
                && origin.Column != target.Column
                && capturedPiece == VulnerableEnPassant)
            {
                Piece pawn = Board.RemovePiece(target);
                Position positionPawn;
                if (piece.Color == Color.White)
                {
                    positionPawn = new Position(3, target.Column);
                }
                else
                {
                    positionPawn = new Position(4, target.Column);
                }
            }

            Board.PutPiece(piece, origin);
        }

        public void MakeTurn(Position origin, Position target)
        {
            Piece capturedPiece = MakeMovement(origin, target);
            Piece piece = Board.ViewPiece(target);

            if (IsInCheck(CurrentPlayer))
            {
                UndoMovement(origin, target, capturedPiece);
                throw new BoardException("You can't put yourself in check");
            }

            // Special Move - Promotion
            if (piece is Pawn)
            {
                if ((piece.Color == Color.White && target.Line == 0)
                    || (piece.Color == Color.Black && target.Line == 7))
                {
                    piece = Board.RemovePiece(target);
                    TotalPieces.Remove(piece);
                    Piece queen = new Queen(Board, piece.Color);
                    Board.PutPiece(queen, target);
                    TotalPieces.Add(queen);
                }
            }


            if (IsInCheck(AdversaryColor(CurrentPlayer)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }

            if (IsCheckMate(AdversaryColor(CurrentPlayer)))
            {
                Finished = true;
            }
            else
            {
                Turn++;
                ChangePlayer();
            }

            // SpecialMove En Passant
            if (piece is Pawn
                && target.Line == origin.Line - 2
                || target.Line == origin.Line + 2)
            {
                VulnerableEnPassant = piece;
            }
            else
            {
                VulnerableEnPassant = null;
            }
        }

        public void ValidadeOriginPosition(Position position)
        {
            if (Board.ViewPiece(position) == null)
            {
                throw new BoardException("There is no part in the chosen source position");
            }
            
            if (CurrentPlayer != Board.ViewPiece(position).Color)
            {
                throw new BoardException("The chosen piece is not yours");
            }

            if (!Board.ViewPiece(position).HasPossibleMovements())
            {
                throw new BoardException("There are no possible movements for the chosen piece");
            }
        }

        public void ValidateTargetPosition(Position origin, Position target)
        {
            if (!Board.ViewPiece(origin).CanMoveTo(target))
            {
                throw new BoardException("Invalid target position");
            }
        }

        private void ChangePlayer()
        {
            if (CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            }
            else
            {
                CurrentPlayer = Color.White;
            }
        }

        public HashSet<Piece> CapturedPiecesByColor(Color color)
        {
            HashSet<Piece> response = new HashSet<Piece>();
            foreach (Piece piece in CapturedPieces)
            {
                if (piece.Color == color)
                {
                    response.Add(piece);
                }
            }
            return response;
        }

        public HashSet<Piece> InGamePiecesByColor(Color color)
        {
            HashSet<Piece> response = new HashSet<Piece>();
            foreach (Piece piece in TotalPieces)
            {
                if (piece.Color == color)
                {
                    response.Add(piece);
                }
            }
            response.ExceptWith(CapturedPiecesByColor(color));
            return response;
        }

        private Color AdversaryColor(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        private Piece KingByColor(Color color)
        {
            foreach (Piece piece in InGamePiecesByColor(color))
            {
                if (piece is King)
                {
                    return piece;
                }
            }
            return null;
        }

        public bool IsInCheck(Color color)
        {
            Piece king = KingByColor(color);

            if (king == null)
            {
                throw new BoardException($"Don't have a {color} King ");
            }

            foreach (Piece piece in InGamePiecesByColor(AdversaryColor(color)))
            {
                bool[,] aux = piece.PossibleMovements();
                if (aux[king.Position.Line, king.Position.Column])
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsCheckMate(Color color)
        {
            if (!IsInCheck(color))
            {
                return false;
            }

            foreach (Piece piece in InGamePiecesByColor(color))
            {
                bool[,] moves = piece.PossibleMovements();
                for (int i = 0; i < Board.Lines; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (moves[i, j])
                        {
                            Position origin = piece.Position;
                            Position target = new Position(i, j);
                            Piece capturedPiece = MakeMovement(origin, target);
                            bool testCheck = IsInCheck(color);
                            UndoMovement(origin, target, capturedPiece);

                            if (!testCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void PutNewPiece(char column, int line, Piece piece)
        {
            Board.PutPiece(piece, new ChessPosition(column, line).ToPosition());
            TotalPieces.Add(piece);
        }

        private void PutPieces()
        {
            PutNewPiece('a', 1, new Rook(Board, Color.White));
            PutNewPiece('b', 1, new Knight(Board, Color.White));
            PutNewPiece('c', 1, new Bishop(Board, Color.White));
            PutNewPiece('d', 1, new Queen(Board, Color.White));
            PutNewPiece('e', 1, new King(Board, Color.White, this));
            PutNewPiece('f', 1, new Bishop(Board, Color.White));
            PutNewPiece('g', 1, new Knight(Board, Color.White));
            PutNewPiece('h', 1, new Rook(Board, Color.White));
            PutNewPiece('a', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('b', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('c', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('d', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('e', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('f', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('g', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('h', 2, new Pawn(Board, Color.White, this));

            PutNewPiece('a', 8, new Rook(Board, Color.Black));
            PutNewPiece('b', 8, new Knight(Board, Color.Black));
            PutNewPiece('c', 8, new Bishop(Board, Color.Black));
            PutNewPiece('d', 8, new Queen(Board, Color.Black));
            PutNewPiece('e', 8, new King(Board, Color.Black, this));
            PutNewPiece('f', 8, new Bishop(Board, Color.Black));
            PutNewPiece('g', 8, new Knight(Board, Color.Black));
            PutNewPiece('h', 8, new Rook(Board, Color.Black));
            PutNewPiece('a', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('b', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('c', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('d', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('e', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('f', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('g', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('h', 7, new Pawn(Board, Color.Black, this));
        }
    }
}
