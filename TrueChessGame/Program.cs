using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TrueChessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            string mode;
            do
            {
                Console.WriteLine("Enter \"m\" if you want to play MultiPlayer"); 
                Console.WriteLine("or \"s\" if you want to play SinglePlayer");
                mode=Console.ReadLine();
            }
            while(mode!="s" && mode!="m");
            if (mode == "m")
            {
                ChessBoard gameboard = new ChessBoard();
                gameboard.SetDefaultChessBoard();
                DefaultInfo.SetDefaultValues();
                gameboard.DebugConsoleSimpleDraw();
                
            }



            #region oldtestingstuff
            // ChessBoard myboard = new ChessBoard();
           // //myboard['a', 1] = 4;
           // myboard.SetDefaultChessBoard();
           // //Console.WriteLine(myboard['a',1]);
           // // Console.WriteLine('c' - 96);
           // //myboard['b', 7] = 1;
           // myboard.DebugConsoleSimpleDraw();
           // Console.WriteLine();
           // Console.WriteLine(WhiteKing.IsSafe(myboard));

           // //Piece temp = new WhiteKing();
           // Piece temp = new WhitePawn();
           //// myboard['e', 1] = 0;
           //// myboard['e', 6] = 6;
           // //myboard['b', 1] = 0;
           // //myboard['c', 1] = 0;
           // //myboard['d', 1] = 0;
           // //myboard['f', 1] = 0;
           // //myboard['g', 1] = 0;
           // myboard.DebugConsoleSimpleDraw();
           // Console.WriteLine(WhiteKing.IsSafe(myboard));
           // myboard['f', 7] = 0;
           // myboard['f', 5] = -1;
           // DefaultInfo.BlackEnPassantEndangered = true;
           // DefaultInfo.EnPassantPossibleCapture = new Square('f', 6);
           // var mycollection = temp.GetPossiblePositions(myboard, 'e', 5);
           // foreach (var piecetemp in mycollection)
           // {
            //    // Console.WriteLine(pawntemp.file + " " + pawntemp.rank);
            //    piecetemp.DebugConsoleSimpleDraw();
            //    Console.WriteLine();
            //}
            //char file = 'a';
            #endregion
        }
    }
}
