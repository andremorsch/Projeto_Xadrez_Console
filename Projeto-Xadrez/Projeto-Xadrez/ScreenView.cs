﻿using System;
using Projeto_Xadrez.Boardx;
using Projeto_Xadrez.Chess;

namespace Projeto_Xadrez
{
    class ScreenView
    {
        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < board.Columns; j++)
                {
                    if (board.ViewPiece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        PrintPiece(board.ViewPiece(i, j));
                        Console.Write(" ");
                    }
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
        }
    }
}
