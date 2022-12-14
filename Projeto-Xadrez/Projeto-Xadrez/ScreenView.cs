using System;
using System.Collections.Generic;
using Projeto_Xadrez.Boardx;
using Projeto_Xadrez.Chess;

namespace Projeto_Xadrez
{
    class ScreenView
    {
        public static void PrintMatch(ChessMatch match)
        {
            ScreenView.PrintBoard(match.Board);

            Console.WriteLine();
            PrintCapturedPieces(match);
            Console.WriteLine();

            Console.WriteLine($"\nTurn: {match.Turn}");
            
            if (!match.Finished)
            {
                Console.WriteLine($"Waiting player {match.CurrentPlayer}");
                if (match.Check)
                {
                    Console.WriteLine("\nCHECK!");
                }
            }
            else
            {
                Console.WriteLine("CHECKMATE!");
                Console.WriteLine($"Winner: {match.CurrentPlayer}");
            }
        }

        public static void PrintCapturedPieces(ChessMatch match)
        {
            ConsoleColor originalForeground = Console.ForegroundColor;
            ConsoleColor changedForeground = ConsoleColor.DarkRed;

            Console.WriteLine("Captured pieces:");
            Console.Write("\nWhite pieces: ");
            PrintGroupPieces(match.CapturedPiecesByColor(Color.White));


            Console.Write("\nBlack Pieces: ");
            Console.ForegroundColor = changedForeground;
            PrintGroupPieces(match.CapturedPiecesByColor(Color.Black));
            Console.ForegroundColor = originalForeground;
        }

        public static void PrintGroupPieces(HashSet<Piece> pieces)
        {
            Console.Write("[ ");
            foreach (Piece piece in pieces)
            {
                Console.Write($"{piece} ");
            }
            Console.Write("]");
        }

        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < board.Columns; j++)
                {
                    PrintPiece(board.ViewPiece(i, j));
                }

                Console.WriteLine();
            }

            Console.WriteLine("  A B C D E F G H");
        }

        public static void PrintBoard(Board board, bool[,] possiblePositions)
        {
            ConsoleColor originalBakcground = Console.BackgroundColor;
            ConsoleColor changedBackground = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < board.Columns; j++)
                {
                    if (possiblePositions[i, j])
                    {
                        Console.BackgroundColor = changedBackground;
                    }
                    else
                    {
                        Console.BackgroundColor = originalBakcground;
                    }
                    PrintPiece(board.ViewPiece(i, j));
                    Console.BackgroundColor = originalBakcground;
                }

                Console.WriteLine();
            }

            Console.WriteLine("  A B C D E F G H");
        }

        public static ChessPosition ReadChessPosition()
        {
            string read = Console.ReadLine();
            char column = read[0];
            int line = int.Parse(read[1] + "");
            return new ChessPosition(column, line);
        }

        public static void PrintPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.Color == Color.White)
                {
                    Console.Write(piece);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }

                Console.Write(" ");
            }
        }
    }
}
