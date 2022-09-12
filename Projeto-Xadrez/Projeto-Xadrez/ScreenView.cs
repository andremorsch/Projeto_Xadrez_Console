using System;
using Projeto_Xadrez.Boardx;

namespace Projeto_Xadrez
{
    class ScreenView
    {
        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                for (int j = 0; j < board.Columns; j++)
                {
                    if (board.ViewPiece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                    Console.Write($"{board.ViewPiece(i, j)} ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
