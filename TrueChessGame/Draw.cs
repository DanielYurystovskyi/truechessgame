using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueChessGame.GameEngine
{
    public class Draw
    {

        public static void PrintHelp()
        {
            Console.Clear();
            Console.WriteLine("Here are some examples of the default chess notation");
            Console.WriteLine("Pawn moves from e2 to e4 - \"e4\"");
            Console.WriteLine("Pawn from d4 captures piece at e5 - \"de5\"");
            Console.WriteLine("Pawn from e7 moves to e8 and promotes to Queen - \"e8Q\"");
            Console.WriteLine("Same for capture - \"de8Q\"");
            Console.WriteLine("kNight moves to f3 - \"Nf3\"");
            Console.WriteLine("Two kNights may move to f3. So we also enter it's _file - \"Ngf3\"");
            Console.WriteLine("Or it's _rank - \"N1f3\"");
            Console.WriteLine("Or even both - \"Ng1f3\"");
            Console.WriteLine("Short (king's) castling is entered as \"0-0\"");
            Console.WriteLine("And long (queen's) as \"0-0-0\"");
            Console.WriteLine("Have fun \\m/");
            Console.WriteLine("Press ENTER now");
        }

        public static void DrawChessBoard(ChessBoard board)
        {
            // Code Review: Назва локальної змінної повинна починатися з малої літери.
            bool isWhite = true;
            //Console.WriteLine();
            Console.Write("   ");
            for (char tfile = 'a'; tfile <= 'h'; tfile++)
            {
                Console.Write(" " + tfile + " ");
            }
            //Console.WriteLine();
            for (int trank = 8; trank >= 1; trank--)
            {
                Console.WriteLine();
                //Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" "+trank+" ");
                for (char tfile = 'a'; tfile <= 'h'; tfile++)
                {
                    //Console.WriteLine(tfile + " ");
                    //Console.Write(trank + " ");
                    if (isWhite)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        //Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        //Console.ForegroundColor = ConsoleColor.Black;
                    }
                    //Console.WriteLine(tfile + " " + trank);
                    if (board[tfile, trank] < 0)
                    {
                        //Console.Write(board[tfile, trank] + " ");
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else if (board[tfile, trank] > 0)
                    {
                        //Console.Write("+" + board[tfile, trank] + " ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.Write(" " + FIDEnotation.GetLetter(board[tfile, trank]) + " ");
                    //else
                    //{
                    //    Console.Write("   ");
                    //}
                    isWhite = !isWhite;
                }
                isWhite = !isWhite;
                Console.ResetColor();
                Console.Write(" " + trank + " ");
            }
            Console.WriteLine();
            Console.Write("   ");
            for (char tfile = 'a'; tfile <= 'h'; tfile++)
            {
                Console.Write(" " + tfile + " ");
            }
            Console.WriteLine();
        }

        public static void DrawRevertedChessBoard(ChessBoard board)
        {
            // Code Review: Назва локальної змінної повинна починатися з малої літери.
            bool isWhite = true;
            //Console.WriteLine();
            Console.Write("   ");
            for (char tfile = 'h'; tfile >= 'a'; tfile--)
            {
                Console.Write(" " + tfile + " ");
            }
            //Console.WriteLine();
            for (int trank = 1; trank <= 8; trank++)
            {
                Console.SetCursorPosition(32, trank);
                //Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" " + trank + " ");
                for (char tfile = 'h'; tfile >= 'a'; tfile--)
                {
                    //Console.WriteLine(tfile + " ");
                    //Console.Write(trank + " ");
                    if (isWhite)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        //Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        //Console.ForegroundColor = ConsoleColor.Black;
                    }
                    //Console.WriteLine(tfile + " " + trank);
                    if (board[tfile, trank] < 0)
                    {
                        //Console.Write(board[tfile, trank] + " ");
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else if (board[tfile, trank] > 0)
                    {
                        //Console.Write("+" + board[tfile, trank] + " ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.Write(" " + FIDEnotation.GetLetter(board[tfile, trank]) + " ");
                    //else
                    //{
                    //    Console.Write("   ");
                    //}
                    isWhite = !isWhite;
                }
                isWhite = !isWhite;
                Console.ResetColor();
                Console.Write(" " + trank + " ");
            }
            Console.SetCursorPosition(32, 9);
            Console.Write("   ");
            for (char tfile = 'h'; tfile >= 'a'; tfile--)
            {
                Console.Write(" " + tfile + " ");
            }
            Console.SetCursorPosition(32, 0);

        }

    }
}
