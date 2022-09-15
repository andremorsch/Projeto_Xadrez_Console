﻿using Projeto_Xadrez.Boardx;
using Projeto_Xadrez.Boardx.Exceptions;

namespace Projeto_Xadrez.Chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public  Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            PutPieces();
        }

        public void MakeMovement(Position origin, Position target)
        {
            Piece piece = Board.RemovePiece(origin);
            piece.IncreaseQuantityMovements();
            Piece capturedPiece = Board.RemovePiece(target);
            Board.PutPiece(piece, target);
        }

        public void MakeTurn(Position origin, Position target)
        {
            MakeMovement(origin, target);
            Turn++;
            ChangePlayer();
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

        public void PutNewPiece(char column, int line, Piece piece)
        {
            Board.PutPiece(piece, new ChessPosition(column, line).ToPosition());
            TotalPieces.Add(piece);
        }

        private void PutPieces()
        {
            Board.PutPiece(new Rook(Board, Color.White), new ChessPosition('c', 1).ToPosition());
            Board.PutPiece(new Rook(Board, Color.White), new ChessPosition('c', 2).ToPosition());
            Board.PutPiece(new Rook(Board, Color.White), new ChessPosition('d', 2).ToPosition());
            Board.PutPiece(new Rook(Board, Color.White), new ChessPosition('e', 2).ToPosition());
            Board.PutPiece(new Rook(Board, Color.White), new ChessPosition('e', 1).ToPosition());
            Board.PutPiece(new King(Board, Color.White), new ChessPosition('d', 1).ToPosition());

            Board.PutPiece(new Rook(Board, Color.Black), new ChessPosition('c', 7).ToPosition());
            Board.PutPiece(new Rook(Board, Color.Black), new ChessPosition('c', 8).ToPosition());
            Board.PutPiece(new Rook(Board, Color.Black), new ChessPosition('d', 7).ToPosition());
            Board.PutPiece(new Rook(Board, Color.Black), new ChessPosition('e', 7).ToPosition());
            Board.PutPiece(new Rook(Board, Color.Black), new ChessPosition('e', 8).ToPosition());
            Board.PutPiece(new King(Board, Color.Black), new ChessPosition('d', 8).ToPosition());
        }
    }
}
