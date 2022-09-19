using System;
using Projeto_Xadrez.Boardx;
using Projeto_Xadrez.Boardx.Exceptions;
using Projeto_Xadrez.Chess;

namespace Projeto_Xadrez
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessMatch match = new ChessMatch();

                while (!match.Finished)
                {
                    try
                    {
                        Console.Clear();
                        ScreenView.PrintMatch(match);

                        Console.Write("\nOrigin position: ");
                        Position origin = ScreenView.ReadChessPosition().ToPosition();
                        match.ValidadeOriginPosition(origin);

                        bool[,] PossiblePositions = match.Board.ViewPiece(origin).PossibleMovements();

                        Console.Clear();
                        ScreenView.PrintBoard(match.Board, PossiblePositions);

                        Console.Write("\nTarget position: ");
                        Position target = ScreenView.ReadChessPosition().ToPosition();
                        match.ValidateTargetPosition(origin, target);

                        match.MakeTurn(origin, target);
                    }
                    catch (BoardException error)
                    {
                        Console.WriteLine(error.Message);
                        Console.ReadLine();
                    }
                }

                Console.Clear();
                ScreenView.PrintMatch(match);

            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
