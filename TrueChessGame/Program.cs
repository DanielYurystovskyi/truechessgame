using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueChessGame.GameEngine;

namespace TrueChessGame.ConsoleUI
{
    class Program
    {
        //TO DO drawns

        static void Main(string[] args)
        {
            ChessBoard gameboard = new ChessBoard();
            gameboard.SetDefaultChessBoard();
            DefaultInfo.SetDefaultValues();
            string notation = "";
            List<string> gamestory = new List<string>();
            string move = "";
            int NumberOfMoves=1;
            int rows = 20;
            int rowwidth = 20;
            //Draw.DrawChessBoard(gameboard);
            Console.Title="Dan's True Chess Game";
            do
            {
                DrawGameField(gameboard, gamestory, ref move, NumberOfMoves, ref rows, rowwidth);
                if (DefaultInfo.IsWhiteMove)                
                {
                    if (!FIDEnotation.CheckIfArePossibleMoves(gameboard, true))
                    {
                        if (!WhiteKing.IsSafe(gameboard))
                        {
                            DefaultInfo.BlackWin = true;
                        }
                        break;
                    }
                    WhiteMove(ref gameboard, ref notation, ref move);
                }
                else
                {
                    if (!FIDEnotation.CheckIfArePossibleMoves(gameboard, false))
                    {
                        if (!BlackKing.IsSafe(gameboard))
                        {
                            DefaultInfo.WhiteWin = true;
                        }
                        
                        break;
                    }
                    BlackMove(ref gameboard, ref notation, gamestory, ref move, ref NumberOfMoves);
                }
                
            }
            while (true);
            
            if (DefaultInfo.WhiteWin)
            {
                gamestory[gamestory.Count] += "+";
                move = " ";
                DrawGameField(gameboard, gamestory, ref move, NumberOfMoves, ref rows, rowwidth);
                Console.WriteLine("White win!");
            }
            if (DefaultInfo.BlackWin)
            {
                gamestory[gamestory.Count-1] += "+";
                move = " ";
                DrawGameField(gameboard, gamestory, ref move, NumberOfMoves, ref rows, rowwidth);
                Console.WriteLine("Black win!");
            }
            else
            {
                gamestory[gamestory.Count] += "=";
                move = " ";
                DrawGameField(gameboard, gamestory, ref move, NumberOfMoves, ref rows, rowwidth);
                Console.WriteLine("Draw");
            }

        }

        private static void DrawGameField(ChessBoard gameboard, List<string> gamestory, ref string move, int NumberOfMoves, ref int rows, int rowwidth)
        {
            Console.Clear();
            Draw.DrawChessBoard(gameboard);
           // Console.WriteLine();
           // Draw.DrawRevertedChessBoard(gameboard);

            Console.WriteLine();
            Console.SetCursorPosition(32, 0);
            Draw.DrawRevertedChessBoard(gameboard);
            Console.SetCursorPosition(0, 11);
            DrawSidesStrength(gameboard);
            DrawScoresheet(gamestory, ref move, NumberOfMoves, ref rows, rowwidth);
            Console.WriteLine();
        }

        private static void DrawSidesStrength(ChessBoard gameboard)
        {
            List<sbyte> whitepieces = new List<sbyte>();
            List<sbyte> blackpieces = new List<sbyte>();
            for (char tfile = 'a'; tfile <= 'h'; tfile++)
            {
                for (sbyte trank = 1; trank <= 8; trank++)
                {
                    if (gameboard[tfile, trank] > 0)
                    {
                        whitepieces.Add(gameboard[tfile, trank]);
                    }
                    else if (gameboard[tfile, trank] < 0)
                    {
                        blackpieces.Add(Math.Abs(gameboard[tfile, trank]));
                    }
                }
            }
            Console.SetCursorPosition(0, 10);
            Console.Write("White: ");
            //List<string> letters = new List<string>();
            whitepieces.Sort();
            whitepieces.Reverse();
            
            foreach (sbyte temp in whitepieces)
            {
                if (temp == 1)
                {
                    break;
                }
                Console.Write(FIDEnotation.GetLetter(temp) + " ");
            }
            Console.Write(whitepieces.Count - whitepieces.IndexOf(1));
            Console.Write('P');
            Console.WriteLine();
            //Console.SetCursorPosition(35, 11);
            Console.Write("Black: ");
            blackpieces.Sort();
            blackpieces.Reverse();
            foreach (sbyte temp in blackpieces)
            {
                if (temp == 1)
                {
                    break;
                }
                Console.Write(FIDEnotation.GetLetter(temp) + " ");
            }
            Console.Write(blackpieces.Count - blackpieces.IndexOf(1));
            Console.Write('P');
            Console.WriteLine();
        }

        private static void DrawScoresheet(List<string> gamestory, ref string move, int NumberOfMoves, ref int rows, int rowwidth)
        {
            Console.SetCursorPosition(0, 14);
            Console.Write("Scoresheet");
            for (int i = 0; i < gamestory.Count; i++)
            {
                if (( (i / rows) * rowwidth) + rowwidth >= Console.BufferWidth)
                {
                    rows = rows * 2;
                }

                Console.SetCursorPosition((i / rows) * rowwidth, i % rows + 15);
                Console.Write(gamestory[i]);
            }
            if (move == "")
            {
                move = NumberOfMoves.ToString() + ".";
            }
            Console.SetCursorPosition((gamestory.Count / rows) * rowwidth, gamestory.Count % rows + 15);
            Console.Write(move);
            Console.SetCursorPosition(0, 20);
        }

        private static void BlackMove(ref ChessBoard gameboard, ref string notation, List<string> gamestory, ref string move, ref int NumberOfMoves)
        {
            Console.SetCursorPosition(0, 12);
            Console.Write("Black, your move. Enter \"help\" if you don't know what to do");
            Console.WriteLine();
            notation = Console.ReadLine();
            if (notation == "help")
            {
                Draw.PrintHelp();
                Console.ReadLine();
                return;
            }
            DefaultInfo.BlackEnPassantEndangered = false;            
            try
            {
                gameboard = FIDEnotation.PerformBlackMove(gameboard, notation);
                NumberOfMoves++;
                move = move + " " + notation;
                if (!WhiteKing.IsSafe(gameboard))
                {
                    move += "+";
                }
                gamestory.Add(move);
                move = "";
                DefaultInfo.IsWhiteMove = !DefaultInfo.IsWhiteMove;
                
            }
            catch (ArgumentException)
            {
               // Console.WriteLine(e.Message);
               // BlackMove(ref gameboard, ref notation, gamestory, ref move, ref NumberOfMoves);
                return;
            }
        }

        private static void WhiteMove(ref ChessBoard gameboard, ref string notation, ref string move)
        {
            Console.SetCursorPosition(0, 12);
            Console.Write("White, your move. Enter \"help\" if you don't know what to do");
            Console.WriteLine();
            notation = Console.ReadLine();
            if (notation == "help")
            {
                Draw.PrintHelp();
                Console.ReadLine();
                return;
            }
            DefaultInfo.WhiteEnPassantEndangered = false;
            try
            {
                gameboard = FIDEnotation.PerformWhiteMove(gameboard, notation);
                move = move + " " + notation;
                if (!BlackKing.IsSafe(gameboard))
                {
                    move+= "+";
                }
                DefaultInfo.IsWhiteMove = !DefaultInfo.IsWhiteMove;
                
            }
            catch (ArgumentException)
            {
                //Console.WriteLine(e.Message);
                return;
               // WhiteMove(ref gameboard, ref notation, ref move);
            }
            
        }
    }
}
