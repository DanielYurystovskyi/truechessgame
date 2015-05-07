using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueChessGame.GameEngine
{
    /*			
		Review VV:
		    для читабельності такі записи краще робити у багато рядків	
	*/
    public enum DefaultPieces : sbyte
    {
        BlackKing = -6,
        BlackQueen = -5,
        BlackRook = -4,
        BlackBishop = -3,
        BlackkNight = -2,
        BlackPawn = -1,
        Empty = 0,
        WhitePawn = 1,
        WhitekNight = 2,
        WhiteBishop = 3,
        WhiteRook = 4,
        WhiteQueen = 5,
        WhiteKing = 6
    };

    /*			
		Review VV:
		    1) не зрозуміле призначення цього класу
            на мою думку, його функції повинні бути нестатичними функціями сутності "Game"	
            2) всі функції класів-фігур повинні бути нестатичними

	*/
    public class Piece
    {
        public static ChessBoard PerformMove(ChessBoard board, Square oldposition, Square newposition)
        {
            sbyte piecetype = board[oldposition.file, oldposition.rank];
            /*			
			    Review VV:
			        для чого необхідно робити копію ігрового поля?
		    */
            ChessBoard tempboard = board.ShallowCopy();
            tempboard[oldposition.file, oldposition.rank] = 0;
            tempboard[newposition.file, newposition.rank] = piecetype;
            // tempboard.DebugConsoleSimpleDraw(); Console.WriteLine();
            return tempboard;
        }

        public static List<ChessBoard> AddNewPosition(List<ChessBoard> ResultedPositionsList, ChessBoard position, bool IsWhite)
        {
            if (IsWhite && WhiteKing.IsSafe(position))
            {
                ResultedPositionsList.Add(position);
            }
            else if (!IsWhite && BlackKing.IsSafe(position))
            {
                ResultedPositionsList.Add(position);
            }
            return ResultedPositionsList;
        }

        public static Square[] GetSimplekNightMoveDestinations(Square current)
        {
            Square[] moves = new Square[8];
            moves[0] = new Square((char)(current.file + 1), current.rank + 2);
            moves[1] = new Square((char)(current.file + 2), current.rank + 1);
            moves[2] = new Square((char)(current.file + 2), current.rank - 1);
            moves[3] = new Square((char)(current.file + 1), current.rank - 2);
            moves[4] = new Square((char)(current.file - 1), current.rank + 2);
            moves[5] = new Square((char)(current.file - 2), current.rank + 1);
            moves[6] = new Square((char)(current.file - 2), current.rank - 1);
            moves[7] = new Square((char)(current.file - 1), current.rank - 2);
            return moves;
        }

        public static Square[] GetSimpleKingMoveDestinations(Square current)
        {
            char file = current.file; int rank = current.rank;
            Square[] moves = new Square[8];
            moves[0] = new Square(file, rank + 1);
            moves[1] = new Square(file, rank - 1);
            moves[2] = new Square((char)(file + 1), rank + 1);
            moves[3] = new Square((char)(file + 1), rank);
            moves[4] = new Square((char)(file + 1), rank - 1);
            moves[5] = new Square((char)(file - 1), rank + 1);
            moves[6] = new Square((char)(file - 1), rank);
            moves[7] = new Square((char)(file - 1), rank - 1);
            return moves;
        }

        public static Square GetPosition(ChessBoard board, sbyte piece)
        {
            Square result = new Square();
            for (char tfile = 'a'; tfile <= 'h'; tfile++)
            {
                for (int trank = 1; trank <= 8; trank++)
                {
                    if (board[tfile, trank] == piece)
                    {
                        result.file = tfile;
                        result.rank = (sbyte)trank;
                        break;
                    }
                }
            }
            return result;
        }
    }

    #region White Pieces
    /*			
		Review VV:
		    не бачу сенсу в такому наслідуванні, оскільки всі функції класу	Piece статичні
	*/
    public class WhitePiece : Piece
    {
        public static void GetDiagonalDestinations(ChessBoard board, ref List<Square> moves, Square current, int multfile, int multrank)
        {
            char file = current.file;
            int rank = current.rank;
            for (int i = 1; i < 8; i++)
            {
                Square tempsquare=new Square((char)(file+multfile*i), rank+multrank*i);
                if (!tempsquare.IsOK())
                {
                    break;
                }
                else if (board[tempsquare] > 0)
                {
                    break;
                }
                else if (board[tempsquare] < 0)
                {
                    moves.Add(tempsquare);
                    break;
                }
                else
                {
                    moves.Add(tempsquare);
                }
            }
        }

        public static void GetVerticalUpDestinations(ChessBoard board, List<Square> moves, Square current)
        {
            for (int i = current.rank + 1; i <= 8; i++)
            {
                if (board[current.file, i] > 0)
                {
                    break;
                }
                else
                {
                    if (board[current.file, i] < 0)
                    {
                        moves.Add(new Square(current.file, i));
                        break;
                    }
                    else
                    {
                        moves.Add(new Square(current.file, i));
                    }
                }
            }
            
        }

        public static void GetVerticalDownDestinations(ChessBoard board, List<Square> moves, Square current)
        {
            for (int i = current.rank - 1; i >= 1; i--)
            {
                if (board[current.file, i] > 0)
                {
                    break;
                }
                else
                {
                    if (board[current.file, i] < 0)
                    {
                        moves.Add(new Square(current.file, i));
                        break;
                    }
                    else
                    {
                        moves.Add(new Square(current.file, i));
                    }
                }
            }
        }

        public static void GetHorizontalLeftDestinations(ChessBoard board, List<Square> moves, Square current)
        {
            for (char tchar = (char)(current.file - 1); tchar >= 'a'; tchar--)
            {
                if (board[tchar, current.rank] > 0)
                {
                    break;
                }
                else
                {
                    if (board[tchar, current.rank] < 0)
                    {
                        moves.Add(new Square(tchar, current.rank));
                        break;
                    }
                    else
                    {
                        moves.Add(new Square(tchar, current.rank));
                    }
                }
            }
        }

        public static void GetHorizontalRightDestinations(ChessBoard board, List<Square> moves, Square current)
        {
            for (char tchar = (char)(current.file + 1); tchar <= 'h'; tchar++)
            {
                if (board[tchar, current.rank] > 0)
                {
                    break;
                }
                else
                {
                    if (board[tchar, current.rank] < 0)
                    {
                        moves.Add(new Square(tchar, current.rank));
                        break;
                    }
                    else
                    {
                        moves.Add(new Square(tchar, current.rank));
                    }
                }
            }
        }

        public static List<Square> GetPossibleBlackAttackersToSquare(ChessBoard board, Square goalsquare)
        {
            List<Square> result = new List<Square>();
            char file = goalsquare.file; int rank = goalsquare.rank;
            //check for Pawn
            if (file >= 'a' && file <= 'g' && rank<=7 && board[(char)(file + 1), rank + 1] == (sbyte)DefaultPieces.BlackPawn)
            {
                result.Add(new Square((char)(file + 1), rank + 1));
            }
            if (file >= 'b' && file <= 'h' && rank <=7 && board[(char)(file - 1), rank + 1] == (sbyte)DefaultPieces.BlackPawn)
            {
                result.Add(new Square((char)(file - 1), rank + 1));
            }
            Square[] moves_array = Piece.GetSimplekNightMoveDestinations(goalsquare);
            foreach (Square move in moves_array)
            {
                if (move.IsOK() && board[move] == (sbyte)DefaultPieces.BlackkNight)
                {
                    result.Add(move);
                }
            }
            GetVerticalUpBlackAttackers(board, result, file, rank);
            GetVerticalDownBlackAttackers(board, result, file, rank);
            GetHorizontalRightBlackAttacker(board, result, file, rank);
            GetHorizontalLeftBlackAttackers(board, result, file, rank);
            GetDiagonalBlackAttackers(board, result, goalsquare, 1, 1);
            GetDiagonalBlackAttackers(board, result, goalsquare, 1, -1);
            GetDiagonalBlackAttackers(board, result, goalsquare, -1, 1);
            GetDiagonalBlackAttackers(board, result, goalsquare, -1, -1);
            Square[] moves = Piece.GetSimpleKingMoveDestinations(goalsquare);
            foreach (Square move in moves)
            {
                if (move.IsOK() && board[move] == (sbyte)DefaultPieces.BlackKing)
                {
                    result.Add(move); break;
                }
            }
            return result;
        }

        private static bool CheckSquareForBlackRookOrQueen(ChessBoard board, Square CheckedSquare, List<Square> result)
        {
            bool ToBreakNow = false;
            if (board[CheckedSquare]>0)
            {
                ToBreakNow = true;
            }
            else if (board[CheckedSquare]<0 && board[CheckedSquare]!=(sbyte)DefaultPieces.BlackRook && board[CheckedSquare]!=(sbyte)DefaultPieces.BlackQueen)
            {
                ToBreakNow = true;
            }
            else if (board[CheckedSquare]==(sbyte)DefaultPieces.BlackRook || board[CheckedSquare]==(sbyte)DefaultPieces.BlackQueen)
            {
                result.Add(CheckedSquare);
                ToBreakNow=true;
            }
            return ToBreakNow;
        }

        private static void GetHorizontalLeftBlackAttackers(ChessBoard board, List<Square> result, char file, int rank)
        {
            for (char tchar = (char)(file - 1); tchar >= 'a'; tchar--)
            {
                bool EndOfCycle = CheckSquareForBlackRookOrQueen(board, new Square(tchar, rank), result);
                if (EndOfCycle)
                {
                    break;
                }
            }
        }

        private static void GetHorizontalRightBlackAttacker(ChessBoard board, List<Square> result, char file, int rank)
        {
            for (char tchar = (char)(file + 1); tchar <= 'h'; tchar++)
            {
                bool EndOfCycle = CheckSquareForBlackRookOrQueen(board, new Square(tchar, rank), result);
                if (EndOfCycle)
                {
                    break;
                }
            }
        }

        private static void GetVerticalDownBlackAttackers(ChessBoard board, List<Square> result, char file, int rank)
        {
            for (int i = rank - 1; i >= 1; i--)
            {
                bool EndOfCycle = CheckSquareForBlackRookOrQueen(board, new Square(file, i), result);
                if (EndOfCycle)
                {
                    break;
                }
            }
        }

        private static void GetVerticalUpBlackAttackers(ChessBoard board, List<Square> result, char file, int rank)
        {
            for (int i = rank + 1; i <= 8; i++)
            {
                bool EndOfCycle = CheckSquareForBlackRookOrQueen(board, new Square(file, i), result);
                if (EndOfCycle)
                {
                    break;
                }
            }
        }

        private static void GetDiagonalBlackAttackers(ChessBoard board, List<Square> result, Square current, int multfile, int multrank)
        {
            char file = current.file;
            int rank = current.rank;
            for (int i = 1; i < 8; i++)
            {
                Square tempsquare = new Square((char)(file + multfile * i), rank + multrank * i);
                if (!tempsquare.IsOK())
                {
                    break;
                }
                else if (board[tempsquare] > 0)
                {
                    break;
                }
                else if (board[tempsquare] < 0 && board[tempsquare]!=(sbyte)DefaultPieces.BlackBishop && board[tempsquare] != (sbyte)DefaultPieces.BlackQueen)
                {
                    break;
                }
                else if (board[tempsquare]== (sbyte)DefaultPieces.BlackBishop || board[tempsquare] == (sbyte)DefaultPieces.BlackQueen)
                {
                    result.Add(new Square((char)(file + multfile * i), rank + multrank * i));
                    break;
                }
            }
        }
    }

    public class WhitePawn
    {

        public static List<ChessBoard> GetPossiblePositions(ChessBoard board, char file, sbyte rank)
        {
            List<ChessBoard> result = new List<ChessBoard>();
            ChessBoard tempboard = board.ShallowCopy();
            Square currentposition=new Square(file, rank);
            if (DefaultInfo.BlackEnPassantEndangered)
            {
                GetEnPassantPositions(ref result, board, currentposition);
            }
            if (rank == 2 && board[file, rank + 1] == 0 && board[file, rank + 2] == 0)
            {
                tempboard = Piece.PerformMove(board, currentposition, new Square(file, rank + 2));
                result = Piece.AddNewPosition(result, tempboard, true);
            }
            if (board[file, rank + 1] == 0)
            {
                tempboard = Piece.PerformMove(board, currentposition, new Square(file, rank + 1));
                result = Piece.AddNewPosition(result, tempboard, true);
            }
            if (file >= 'a' && file <= 'g' && board[(char)(file + 1), rank + 1] < 0)
            {
                tempboard = Piece.PerformMove(board, currentposition, new Square((char)(file + 1), rank + 1));
                result = Piece.AddNewPosition(result, tempboard, true);
            }
            if (file >= 'b' && file <= 'h' && board[(char)(file - 1), rank + 1] < 0)
            {
                tempboard = Piece.PerformMove(board, currentposition, new Square((char)(file - 1), rank + 1));
                result = Piece.AddNewPosition(result, tempboard, true);
            }
            if (rank <= 6 || result.Count == 0)
            {
                return result;
            }
            else
            {
                return GetPawnPromotionPositions(file, result, tempboard);
            }
        }

        private static void GetEnPassantPositions(ref List<ChessBoard> result, ChessBoard board, Square currentposition)
        {
            ChessBoard tempboard = board.ShallowCopy();
            bool CheckEnPassant = DefaultInfo.EnPassantPossibleCapture.rank == currentposition.rank + 1 && (DefaultInfo.EnPassantPossibleCapture.file == (char)(currentposition.file + 1) || DefaultInfo.EnPassantPossibleCapture.file == (char)(currentposition.file - 1));
            if (CheckEnPassant)
            {
                tempboard = Piece.PerformMove(board, currentposition, DefaultInfo.EnPassantPossibleCapture);
                tempboard[DefaultInfo.EnPassantPossibleCapture.file, DefaultInfo.EnPassantPossibleCapture.rank - 1] = 0;
                result = Piece.AddNewPosition(result, tempboard, true);
            }
        }

        private static List<ChessBoard> GetPawnPromotionPositions(char file, List<ChessBoard> result, ChessBoard tempboard)
        {
            List<ChessBoard> resultpromotion = new List<ChessBoard>();
            foreach (ChessBoard promotiontempboard in result)
            {
                tempboard = promotiontempboard.ShallowCopy();
                char promotionfile = file;
                for (char tempfile = 'a'; tempfile <= 'h'; tempfile++)
                {
                    if (tempboard[tempfile, 8] == (sbyte)DefaultPieces.WhitePawn)
                    {
                        promotionfile = tempfile;
                        break;
                    }
                }
                //now Promotions!
                tempboard[promotionfile, 8] = (sbyte)DefaultPieces.WhitekNight;
                resultpromotion.Add(tempboard.ShallowCopy());
                tempboard[promotionfile, 8] = (sbyte)DefaultPieces.WhiteBishop;
                resultpromotion.Add(tempboard.ShallowCopy());
                tempboard[promotionfile, 8] = (sbyte)DefaultPieces.WhiteRook;
                resultpromotion.Add(tempboard.ShallowCopy());
                tempboard[promotionfile, 8] = (sbyte)DefaultPieces.WhiteQueen;
                resultpromotion.Add(tempboard.ShallowCopy());
            }
            return resultpromotion;
        }
    }

    public class WhitekNight
    {

        public static List<ChessBoard> GetPossiblePositions(ChessBoard board, char file, sbyte rank)
        {
            List<ChessBoard> result = new List<ChessBoard>();
            Square current = new Square(file, rank);
            Square[] moves = Piece.GetSimplekNightMoveDestinations(current);
            ChessBoard tempboard;
            foreach (Square move in moves)
            {
                if (move.IsOK())
                {
                    if (board[move] <= 0)
                    {
                        tempboard = Piece.PerformMove(board, current, move);
                        result = Piece.AddNewPosition(result, tempboard, true);
                    }
                }
            }
            return result;
        }

    }

    public class WhiteBishop
    {

        public static List<ChessBoard> GetPossiblePositions(ChessBoard board, char file, sbyte rank)
        {
            List<ChessBoard> result = new List<ChessBoard>();
            ChessBoard tempboard;
            List<Square> moves = new List<Square>();
            Square current = new Square(file, rank);
            WhitePiece.GetDiagonalDestinations(board, ref moves, current, 1, 1);
            WhitePiece.GetDiagonalDestinations(board, ref moves, current, 1, -1);
            WhitePiece.GetDiagonalDestinations(board, ref moves, current, -1, 1);
            WhitePiece.GetDiagonalDestinations(board, ref moves, current, -1, -1);
            foreach (Square move in moves)
            {
                tempboard = Piece.PerformMove(board, current, move);
                result = Piece.AddNewPosition(result, tempboard, true);
            }
            return result;
        }

    }

    public class WhiteRook
    {

        public static List<ChessBoard> GetPossiblePositions(ChessBoard board, char file, sbyte rank)
        {
            List<ChessBoard> result = new List<ChessBoard>();
            ChessBoard tempboard;
            List<Square> moves = new List<Square>();
            Square current=new Square(file, rank);
            WhitePiece.GetVerticalUpDestinations(board, moves, current);
            WhitePiece.GetVerticalDownDestinations(board, moves, current);
            WhitePiece.GetHorizontalLeftDestinations(board, moves, current);
            WhitePiece.GetHorizontalRightDestinations(board, moves, current);
            Console.WriteLine(moves.Count);
            foreach (Square move in moves)
            {
                tempboard = Piece.PerformMove(board, current, move);
                result = Piece.AddNewPosition(result, tempboard, true);
            }
            Console.WriteLine(result.Count);
            return result;
        }
    }

    public class WhiteQueen
    {

        public static List<ChessBoard> GetPossiblePositions(ChessBoard board, char file, sbyte rank)
        {
            List<ChessBoard> result = new List<ChessBoard>();
            ChessBoard tempboard;
            List<Square> moves = new List<Square>();
            Square current = new Square(file, rank);
            WhitePiece.GetDiagonalDestinations(board, ref moves, current, 1, 1);
            WhitePiece.GetDiagonalDestinations(board, ref moves, current, 1, -1);
            WhitePiece.GetDiagonalDestinations(board, ref moves, current, -1, 1);
            WhitePiece.GetDiagonalDestinations(board, ref moves, current, -1, -1);
            WhitePiece.GetVerticalUpDestinations(board, moves, current);
            WhitePiece.GetVerticalDownDestinations(board, moves, current);
            WhitePiece.GetHorizontalLeftDestinations(board, moves, current);
            WhitePiece.GetHorizontalRightDestinations(board, moves, current);
            foreach (Square move in moves)
            {
                tempboard = Piece.PerformMove(board, current, move);
                result = Piece.AddNewPosition(result, tempboard, true);
            }
            return result;
        }
    }

    public class WhiteKing
    {

        public static bool IsSafe(ChessBoard board)
        {
            Square current = Piece.GetPosition(board, (sbyte)DefaultPieces.WhiteKing);
            char file = current.file; int rank = current.rank;
            List<Square> result = WhitePiece.GetPossibleBlackAttackersToSquare(board, current);
            return (result.Count == 0);
        }

        public static List<ChessBoard> GetPossiblePositions(ChessBoard board, char file, sbyte rank)
        {
            List<ChessBoard> result = new List<ChessBoard>();
            ChessBoard tempboard;
            Square current=new Square(file, rank);
            Square[] moves = Piece.GetSimpleKingMoveDestinations(current);
            foreach (Square move in moves)
            {
                if (move.IsOK() && board[move]<=0)
                {
                    tempboard = Piece.PerformMove(board, current, move);
                    result = Piece.AddNewPosition(result, tempboard, true);
                }
            }
            GetQueenCastlingPosition(board, file, rank, result, current);
            GetKingCastlingPosition(board, file, rank, result, current);
            return result;
        }

        private static void GetKingCastlingPosition(ChessBoard board, char file, sbyte rank, List<ChessBoard> result, Square current)
        {
            ChessBoard tempboard;
            if (WhiteKing.IsSafe(board) && DefaultInfo.WhiteKingIsUnMoved && DefaultInfo.WhiteHsideRookIsUnMoved)
            {
                char rookfile = 'h';
                for (char tfile = file; tfile < 'h'; tfile++)
                {
                    if (board[tfile, rank] == (sbyte)DefaultPieces.WhiteRook)
                    {
                        rookfile = tfile;
                    }
                }
                tempboard = board.ShallowCopy();
                bool CastlingAvailable = true;

                for (char tfile = file; tfile <= 'g'; tfile++)
                {
                    if (board[tfile, rank] != 0 && board[tfile, rank] != (sbyte)DefaultPieces.WhiteKing && board[tfile, rank] != (sbyte)DefaultPieces.WhiteRook)
                    {
                        CastlingAvailable = false;
                        break;
                    }
                }
                for (char tfile = rookfile; tfile >= 'f'; tfile--)
                {
                    if (board[tfile, rank] != 0 && board[tfile, rank] != (sbyte)DefaultPieces.WhiteKing && board[tfile, rank] != (sbyte)DefaultPieces.WhiteRook)
                    {
                        CastlingAvailable = false;
                        break;
                    }
                }
                for (char tfile = rookfile; tfile <= 'f'; tfile++)
                {
                    if (board[tfile, rank] != 0 && board[tfile, rank] != (sbyte)DefaultPieces.WhiteKing && board[tfile, rank] != (sbyte)DefaultPieces.WhiteRook)
                    {
                        CastlingAvailable = false;
                        break;
                    }
                }
                for (char tfile = file; tfile <= 'g'; tfile++)
                {

                    ChessBoard temp2board = board.ShallowCopy();
                    temp2board[file, rank] = 0;
                    temp2board[tfile, rank] = (sbyte)DefaultPieces.WhiteKing;
                    if (!WhiteKing.IsSafe(temp2board))
                    {
                        CastlingAvailable = false;
                        break;
                    }
                }
                if (CastlingAvailable)
                {
                    tempboard = Piece.PerformMove(board, current, new Square('g', rank));
                    tempboard = Piece.PerformMove(tempboard, new Square(rookfile, rank), new Square('f', rank));
                    result.Add(tempboard);
                }
            }
        }

        private static void GetQueenCastlingPosition(ChessBoard board, char file, sbyte rank, List<ChessBoard> result, Square current)
        {
            ChessBoard tempboard;
            if (WhiteKing.IsSafe(board) && DefaultInfo.WhiteKingIsUnMoved && DefaultInfo.WhiteAsideRookIsUnMoved)
            {
                char rookfile = 'a';
                for (char tfile = file; tfile > 'a'; tfile--)
                {
                    if (board[tfile, rank] == (sbyte)DefaultPieces.WhiteRook)
                    {
                        rookfile = tfile;
                    }
                }
                tempboard = board.ShallowCopy();
                bool CastlingAvailable = true;
                for (char tfile = file; tfile >= 'c'; tfile--)
                {
                    if (board[tfile, rank] != 0 && board[tfile, rank] != (sbyte)DefaultPieces.WhiteKing && board[tfile, rank] != (sbyte)DefaultPieces.WhiteRook)
                    {
                        CastlingAvailable = false;
                        break;
                    }
                }
                for (char tfile = rookfile; tfile <= 'd'; tfile++)
                {
                    if (board[tfile, rank] != 0 && board[tfile, rank] != (sbyte)DefaultPieces.WhiteKing && board[tfile, rank] != (sbyte)DefaultPieces.WhiteRook)
                    {
                        CastlingAvailable = false;
                        break;
                    }
                }
                for (char tfile = rookfile; tfile >= 'd'; tfile--)
                {
                    if (board[tfile, rank] != 0 && board[tfile, rank] != (sbyte)DefaultPieces.WhiteKing && board[tfile, rank] != (sbyte)DefaultPieces.WhiteRook)
                    {
                        CastlingAvailable = false;
                        break;
                    }
                }
                for (char tfile = file; tfile >= 'c'; tfile--)
                {
                    ChessBoard temp2board = Piece.PerformMove(board, current, new Square(tfile, rank));
                    if (!WhiteKing.IsSafe(temp2board))
                    {
                        CastlingAvailable = false;
                        break;
                    }
                }
                if (file == 'b')
                {
                    ChessBoard temp2board = Piece.PerformMove(board, current, new Square('c', rank));
                    if (!WhiteKing.IsSafe(temp2board))
                    {
                        CastlingAvailable = false;
                    }
                }
                if (CastlingAvailable)
                {
                    tempboard = Piece.PerformMove(board, current, new Square('c', rank));
                    tempboard = Piece.PerformMove(tempboard, new Square(rookfile, rank), new Square('d', rank));
                    result.Add(tempboard);
                }
            }
        }

    }

    #endregion

    #region Black Pieces
    /*			
        Review VV:
            не бачу сенсу в такому наслідуванні, оскільки всі функції класу	Piece статичні
    */
    public class BlackPiece:Piece
        {
            public static List<ChessBoard> GetReversedPossibleWhitePositions(ChessBoard board, char file, sbyte rank, sbyte piece)
            {
                ChessBoard tempboard = board.ShallowCopy();
                tempboard.ReverseSides();
                Square tempsquare = new Square(file, rank);
                tempsquare.Reverse();
                char piecechar = FIDEnotation.GetLetter(piece);
                FIDEnotation.GetPiecePositionsType function = FIDEnotation.GetWhitePiecePositionsType(piecechar);
                List<ChessBoard> result = function(tempboard, tempsquare.file, tempsquare.rank);
                foreach (ChessBoard temp in result)
                {
                    temp.ReverseSides();
                }
                return result;
            }

            public static List<Square> GetPossibleWhiteAttackersToSquare(ChessBoard board, Square goalsquare)
            {
                ChessBoard tempboard = board.ShallowCopy();
                tempboard.DebugConsoleSimpleDraw();
                Console.WriteLine();
                tempboard.ReverseSides();
                //tempboard.DebugConsoleSimpleDraw();
                Square tempsquare=new Square(goalsquare.file, goalsquare.rank);
                tempsquare.Reverse();
                var result = WhitePiece.GetPossibleBlackAttackersToSquare(tempboard, tempsquare);
                for (int i = 0; i < result.Count; i++ )
                {
                        Square temp = result[i];
                        temp.Reverse();
                        result[i] = temp;
                }
                return result;
            }
        }

        public class BlackPawn
        {
            public static List<ChessBoard> GetPossiblePositions(ChessBoard board, char file, sbyte rank)
            {
                List<ChessBoard> result = new List<ChessBoard>();
                ChessBoard tempboard = board.ShallowCopy();
                Square currentposition = new Square(file, rank);
                if (DefaultInfo.WhiteEnPassantEndangered)
                {
                    GetEnPassantPositions(ref result, board, currentposition);
                }
                if (rank == 7 && board[file, rank - 1] == 0 && board[file, rank - 2] == 0)
                {
                    tempboard = Piece.PerformMove(board, currentposition, new Square(file, rank - 2));
                    result = Piece.AddNewPosition(result, tempboard, false);
                }
                if (board[file, rank - 1] == 0)
                {
                    tempboard = Piece.PerformMove(board, currentposition, new Square(file, rank - 1));
                    result = Piece.AddNewPosition(result, tempboard, false);
                }
                if (file >= 'a' && file <= 'g' && board[(char)(file + 1), rank - 1] > 0)
                {
                    tempboard = Piece.PerformMove(board, currentposition, new Square((char)(file + 1), rank - 1));
                    result = Piece.AddNewPosition(result, tempboard, false);
                }
                if (file >= 'b' && file <= 'h' && board[(char)(file - 1), rank - 1] > 0)
                {
                    tempboard = Piece.PerformMove(board, currentposition, new Square((char)(file - 1), rank - 1));
                    result = Piece.AddNewPosition(result, tempboard, false);
                }
                if (rank > 2 || result.Count == 0)
                {
                    return result;
                }
                else
                {
                    return GetPawnPromotionPositions(file, result, tempboard);
                }
            }

            private static void GetEnPassantPositions(ref List<ChessBoard> result, ChessBoard board, Square currentposition)
            {
                ChessBoard tempboard = board.ShallowCopy();
                bool CheckEnPassant = DefaultInfo.EnPassantPossibleCapture.rank == currentposition.rank - 1 && (DefaultInfo.EnPassantPossibleCapture.file == (char)(currentposition.file + 1) || DefaultInfo.EnPassantPossibleCapture.file == (char)(currentposition.file - 1));
                if (CheckEnPassant)
                {
                    tempboard = Piece.PerformMove(board, currentposition, DefaultInfo.EnPassantPossibleCapture);
                    tempboard[DefaultInfo.EnPassantPossibleCapture.file, DefaultInfo.EnPassantPossibleCapture.rank + 1] = 0;
                    result = Piece.AddNewPosition(result, tempboard, false);
                }
            }

            private static List<ChessBoard> GetPawnPromotionPositions(char file, List<ChessBoard> result, ChessBoard tempboard)
            {
                List<ChessBoard> resultpromotion = new List<ChessBoard>();
                foreach (ChessBoard promotiontempboard in result)
                {
                    tempboard = promotiontempboard.ShallowCopy();
                    char promotionfile = file;
                    for (char tempfile = 'a'; tempfile <= 'h'; tempfile++)
                    {
                        if (tempboard[tempfile, 1] == (sbyte)DefaultPieces.BlackPawn)
                        {
                            promotionfile = tempfile;
                            break;
                        }
                    }
                    //now Promotions!
                    tempboard[promotionfile, 1] = (sbyte)DefaultPieces.BlackkNight;
                    resultpromotion.Add(tempboard.ShallowCopy());
                    tempboard[promotionfile, 1] = (sbyte)DefaultPieces.BlackBishop;
                    resultpromotion.Add(tempboard.ShallowCopy());
                    tempboard[promotionfile, 1] = (sbyte)DefaultPieces.BlackRook;
                    resultpromotion.Add(tempboard.ShallowCopy());
                    tempboard[promotionfile, 1] = (sbyte)DefaultPieces.BlackQueen;
                    resultpromotion.Add(tempboard.ShallowCopy());
                }
                return resultpromotion;
            }
        }

        public class BlackkNight
        {

             public static List<ChessBoard> GetPossiblePositions(ChessBoard board, char file, sbyte rank)
            {
                sbyte piece =(sbyte) DefaultPieces.WhitekNight;
                return BlackPiece.GetReversedPossibleWhitePositions(board, file, rank, piece);
            }

        }

        public class BlackBishop
        {

            public static List<ChessBoard> GetPossiblePositions(ChessBoard board, char file, sbyte rank)
            {
                sbyte piece = (sbyte)DefaultPieces.WhiteBishop;
                return BlackPiece.GetReversedPossibleWhitePositions(board, file, rank, piece);

            }
        }

        public class BlackRook
        {

            public static List<ChessBoard> GetPossiblePositions(ChessBoard board, char file, sbyte rank)
            {
                sbyte piece = (sbyte)DefaultPieces.WhiteRook;
                return BlackPiece.GetReversedPossibleWhitePositions(board, file, rank, piece);

            }
        }

        public class BlackQueen
        {

            public static List<ChessBoard> GetPossiblePositions(ChessBoard board, char file, sbyte rank)
            {
                sbyte piece = (sbyte)DefaultPieces.WhiteQueen;
                return BlackPiece.GetReversedPossibleWhitePositions(board, file, rank, piece);

            }
        }

        public class BlackKing
        {
            public static bool IsSafe(ChessBoard board)
            {
                ChessBoard tempboard = board.ShallowCopy();
                tempboard.ReverseSides();
                return WhiteKing.IsSafe(tempboard);
            }

            public static List<ChessBoard> GetPossiblePositions(ChessBoard board, char file, sbyte rank)
            {
                List<ChessBoard> result = new List<ChessBoard>();
                ChessBoard tempboard;
                Square current = new Square(file, rank);
                Square[] moves = Piece.GetSimpleKingMoveDestinations(current);
                foreach (Square move in moves)
                {
                    if (move.IsOK() && board[move] >= 0)
                    {
                        tempboard = Piece.PerformMove(board, current, move);
                        result = Piece.AddNewPosition(result, tempboard, false);
                    }
                }
                GetQueenCastlingPosition(board, file, rank, result, current);
                GetKingCastlingPosition(board, file, rank, result, current);
                return result;
            }

            private static void GetKingCastlingPosition(ChessBoard board, char file, sbyte rank, List<ChessBoard> result, Square current)
            {
                ChessBoard tempboard;
                if (BlackKing.IsSafe(board) && DefaultInfo.BlackKingIsUnMoved && DefaultInfo.BlackHsideRookIsUnMoved)
                {
                    char rookfile = 'h';
                    for (char tfile = file; tfile < 'h'; tfile++)
                    {
                        if (board[tfile, rank] == (sbyte)DefaultPieces.BlackRook)
                        {
                            rookfile = tfile;
                        }
                    }
                    tempboard = board.ShallowCopy();
                    bool CastlingAvailable = true;

                    for (char tfile = file; tfile <= 'g'; tfile++)
                    {
                        if (board[tfile, rank] != 0 && board[tfile, rank] != (sbyte)DefaultPieces.BlackKing && board[tfile, rank] != (sbyte)DefaultPieces.BlackRook)
                        {
                            CastlingAvailable = false;
                            return;
                        }
                    }
                    for (char tfile = rookfile; tfile >= 'f'; tfile--)
                    {
                        if (board[tfile, rank] != 0 && board[tfile, rank] != (sbyte)DefaultPieces.BlackKing && board[tfile, rank] != (sbyte)DefaultPieces.BlackRook)
                        {
                            CastlingAvailable = false;
                            return;
                        }
                    }
                    for (char tfile = rookfile; tfile <= 'f'; tfile++)
                    {
                        if (board[tfile, rank] != 0 && board[tfile, rank] != (sbyte)DefaultPieces.BlackKing && board[tfile, rank] != (sbyte)DefaultPieces.BlackRook)
                        {
                            CastlingAvailable = false;
                            return;
                        }
                    }
                    for (char tfile = file; tfile <= 'g'; tfile++)
                    {

                        ChessBoard temp2board = board.ShallowCopy();
                        temp2board[file, rank] = 0;
                        temp2board[tfile, rank] = (sbyte)DefaultPieces.BlackKing;
                        if (!BlackKing.IsSafe(temp2board))
                        {
                            CastlingAvailable = false;
                            return;
                        }
                    }
                    if (CastlingAvailable)
                    {
                        tempboard = Piece.PerformMove(board, current, new Square('g', rank));
                        tempboard = Piece.PerformMove(tempboard, new Square(rookfile, rank), new Square('f', rank));
                        result.Add(tempboard);
                    }
                }
            }

            private static void GetQueenCastlingPosition(ChessBoard board, char file, sbyte rank, List<ChessBoard> result, Square current)
            {
                ChessBoard tempboard;
                if (BlackKing.IsSafe(board) && DefaultInfo.BlackKingIsUnMoved && DefaultInfo.BlackAsideRookIsUnMoved)
                {
                    char rookfile = 'a';
                    for (char tfile = file; tfile > 'a'; tfile--)
                    {
                        if (board[tfile, rank] == (sbyte)DefaultPieces.BlackRook)
                        {
                            rookfile = tfile;
                        }
                    }
                    tempboard = board.ShallowCopy();
                    bool CastlingAvailable = true;
                    for (char tfile = file; tfile >= 'c'; tfile--)
                    {
                        if (board[tfile, rank] != 0 && board[tfile, rank] != (sbyte)DefaultPieces.BlackKing && board[tfile, rank] != (sbyte)DefaultPieces.BlackRook)
                        {
                            CastlingAvailable = false;
                            return;
                        }
                    }
                    for (char tfile = rookfile; tfile <= 'd'; tfile++)
                    {
                        if (board[tfile, rank] != 0 && board[tfile, rank] != (sbyte)DefaultPieces.BlackKing && board[tfile, rank] != (sbyte)DefaultPieces.BlackRook)
                        {
                            CastlingAvailable = false;
                            return;
                        }
                    }
                    for (char tfile = rookfile; tfile >= 'd'; tfile--)
                    {
                        if (board[tfile, rank] != 0 && board[tfile, rank] != (sbyte)DefaultPieces.BlackKing && board[tfile, rank] != (sbyte)DefaultPieces.BlackRook)
                        {
                            CastlingAvailable = false;
                            return;
                        }
                    }
                    for (char tfile = file; tfile >= 'c'; tfile--)
                    {
                        ChessBoard temp2board = Piece.PerformMove(board, current, new Square(tfile, rank));
                        if (!BlackKing.IsSafe(temp2board))
                        {
                            CastlingAvailable = false;
                            return;
                        }
                    }
                    if (file == 'b')
                    {
                        ChessBoard temp2board = Piece.PerformMove(board, current, new Square('c', rank));
                        if (!BlackKing.IsSafe(temp2board))
                        {
                            CastlingAvailable = false;
                        }
                    }
                    if (CastlingAvailable)
                    {
                        tempboard = Piece.PerformMove(board, current, new Square('c', rank));
                        tempboard = Piece.PerformMove(tempboard, new Square(rookfile, rank), new Square('d', rank));
                        result.Add(tempboard);
                    }
                }
            }

        }
    }
    #endregion
