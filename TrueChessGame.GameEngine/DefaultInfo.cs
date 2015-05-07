using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueChessGame.GameEngine
{
    /*			
		Review VV:
		    1) призначення класу не відповідає його назві, 
            оскільки поля класу відображають стан гри
            2) поля цього класу повинні бути нестатичними поля сутності "Game"
	*/
    public static class DefaultInfo
    {
        public static bool IsWhiteMove;

        public static bool WhiteWin;

        public static bool BlackWin;

        public static bool WhiteAsideRookIsUnMoved;

        public static bool WhiteHsideRookIsUnMoved;

        public static bool WhiteKingIsUnMoved;

        public static bool BlackAsideRookIsUnMoved;

        public static bool BlackHsideRookIsUnMoved;

        public static bool BlackKingIsUnMoved;

        public static int MovesWithoutPawnOrCapture;

        public static Square EnPassantPossibleCapture;

        public static bool WhiteEnPassantEndangered;

        public static bool BlackEnPassantEndangered;

        public static List<ChessBoard> GamePositions;

        public static void SetDefaultValues()
        {
            IsWhiteMove = true;
            WhiteWin = false;
            BlackWin = false;
            WhiteAsideRookIsUnMoved = true;
            WhiteHsideRookIsUnMoved = true;
            BlackAsideRookIsUnMoved = true;
            BlackHsideRookIsUnMoved = true;
            WhiteKingIsUnMoved = true;
            BlackKingIsUnMoved = true;
            MovesWithoutPawnOrCapture = 0;
            EnPassantPossibleCapture = new Square();
            WhiteEnPassantEndangered = false;
            BlackEnPassantEndangered = false;
            GamePositions = new List<ChessBoard>();
        }
    }
}
