using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueChessGame
{

    public enum DefaultPieces : sbyte { BlackKing = -6, BlackQueen = -5, BlackRook = -4, BlackBishop = -3, BlackkNight = -2, BlackPawn = -1, Empty = 0, WhitePawn = 1, WhitekNight = 2, WhiteBishop = 3, WhiteRook = 4, WhiteQueen = 5, WhiteKing = 6 };

    //public abstract class Piece
    //{
    //    public enum DefaultPieces : sbyte { BlackKing = -6, BlackQueen = -5, BlackRook = -4, BlackBishop = -3, BlackkNight = -2, BlackPawn = -1, Empty = 0, WhitePawn = 1, WhitekNight = 2, WhiteBishop = 3, WhiteRook = 4, WhiteQueen = 5, WhiteKing = 6 };

    //    public Piece()
    //    {

    //    }

    //    public abstract List<ChessBoard> GetPossiblePositions(ChessBoard board, char file, sbyte rank);

    //    // public abstract ChessBoard FIDEnotation(ChessBoard board, char startfile, sbyte );
    //}

    //interface IPiece
    //{
    //    List<ChessBoard> GetPossiblePositions(ChessBoard board, char file, sbyte rank);
    //}

    #region White Pieces
    public class WhitePawn
    {

        public static List<ChessBoard> GetPossiblePositions(ChessBoard board, char file, sbyte rank)
        {
            List<ChessBoard> result = new List<ChessBoard>();
            ChessBoard tempboard;
            sbyte usedpiece = (sbyte)DefaultPieces.WhitePawn;
            //en passant!
            if (DefaultInfo.BlackEnPassantEndangered)
            {
                if (DefaultInfo.EnPassantPossibleCapture.file==(char)(file+1) && DefaultInfo.EnPassantPossibleCapture.rank==rank+1)
                {
                    tempboard = board.ShallowCopy();
                    tempboard[file, rank] = 0;
                    tempboard[DefaultInfo.EnPassantPossibleCapture.file, DefaultInfo.EnPassantPossibleCapture.rank] = usedpiece;
                    tempboard[DefaultInfo.EnPassantPossibleCapture.file, DefaultInfo.EnPassantPossibleCapture.rank - 1] = 0;
                    if (WhiteKing.IsSafe(tempboard))
                    {
                        result.Add(tempboard);
                    }
                }
                else if(DefaultInfo.EnPassantPossibleCapture.file==(char)(file-1) && DefaultInfo.EnPassantPossibleCapture.rank==rank+1)
                {
                    tempboard = board.ShallowCopy();
                    tempboard[file, rank] = 0;
                    tempboard[DefaultInfo.EnPassantPossibleCapture.file, DefaultInfo.EnPassantPossibleCapture.rank] = usedpiece;
                    tempboard[DefaultInfo.EnPassantPossibleCapture.file, DefaultInfo.EnPassantPossibleCapture.rank - 1] = 0;
                    if (WhiteKing.IsSafe(tempboard))
                    {
                        result.Add(tempboard);
                    }
                }
            }


            if (rank == 2 && board[file, rank + 2] == 0)
            {
                tempboard = board.ShallowCopy();
                tempboard[file, rank] = 0;
                tempboard[file, rank + 2] = usedpiece;
                if (WhiteKing.IsSafe(tempboard))
                {
                    result.Add(tempboard);
                }
            }
            if (board[file, rank + 1] == 0)
            {
                tempboard = board.ShallowCopy();
                tempboard[file, rank] = 0;
                tempboard[file, rank + 1] = usedpiece;
                if (WhiteKing.IsSafe(tempboard))
                {
                    result.Add(tempboard);
                }
            }
            if (file == 'a' && board['b', rank + 1] < 0)
            {
                tempboard = board.ShallowCopy();
                tempboard[file, rank] = 0;
                tempboard[(char)(file + 1), rank + 1] = usedpiece;
                if (WhiteKing.IsSafe(tempboard))
                {
                    result.Add(tempboard);
                }
            }
            if (file == 'h' && board['g', rank + 1] < 0)
            {
                tempboard = board.ShallowCopy();
                tempboard[file, rank] = 0;
                tempboard[(char)(file - 1), rank + 1] = usedpiece;
                if (WhiteKing.IsSafe(tempboard))
                {
                    result.Add(tempboard);
                }
            }
            if (file > 'a' && file < 'h')
            {
                if (board[(char)(file - 1), rank + 1] < 0)
                {
                    tempboard = board.ShallowCopy();
                    tempboard[file, rank] = 0;
                    tempboard[(char)(file - 1), rank + 1] = usedpiece;
                    if (WhiteKing.IsSafe(tempboard))
                    {
                        result.Add(tempboard);
                    }
                }
                if (board[(char)(file + 1), rank + 1] < 0)
                {
                    tempboard = board.ShallowCopy();
                    tempboard[file, rank] = 0;
                    tempboard[(char)(file + 1), rank + 1] = usedpiece;
                    if (WhiteKing.IsSafe(tempboard))
                    {
                        result.Add(tempboard);
                    }
                }
            }
            if (rank <= 6 || result.Count == 0)
            {
                return result;
            }
            else
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
                            //Console.WriteLine(tempfile);
                            promotionfile = tempfile;
                            break;
                        }
                    }
                    //Console.WriteLine(promotionfile);
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
    }

    public class WhitekNight
    {

        public static List<ChessBoard> GetPossiblePositions(ChessBoard board, char file, sbyte rank)
        {
            List<ChessBoard> result = new List<ChessBoard>();
            ChessBoard tempboard;
            sbyte usedpiece = (sbyte)DefaultPieces.WhitekNight;
            Square[] moves = new Square[8];
            moves[0] = new Square((char)(file + 1), rank + 2);
            moves[1] = new Square((char)(file + 2), rank + 1);
            moves[2] = new Square((char)(file + 2), rank - 1);
            moves[3] = new Square((char)(file + 1), rank - 2);
            moves[4] = new Square((char)(file - 1), rank + 2);
            moves[5] = new Square((char)(file - 2), rank + 1);
            moves[6] = new Square((char)(file - 2), rank - 1);
            moves[7] = new Square((char)(file - 1), rank - 2);
            foreach (Square move in moves)
            {
                if (move.file >= 'a' && move.file <= 'h' && move.rank >= 1 && move.rank <= 8)
                {
                    if (board[move.file, move.rank] <= 0)
                    {
                        tempboard = board.ShallowCopy();
                        tempboard[file, rank] = 0;
                        tempboard[move.file, move.rank] = usedpiece;
                        if (WhiteKing.IsSafe(tempboard))
                        {
                            result.Add(tempboard);
                        }
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
            sbyte usedpiece = (sbyte)DefaultPieces.WhiteBishop;
            List<Square> moves = new List<Square>();
            #region BishopMovesCycles
            //upper-right
            for (int i = 1; i < 8; i++)
            {
                if ((file + i) > 'h' || rank + i > 8)
                {
                    break;
                }
                else
                {
                    if (board[(char)(file + i), rank + i] > 0)
                    {
                        break;
                    }
                    else if (board[(char)(file + i), rank + i] < 0)
                    {
                        moves.Add(new Square((char)(file + i), rank + i));
                        break;
                    }
                    else
                    {
                        moves.Add(new Square((char)(file + i), rank + i));
                    }
                }
            }
            //lower-right
            for (int i = 1; i < 8; i++)
            {
                if ((file + i) > 'h' || rank - i < 1)
                {
                    break;
                }
                else
                {
                    if (board[(char)(file + i), rank - i] > 0)
                    {
                        break;
                    }
                    else if (board[(char)(file + i), rank - i] < 0)
                    {
                        moves.Add(new Square((char)(file + i), rank - i));
                        break;
                    }
                    else
                    {
                        moves.Add(new Square((char)(file + i), rank + -i));
                    }
                }
            }
            //upper-left
            for (int i = 1; i < 8; i++)
            {
                if ((file - i) < 'a' || rank + i > 8)
                {
                    break;
                }
                else
                {
                    if (board[(char)(file - i), rank + i] > 0)
                    {
                        break;
                    }
                    else if (board[(char)(file - i), rank + i] < 0)
                    {
                        moves.Add(new Square((char)(file - i), rank + i));
                        break;
                    }
                    else
                    {
                        moves.Add(new Square((char)(file - i), rank + i));
                    }
                }
            }
            //lower-left
            for (int i = 1; i < 8; i++)
            {
                if ((file - i) < 'a' || rank - i < 1)
                {
                    break;
                }
                else
                {
                    if (board[(char)(file - i), rank - i] > 0)
                    {
                        break;
                    }
                    else if (board[(char)(file - i), rank - i] < 0)
                    {
                        moves.Add(new Square((char)(file - i), rank - i));
                        break;
                    }
                    else
                    {
                        moves.Add(new Square((char)(file - i), rank - i));
                    }
                }
            }
            #endregion
            foreach (Square move in moves)
            {
                tempboard = board.ShallowCopy();
                tempboard[file, rank] = 0;
                tempboard[move.file, move.rank] = usedpiece;
                if (WhiteKing.IsSafe(tempboard))
                {
                    result.Add(tempboard);
                }
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
            sbyte usedpiece = (sbyte)DefaultPieces.WhiteRook;
            List<Square> moves = new List<Square>();
            #region RookMoveCycles
            //Up
            for (int i = rank + 1; i <= 8; i++)
            {
                if (board[file, i] > 0)
                {
                    break;
                }
                else
                {
                    if (board[file, i] < 0)
                    {
                        moves.Add(new Square(file, i));
                        break;
                    }
                    else
                    {
                        moves.Add(new Square(file, i));
                    }
                }
            }
            //Down
            for (int i = rank - 1; i >= 1; i--)
            {
                if (board[file, i] > 0)
                {
                    break;
                }
                else
                {
                    if (board[file, i] < 0)
                    {
                        moves.Add(new Square(file, i));
                        break;
                    }
                    else
                    {
                        moves.Add(new Square(file, i));
                    }
                }
            }
            //Right
            for (char tchar = (char)(file + 1); tchar <= 'h'; tchar++)
            {
                if (board[tchar, rank] > 0)
                {
                    break;
                }
                else
                {
                    if (board[tchar, rank] < 0)
                    {
                        moves.Add(new Square(tchar, rank));
                        break;
                    }
                    else
                    {
                        moves.Add(new Square(tchar, rank));
                    }
                }
            }
            //Left
            for (char tchar = (char)(file - 1); tchar >= 'a'; tchar--)
            {
                if (board[tchar, rank] > 0)
                {
                    break;
                }
                else
                {
                    if (board[tchar, rank] < 0)
                    {
                        moves.Add(new Square(tchar, rank));
                        break;
                    }
                    else
                    {
                        moves.Add(new Square(tchar, rank));
                    }
                }
            }
            #endregion
            foreach (Square move in moves)
            {
                tempboard = board.ShallowCopy();
                tempboard[file, rank] = 0;
                tempboard[move.file, move.rank] = usedpiece;
                if (WhiteKing.IsSafe(tempboard))
                {
                    result.Add(tempboard);
                }
            }
            return result;
        }
    }

    public class WhiteQueen
    {

        public static List<ChessBoard> GetPossiblePositions(ChessBoard board, char file, sbyte rank)
        {
            List<ChessBoard> result = new List<ChessBoard>();
            ChessBoard tempboard;
            sbyte usedpiece = (sbyte)DefaultPieces.WhiteQueen;
            List<Square> moves = new List<Square>();
            #region QueenMoveCycles
            //upper-right
            for (int i = 1; i < 8; i++)
            {
                if ((file + i) > 'h' || rank + i > 8)
                {
                    break;
                }
                else
                {
                    if (board[(char)(file + i), rank + i] > 0)
                    {
                        break;
                    }
                    else if (board[(char)(file + i), rank + i] < 0)
                    {
                        moves.Add(new Square((char)(file + i), rank + i));
                        break;
                    }
                    else
                    {
                        moves.Add(new Square((char)(file + i), rank + i));
                    }
                }
            }
            //lower-right
            for (int i = 1; i < 8; i++)
            {
                if ((file + i) > 'h' || rank - i < 1)
                {
                    break;
                }
                else
                {
                    if (board[(char)(file + i), rank - i] > 0)
                    {
                        break;
                    }
                    else if (board[(char)(file + i), rank - i] < 0)
                    {
                        moves.Add(new Square((char)(file + i), rank - i));
                        break;
                    }
                    else
                    {
                        moves.Add(new Square((char)(file + i), rank + -i));
                    }
                }
            }
            //upper-left
            for (int i = 1; i < 8; i++)
            {
                if ((file - i) < 'a' || rank + i > 8)
                {
                    break;
                }
                else
                {
                    if (board[(char)(file - i), rank + i] > 0)
                    {
                        break;
                    }
                    else if (board[(char)(file - i), rank + i] < 0)
                    {
                        moves.Add(new Square((char)(file - i), rank + i));
                        break;
                    }
                    else
                    {
                        moves.Add(new Square((char)(file - i), rank + i));
                    }
                }
            }
            //lower-left
            for (int i = 1; i < 8; i++)
            {
                if ((file - i) < 'a' || rank - i < 1)
                {
                    break;
                }
                else
                {
                    if (board[(char)(file - i), rank - i] > 0)
                    {
                        break;
                    }
                    else if (board[(char)(file - i), rank - i] < 0)
                    {
                        moves.Add(new Square((char)(file - i), rank - i));
                        break;
                    }
                    else
                    {
                        moves.Add(new Square((char)(file - i), rank - i));
                    }
                }
            }
            //Up
            for (int i = rank + 1; i <= 8; i++)
            {
                if (board[file, i] > 0)
                {
                    break;
                }
                else
                {
                    if (board[file, i] < 0)
                    {
                        moves.Add(new Square(file, i));
                        break;
                    }
                    else
                    {
                        moves.Add(new Square(file, i));
                    }
                }
            }
            //Down
            for (int i = rank - 1; i >= 1; i--)
            {
                if (board[file, i] > 0)
                {
                    break;
                }
                else
                {
                    if (board[file, i] < 0)
                    {
                        moves.Add(new Square(file, i));
                        break;
                    }
                    else
                    {
                        moves.Add(new Square(file, i));
                    }
                }
            }
            //Right
            for (char tchar = (char)(file + 1); tchar <= 'h'; tchar++)
            {
                if (board[tchar, rank] > 0)
                {
                    break;
                }
                else
                {
                    if (board[tchar, rank] < 0)
                    {
                        moves.Add(new Square(tchar, rank));
                        break;
                    }
                    else
                    {
                        moves.Add(new Square(tchar, rank));
                    }
                }
            }
            //Left
            for (char tchar = (char)(file - 1); tchar >= 'a'; tchar--)
            {
                if (board[tchar, rank] > 0)
                {
                    break;
                }
                else
                {
                    if (board[tchar, rank] < 0)
                    {
                        moves.Add(new Square(tchar, rank));
                        break;
                    }
                    else
                    {
                        moves.Add(new Square(tchar, rank));
                    }
                }
            }

            #endregion

            foreach (Square move in moves)
            {
                tempboard = board.ShallowCopy();
                tempboard[file, rank] = 0;
                tempboard[move.file, move.rank] = usedpiece;
                if (WhiteKing.IsSafe(tempboard))
                {
                    result.Add(tempboard);
                }
            }
            return result;
        }
    }

    public class WhiteKing
    {

        public static bool IsSafe(ChessBoard board)
        {
            //get White King coordinates
            char file = 'a'; sbyte rank = 1;
            for (char tfile = 'a'; tfile <= 'h'; tfile++)
            {
                for (int trank = 1; trank <= 8; trank++)
                {
                    if (board[tfile, trank] == (sbyte)DefaultPieces.WhiteKing)
                    {
                        file = tfile;
                        rank = (sbyte)trank;
                        break;
                    }
                }
            }
            //check if chessboard is ok
            if (board[file, rank] != 6)
            {
                return false;
            }
            //check for enemy Pawn
            #region CheckPawn
            if (file == 'a' && rank >= 1 && rank < 8)
            {
                if (board['b', rank + 1] == -1)
                {
                    return false;
                }
            }
            if (file == 'h' && rank >= 1 && rank < 8)
            {
                if (board['g', rank + 1] == -1)
                {
                    return false;
                }
            }
            for (char tfile = 'b'; tfile <= 'g'; tfile++)
            {
                if (rank >= 1 && rank < 8)
                {
                    if (board[(char)(tfile + 1), rank + 1] == -1 || board[(char)(tfile - 1), rank + 1] == -1)
                    {
                        return false;
                    }
                }
            }
            #endregion
            //check for enemy kNight
            #region CheckkNight
            Square[] moves_array = new Square[8];
            moves_array[0] = new Square((char)(file + 1), rank + 2);
            moves_array[1] = new Square((char)(file + 2), rank + 1);
            moves_array[2] = new Square((char)(file + 2), rank - 1);
            moves_array[3] = new Square((char)(file + 1), rank - 2);
            moves_array[4] = new Square((char)(file - 1), rank + 2);
            moves_array[5] = new Square((char)(file - 2), rank + 1);
            moves_array[6] = new Square((char)(file - 2), rank - 1);
            moves_array[7] = new Square((char)(file - 1), rank - 2);
            foreach (Square move in moves_array)
            {
                if (move.file >= 'a' && move.file <= 'h' && move.rank >= 1 && move.rank <= 8)
                {
                    if (board[move.file, move.rank] == -2)
                    {
                        return false;
                    }
                }
            }
            #endregion
            //for enemy Rook or Queen (horizontal)
            #region HorizontalCheck
            //Up
            for (int i = rank + 1; i <= 8; i++)
            {
                if (board[file, i] > 0)
                {
                    break;
                }
                else if (board[file, i] == -4 || board[file, i] == -5)
                {
                    return false;
                }

            }
            //Down
            for (int i = rank - 1; i >= 1; i--)
            {
                if (board[file, i] > 0)
                {
                    break;
                }
                else if (board[file, i] == -4 || board[file, i] == -5)
                {
                    return false;
                }
            }
            //Right
            for (char tchar = (char)(file + 1); tchar <= 'h'; tchar++)
            {
                if (board[tchar, rank] > 0)
                {
                    break;
                }
                else if (board[tchar, rank] == -4 || board[tchar, rank] == -5)
                {
                    return false;
                }

            }
            //Left
            for (char tchar = (char)(file - 1); tchar >= 'a'; tchar--)
            {
                if (board[tchar, rank] > 0)
                {
                    break;
                }
                else if (board[tchar, rank] == -4 || board[tchar, rank] == -5)
                {
                    return false;
                }
            }
            #endregion
            //for enemy Bishop or Queen (diagonal)
            #region DiagonalCheck
            //upper-right
            for (int i = 1; i < 8; i++)
            {
                if ((file + i) > 'h' || rank + i > 8)
                {
                    break;
                }
                else
                {
                    if (board[(char)(file + i), rank + i] > 0)
                    {
                        break;
                    }
                    else if (board[(char)(file + i), rank + i] == -3 || board[(char)(file + i), rank + i] == -5)
                    {
                        return false;
                    }
                }
            }
            //lower-right
            for (int i = 1; i < 8; i++)
            {
                if ((file + i) > 'h' || rank - i < 1)
                {
                    break;
                }
                else
                {
                    if (board[(char)(file + i), rank - i] > 0)
                    {
                        break;
                    }
                    else if (board[(char)(file + i), rank - i] == -3 || board[(char)(file + i), rank - i] == -5)
                    {
                        return false;
                    }
                }
            }
            //upper-left
            for (int i = 1; i < 8; i++)
            {
                if ((file - i) < 'a' || rank + i > 8)
                {
                    break;
                }
                else
                {
                    if (board[(char)(file - i), rank + i] > 0)
                    {
                        break;
                    }
                    else if (board[(char)(file - i), rank + i] == -3 || board[(char)(file - i), rank + i] == -5)
                    {
                        return false;
                    }
                }
            }
            //lower-left
            for (int i = 1; i < 8; i++)
            {
                if ((file - i) < 'a' || rank - i < 1)
                {
                    break;
                }
                else
                {
                    if (board[(char)(file - i), rank - i] > 0)
                    {
                        break;
                    }
                    else if (board[(char)(file - i), rank - i] == -3 || board[(char)(file - i), rank - i] == -5)
                    {
                        return false;
                    }
                }
            }
            #endregion

            return true;
        }

        public static List<ChessBoard> GetPossiblePositions(ChessBoard board, char file, sbyte rank)
        {
            List<ChessBoard> result = new List<ChessBoard>();
            ChessBoard tempboard;
            sbyte usedpiece = (sbyte)DefaultPieces.WhiteKing;
            //default moves
            Square[] moves = new Square[8];
            moves[0] = new Square(file, rank + 1);
            moves[1] = new Square(file, rank - 1);
            moves[2] = new Square((char)(file + 1), rank + 1);
            moves[3] = new Square((char)(file + 1), rank);
            moves[4] = new Square((char)(file + 1), rank - 1);
            moves[5] = new Square((char)(file - 1), rank + 1);
            moves[6] = new Square((char)(file - 1), rank);
            moves[7] = new Square((char)(file - 1), rank - 1);
            foreach (Square move in moves)
            {
                if (move.file >= 'a' && move.file <= 'h' && move.rank >= 1 && move.rank <= 8)
                {
                    if (board[move.file, move.rank] <= 0)
                    {
                        tempboard = board.ShallowCopy();
                        tempboard[file, rank] = 0;
                        tempboard[move.file, move.rank] = usedpiece;
                        if (WhiteKing.IsSafe(tempboard))
                        {
                            result.Add(tempboard);
                        }
                    }
                }
            }
            //check Castling
            #region Castling0-0-0
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
                    if (board[tfile, rank] != 0 && board[tfile, rank] != usedpiece && board[tfile, rank] != (sbyte)DefaultPieces.WhiteRook)
                    {
                        CastlingAvailable = false;
                        break;
                    }
                }
                for (char tfile = rookfile; tfile <= 'd'; tfile++)
                {
                    if (board[tfile, rank] != 0 && board[tfile, rank] != usedpiece && board[tfile, rank] != (sbyte)DefaultPieces.WhiteRook)
                    {
                        CastlingAvailable = false;
                        break;
                    }
                }
                for (char tfile = rookfile; tfile >= 'd'; tfile--)
                {
                    if (board[tfile, rank] != 0 && board[tfile, rank] != usedpiece && board[tfile, rank] != (sbyte)DefaultPieces.WhiteRook)
                    {
                        CastlingAvailable = false;
                        break;
                    }
                }
                for (char tfile = file; tfile >= 'c'; tfile--)
                {

                    ChessBoard temp2board = board.ShallowCopy();
                    temp2board[file, rank] = 0;
                    temp2board[tfile, rank] = usedpiece;
                    if (!WhiteKing.IsSafe(temp2board))
                    {
                        CastlingAvailable = false;
                        break;
                    }
                }
                if (file == 'b')
                {
                    ChessBoard temp2board = board.ShallowCopy();
                    temp2board[file, rank] = 0;
                    temp2board['c', rank] = usedpiece;
                    if (!WhiteKing.IsSafe(temp2board))
                    {
                        CastlingAvailable = false;
                    }
                }
                if (CastlingAvailable)
                {
                    tempboard[file, rank] = 0;
                    tempboard['c', rank] = usedpiece;
                    tempboard[rookfile, rank] = 0;
                    tempboard['d', rank] = (sbyte)DefaultPieces.WhiteRook;
                    result.Add(tempboard);

                }
            }
            #endregion

            #region Castling0-0
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
                    if (board[tfile, rank] != 0 && board[tfile, rank] != usedpiece && board[tfile, rank] != (sbyte)DefaultPieces.WhiteRook)
                    {
                        CastlingAvailable = false;
                        break;
                    }
                }
                for (char tfile = rookfile; tfile >= 'f'; tfile--)
                {
                    if (board[tfile, rank] != 0 && board[tfile, rank] != usedpiece && board[tfile, rank] != (sbyte)DefaultPieces.WhiteRook)
                    {
                        CastlingAvailable = false;
                        break;
                    }
                }
                for (char tfile = rookfile; tfile <= 'f'; tfile++)
                {
                    if (board[tfile, rank] != 0 && board[tfile, rank] != usedpiece && board[tfile, rank] != (sbyte)DefaultPieces.WhiteRook)
                    {
                        CastlingAvailable = false;
                        break;
                    }
                }

                for (char tfile = file; tfile <= 'g'; tfile++)
                {

                    ChessBoard temp2board = board.ShallowCopy();
                    temp2board[file, rank] = 0;
                    temp2board[tfile, rank] = usedpiece;
                    if (!WhiteKing.IsSafe(temp2board))
                    {
                        CastlingAvailable = false;
                        break;
                    }
                }


                if (CastlingAvailable)
                {
                    tempboard[file, rank] = 0;
                    tempboard['g', rank] = usedpiece;
                    tempboard[rookfile, rank] = 0;
                    tempboard['f', rank] = (sbyte)DefaultPieces.WhiteRook;
                    result.Add(tempboard);
                }
            }
            #endregion

            return result;
        }
    }

    #endregion

    #region Black Pieces
        public class BlackPawn
        {

            public static List<ChessBoard> GetPossiblePositions(ChessBoard board, char file, sbyte rank)
            {
                List<ChessBoard> result = new List<ChessBoard>();
                ChessBoard tempboard;
                sbyte usedpiece = (sbyte)DefaultPieces.BlackPawn;
                //en passant!
                if (DefaultInfo.WhiteEnPassantEndangered)
                {
                    if (DefaultInfo.EnPassantPossibleCapture.file == (char)(file + 1) && DefaultInfo.EnPassantPossibleCapture.rank == rank - 1)
                    {
                        tempboard = board.ShallowCopy();
                        tempboard[file, rank] = 0;
                        tempboard[DefaultInfo.EnPassantPossibleCapture.file, DefaultInfo.EnPassantPossibleCapture.rank] = usedpiece;
                        tempboard[DefaultInfo.EnPassantPossibleCapture.file, DefaultInfo.EnPassantPossibleCapture.rank + 1] = 0;
                        if (BlackKing.IsSafe(tempboard))
                        {
                            result.Add(tempboard);
                        }
                    }
                    else if (DefaultInfo.EnPassantPossibleCapture.file == (char)(file - 1) && DefaultInfo.EnPassantPossibleCapture.rank == rank - 1)
                    {
                        tempboard = board.ShallowCopy();
                        tempboard[file, rank] = 0;
                        tempboard[DefaultInfo.EnPassantPossibleCapture.file, DefaultInfo.EnPassantPossibleCapture.rank] = usedpiece;
                        tempboard[DefaultInfo.EnPassantPossibleCapture.file, DefaultInfo.EnPassantPossibleCapture.rank + 1] = 0;
                        if (BlackKing.IsSafe(tempboard))
                        {
                            result.Add(tempboard);
                        }
                    }
                }


                if (rank == 7 && board[file, rank - 2] == 0)
                {
                    tempboard = board.ShallowCopy();
                    tempboard[file, rank] = 0;
                    tempboard[file, rank - 2] = usedpiece;
                    if (BlackKing.IsSafe(tempboard))
                    {
                        result.Add(tempboard);
                    }
                }
                if (board[file, rank - 1] == 0)
                {
                    tempboard = board.ShallowCopy();
                    tempboard[file, rank] = 0;
                    tempboard[file, rank - 1] = usedpiece;
                    if (BlackKing.IsSafe(tempboard))
                    {
                        result.Add(tempboard);
                    }
                }
                if (file == 'a' && board['b', rank - 1] > 0)
                {
                    tempboard = board.ShallowCopy();
                    tempboard[file, rank] = 0;
                    tempboard[(char)(file + 1), rank - 1] = usedpiece;
                    if (BlackKing.IsSafe(tempboard))
                    {
                        result.Add(tempboard);
                    }
                }
                if (file == 'h' && board['g', rank - 1] > 0)
                {
                    tempboard = board.ShallowCopy();
                    tempboard[file, rank] = 0;
                    tempboard[(char)(file - 1), rank - 1] = usedpiece;
                    if (BlackKing.IsSafe(tempboard))
                    {
                        result.Add(tempboard);
                    }
                }
                if (file > 'a' && file < 'h')
                {
                    if (board[(char)(file - 1), rank - 1] > 0)
                    {
                        tempboard = board.ShallowCopy();
                        tempboard[file, rank] = 0;
                        tempboard[(char)(file - 1), rank - 1] = usedpiece;
                        if (BlackKing.IsSafe(tempboard))
                        {
                            result.Add(tempboard);
                        }
                    }
                    if (board[(char)(file + 1), rank - 1] > 0)
                    {
                        tempboard = board.ShallowCopy();
                        tempboard[file, rank] = 0;
                        tempboard[(char)(file + 1), rank - 1] = usedpiece;
                        if (BlackKing.IsSafe(tempboard))
                        {
                            result.Add(tempboard);
                        }
                    }
                }
                if (rank >= 3 || result.Count == 0)
                {
                    return result;
                }
                else
                {
                    List<ChessBoard> resultpromotion = new List<ChessBoard>();
                    foreach (ChessBoard promotiontempboard in result)
                    {
                        tempboard = promotiontempboard.ShallowCopy();
                        char promotionfile = file;
                        for (char tempfile = 'a'; tempfile <= 'h'; tempfile++)
                        {
                            if (tempboard[tempfile, 8] == (sbyte)DefaultPieces.BlackPawn)
                            {
                                //Console.WriteLine(tempfile);
                                promotionfile = tempfile;
                                break;
                            }
                        }
                        //Console.WriteLine(promotionfile);
                        //now Promotions!
                        tempboard[promotionfile, 8] = (sbyte)DefaultPieces.BlackkNight;
                        resultpromotion.Add(tempboard.ShallowCopy());
                        tempboard[promotionfile, 8] = (sbyte)DefaultPieces.BlackBishop;
                        resultpromotion.Add(tempboard.ShallowCopy());
                        tempboard[promotionfile, 8] = (sbyte)DefaultPieces.BlackRook;
                        resultpromotion.Add(tempboard.ShallowCopy());
                        tempboard[promotionfile, 8] = (sbyte)DefaultPieces.BlackQueen;
                        resultpromotion.Add(tempboard.ShallowCopy());
                    }
                    return resultpromotion;
                }
            }
        }

        public class BlackkNight
        {

            public static List<ChessBoard> GetPossiblePositions(ChessBoard board, char file, sbyte rank)
            {
                List<ChessBoard> result = new List<ChessBoard>();
                ChessBoard tempboard;
                sbyte usedpiece = (sbyte)DefaultPieces.BlackkNight;
                Square[] moves = new Square[8];
                moves[0] = new Square((char)(file + 1), rank + 2);
                moves[1] = new Square((char)(file + 2), rank + 1);
                moves[2] = new Square((char)(file + 2), rank - 1);
                moves[3] = new Square((char)(file + 1), rank - 2);
                moves[4] = new Square((char)(file - 1), rank + 2);
                moves[5] = new Square((char)(file - 2), rank + 1);
                moves[6] = new Square((char)(file - 2), rank - 1);
                moves[7] = new Square((char)(file - 1), rank - 2);
                foreach (Square move in moves)
                {
                    if (move.file >= 'a' && move.file <= 'h' && move.rank >= 1 && move.rank <= 8)
                    {
                        if (board[move.file, move.rank] >= 0)
                        {
                            tempboard = board.ShallowCopy();
                            tempboard[file, rank] = 0;
                            tempboard[move.file, move.rank] = usedpiece;
                            if (BlackKing.IsSafe(tempboard))
                            {
                                result.Add(tempboard);
                            }
                        }
                    }
                }
                return result;
            }
        }

        public class BlackBishop
        {

            public static List<ChessBoard> GetPossiblePositions(ChessBoard board, char file, sbyte rank)
            {
                List<ChessBoard> result = new List<ChessBoard>();
                ChessBoard tempboard;
                sbyte usedpiece = (sbyte)DefaultPieces.BlackBishop;
                List<Square> moves = new List<Square>();
                #region BishopMovesCycles
                //upper-right
                for (int i = 1; i < 8; i++)
                {
                    if ((file + i) > 'h' || rank + i > 8)
                    {
                        break;
                    }
                    else
                    {
                        if (board[(char)(file + i), rank + i] < 0)
                        {
                            break;
                        }
                        else if (board[(char)(file + i), rank + i] > 0)
                        {
                            moves.Add(new Square((char)(file + i), rank + i));
                            break;
                        }
                        else
                        {
                            moves.Add(new Square((char)(file + i), rank + i));
                        }
                    }
                }
                //lower-right
                for (int i = 1; i < 8; i++)
                {
                    if ((file + i) > 'h' || rank - i < 1)
                    {
                        break;
                    }
                    else
                    {
                        if (board[(char)(file + i), rank - i] < 0)
                        {
                            break;
                        }
                        else if (board[(char)(file + i), rank - i] > 0)
                        {
                            moves.Add(new Square((char)(file + i), rank - i));
                            break;
                        }
                        else
                        {
                            moves.Add(new Square((char)(file + i), rank + -i));
                        }
                    }
                }
                //upper-left
                for (int i = 1; i < 8; i++)
                {
                    if ((file - i) < 'a' || rank + i > 8)
                    {
                        break;
                    }
                    else
                    {
                        if (board[(char)(file - i), rank + i] < 0)
                        {
                            break;
                        }
                        else if (board[(char)(file - i), rank + i] > 0)
                        {
                            moves.Add(new Square((char)(file - i), rank + i));
                            break;
                        }
                        else
                        {
                            moves.Add(new Square((char)(file - i), rank + i));
                        }
                    }
                }
                //lower-left
                for (int i = 1; i < 8; i++)
                {
                    if ((file - i) < 'a' || rank - i < 1)
                    {
                        break;
                    }
                    else
                    {
                        if (board[(char)(file - i), rank - i] < 0)
                        {
                            break;
                        }
                        else if (board[(char)(file - i), rank - i] > 0)
                        {
                            moves.Add(new Square((char)(file - i), rank - i));
                            break;
                        }
                        else
                        {
                            moves.Add(new Square((char)(file - i), rank - i));
                        }
                    }
                }
                #endregion
                foreach (Square move in moves)
                {
                    tempboard = board.ShallowCopy();
                    tempboard[file, rank] = 0;
                    tempboard[move.file, move.rank] = usedpiece;
                    if (BlackKing.IsSafe(tempboard))
                    {
                        result.Add(tempboard);
                    }
                }
                return result;
            }
        }

        public class BlackRook
        {

            public static List<ChessBoard> GetPossiblePositions(ChessBoard board, char file, sbyte rank)
            {
                List<ChessBoard> result = new List<ChessBoard>();
                ChessBoard tempboard;
                sbyte usedpiece = (sbyte)DefaultPieces.BlackRook;
                List<Square> moves = new List<Square>();
                #region RookMoveCycles
                //Up
                for (int i = rank + 1; i <= 8; i++)
                {
                    if (board[file, i] < 0)
                    {
                        break;
                    }
                    else
                    {
                        if (board[file, i] > 0)
                        {
                            moves.Add(new Square(file, i));
                            break;
                        }
                        else
                        {
                            moves.Add(new Square(file, i));
                        }
                    }
                }
                //Down
                for (int i = rank - 1; i >= 1; i--)
                {
                    if (board[file, i] < 0)
                    {
                        break;
                    }
                    else
                    {
                        if (board[file, i] > 0)
                        {
                            moves.Add(new Square(file, i));
                            break;
                        }
                        else
                        {
                            moves.Add(new Square(file, i));
                        }
                    }
                }
                //Right
                for (char tchar = (char)(file + 1); tchar <= 'h'; tchar++)
                {
                    if (board[tchar, rank] < 0)
                    {
                        break;
                    }
                    else
                    {
                        if (board[tchar, rank] > 0)
                        {
                            moves.Add(new Square(tchar, rank));
                            break;
                        }
                        else
                        {
                            moves.Add(new Square(tchar, rank));
                        }
                    }
                }
                //Left
                for (char tchar = (char)(file - 1); tchar >= 'a'; tchar--)
                {
                    if (board[tchar, rank] < 0)
                    {
                        break;
                    }
                    else
                    {
                        if (board[tchar, rank] > 0)
                        {
                            moves.Add(new Square(tchar, rank));
                            break;
                        }
                        else
                        {
                            moves.Add(new Square(tchar, rank));
                        }
                    }
                }
                #endregion
                foreach (Square move in moves)
                {
                    tempboard = board.ShallowCopy();
                    tempboard[file, rank] = 0;
                    tempboard[move.file, move.rank] = usedpiece;
                    if (BlackKing.IsSafe(tempboard))
                    {
                        result.Add(tempboard);
                    }
                }
                return result;
            }
        }

        public class BlackQueen
        {

            public static List<ChessBoard> GetPossiblePositions(ChessBoard board, char file, sbyte rank)
            {
                List<ChessBoard> result = new List<ChessBoard>();
                ChessBoard tempboard;
                sbyte usedpiece = (sbyte)DefaultPieces.BlackQueen;
                List<Square> moves = new List<Square>();
                #region QueenMoveCycles
                //upper-right
                for (int i = 1; i < 8; i++)
                {
                    if ((file + i) > 'h' || rank + i > 8)
                    {
                        break;
                    }
                    else
                    {
                        if (board[(char)(file + i), rank + i] < 0)
                        {
                            break;
                        }
                        else if (board[(char)(file + i), rank + i] > 0)
                        {
                            moves.Add(new Square((char)(file + i), rank + i));
                            break;
                        }
                        else
                        {
                            moves.Add(new Square((char)(file + i), rank + i));
                        }
                    }
                }
                //lower-right
                for (int i = 1; i < 8; i++)
                {
                    if ((file + i) > 'h' || rank - i < 1)
                    {
                        break;
                    }
                    else
                    {
                        if (board[(char)(file + i), rank - i] < 0)
                        {
                            break;
                        }
                        else if (board[(char)(file + i), rank - i] > 0)
                        {
                            moves.Add(new Square((char)(file + i), rank - i));
                            break;
                        }
                        else
                        {
                            moves.Add(new Square((char)(file + i), rank + -i));
                        }
                    }
                }
                //upper-left
                for (int i = 1; i < 8; i++)
                {
                    if ((file - i) < 'a' || rank + i > 8)
                    {
                        break;
                    }
                    else
                    {
                        if (board[(char)(file - i), rank + i] < 0)
                        {
                            break;
                        }
                        else if (board[(char)(file - i), rank + i] > 0)
                        {
                            moves.Add(new Square((char)(file - i), rank + i));
                            break;
                        }
                        else
                        {
                            moves.Add(new Square((char)(file - i), rank + i));
                        }
                    }
                }
                //lower-left
                for (int i = 1; i < 8; i++)
                {
                    if ((file - i) < 'a' || rank - i < 1)
                    {
                        break;
                    }
                    else
                    {
                        if (board[(char)(file - i), rank - i] < 0)
                        {
                            break;
                        }
                        else if (board[(char)(file - i), rank - i] > 0)
                        {
                            moves.Add(new Square((char)(file - i), rank - i));
                            break;
                        }
                        else
                        {
                            moves.Add(new Square((char)(file - i), rank - i));
                        }
                    }
                }
                //Up
                for (int i = rank + 1; i <= 8; i++)
                {
                    if (board[file, i] < 0)
                    {
                        break;
                    }
                    else
                    {
                        if (board[file, i] > 0)
                        {
                            moves.Add(new Square(file, i));
                            break;
                        }
                        else
                        {
                            moves.Add(new Square(file, i));
                        }
                    }
                }
                //Down
                for (int i = rank - 1; i >= 1; i--)
                {
                    if (board[file, i] < 0)
                    {
                        break;
                    }
                    else
                    {
                        if (board[file, i] > 0)
                        {
                            moves.Add(new Square(file, i));
                            break;
                        }
                        else
                        {
                            moves.Add(new Square(file, i));
                        }
                    }
                }
                //Right
                for (char tchar = (char)(file + 1); tchar <= 'h'; tchar++)
                {
                    if (board[tchar, rank] < 0)
                    {
                        break;
                    }
                    else
                    {
                        if (board[tchar, rank] > 0)
                        {
                            moves.Add(new Square(tchar, rank));
                            break;
                        }
                        else
                        {
                            moves.Add(new Square(tchar, rank));
                        }
                    }
                }
                //Left
                for (char tchar = (char)(file - 1); tchar >= 'a'; tchar--)
                {
                    if (board[tchar, rank] < 0)
                    {
                        break;
                    }
                    else
                    {
                        if (board[tchar, rank] > 0)
                        {
                            moves.Add(new Square(tchar, rank));
                            break;
                        }
                        else
                        {
                            moves.Add(new Square(tchar, rank));
                        }
                    }
                }

                #endregion

                foreach (Square move in moves)
                {
                    tempboard = board.ShallowCopy();
                    tempboard[file, rank] = 0;
                    tempboard[move.file, move.rank] = usedpiece;
                    if (BlackKing.IsSafe(tempboard))
                    {
                        result.Add(tempboard);
                    }
                }
                return result;
            }
        }

        public class BlackKing
        {
            public static bool IsSafe(ChessBoard board)
            {
                //get Black King coordinates
                char file = 'a'; sbyte rank = 1;
                for (char tfile = 'a'; tfile <= 'h'; tfile++)
                {
                    for (int trank = 1; trank <= 8; trank++)
                    {
                        if (board[tfile, trank] == (sbyte)DefaultPieces.BlackKing)
                        {
                            file = tfile;
                            rank = (sbyte)trank;
                            break;
                        }
                    }
                }
                //check if chessboard is ok
                if (board[file, rank] != (sbyte)DefaultPieces.BlackKing)
                {
                    return false;
                }
                //check for enemy Pawn
                #region CheckPawn
                if (file == 'a' && rank > 1 && rank <= 8)
                {
                    if (board['b', rank - 1] == 1)
                    {
                        return false;
                    }
                }
                if (file == 'h' && rank > 1 && rank <= 8)
                {
                    if (board['g', rank - 1] == 1)
                    {
                        return false;
                    }
                }
                for (char tfile = 'b'; tfile <= 'g'; tfile++)
                {
                    if (rank > 1 && rank <= 8)
                    {
                        if (board[(char)(tfile + 1), rank - 1] == 1 || board[(char)(tfile - 1), rank - 1] == 1)
                        {
                            return false;
                        }
                    }
                }
                #endregion
                //check for enemy kNight
                #region CheckkNight
                Square[] moves_array = new Square[8];
                moves_array[0] = new Square((char)(file + 1), rank + 2);
                moves_array[1] = new Square((char)(file + 2), rank + 1);
                moves_array[2] = new Square((char)(file + 2), rank - 1);
                moves_array[3] = new Square((char)(file + 1), rank - 2);
                moves_array[4] = new Square((char)(file - 1), rank + 2);
                moves_array[5] = new Square((char)(file - 2), rank + 1);
                moves_array[6] = new Square((char)(file - 2), rank - 1);
                moves_array[7] = new Square((char)(file - 1), rank - 2);
                foreach (Square move in moves_array)
                {
                    if (move.file >= 'a' && move.file <= 'h' && move.rank >= 1 && move.rank <= 8)
                    {
                        if (board[move.file, move.rank] == 2)
                        {
                            return false;
                        }
                    }
                }
                #endregion
                //for enemy Rook or Queen (horizontal)
                #region HorizontalCheck
                //Up
                for (int i = rank + 1; i <= 8; i++)
                {
                    if (board[file, i] < 0)
                    {
                        break;
                    }
                    else if (board[file, i] == 4 || board[file, i] == 5)
                    {
                        return false;
                    }

                }
                //Down
                for (int i = rank - 1; i >= 1; i--)
                {
                    if (board[file, i] < 0)
                    {
                        break;
                    }
                    else if (board[file, i] == 4 || board[file, i] == 5)
                    {
                        return false;
                    }
                }
                //Right
                for (char tchar = (char)(file + 1); tchar <= 'h'; tchar++)
                {
                    if (board[tchar, rank] < 0)
                    {
                        break;
                    }
                    else if (board[tchar, rank] == 4 || board[tchar, rank] == 5)
                    {
                        return false;
                    }

                }
                //Left
                for (char tchar = (char)(file - 1); tchar >= 'a'; tchar--)
                {
                    if (board[tchar, rank] > 0)
                    {
                        break;
                    }
                    else if (board[tchar, rank] == 4 || board[tchar, rank] == 5)
                    {
                        return false;
                    }
                }
                #endregion
                //for enemy Bishop or Queen (diagonal)
                #region DiagonalCheck
                //upper-right
                for (int i = 1; i < 8; i++)
                {
                    if ((file + i) > 'h' || rank + i > 8)
                    {
                        break;
                    }
                    else
                    {
                        if (board[(char)(file + i), rank + i] < 0)
                        {
                            break;
                        }
                        else if (board[(char)(file + i), rank + i] == 3 || board[(char)(file + i), rank + i] == 5)
                        {
                            return false;
                        }
                    }
                }
                //lower-right
                for (int i = 1; i < 8; i++)
                {
                    if ((file + i) > 'h' || rank - i < 1)
                    {
                        break;
                    }
                    else
                    {
                        if (board[(char)(file + i), rank - i] < 0)
                        {
                            break;
                        }
                        else if (board[(char)(file + i), rank - i] == 3 || board[(char)(file + i), rank - i] == 5)
                        {
                            return false;
                        }
                    }
                }
                //upper-left
                for (int i = 1; i < 8; i++)
                {
                    if ((file - i) < 'a' || rank + i > 8)
                    {
                        break;
                    }
                    else
                    {
                        if (board[(char)(file - i), rank + i] < 0)
                        {
                            break;
                        }
                        else if (board[(char)(file - i), rank + i] == 3 || board[(char)(file - i), rank + i] == 5)
                        {
                            return false;
                        }
                    }
                }
                //lower-left
                for (int i = 1; i < 8; i++)
                {
                    if ((file - i) < 'a' || rank - i < 1)
                    {
                        break;
                    }
                    else
                    {
                        if (board[(char)(file - i), rank - i] < 0)
                        {
                            break;
                        }
                        else if (board[(char)(file - i), rank - i] == 3 || board[(char)(file - i), rank - i] == 5)
                        {
                            return false;
                        }
                    }
                }
                #endregion

                return true;
            }

            public static List<ChessBoard> GetPossiblePositions(ChessBoard board, char file, sbyte rank)
            {
                List<ChessBoard> result = new List<ChessBoard>();
                ChessBoard tempboard;
                sbyte usedpiece = (sbyte)DefaultPieces.BlackKing;
                Square[] moves = new Square[8];
                moves[0] = new Square(file, rank + 1);
                moves[1] = new Square(file, rank - 1);
                moves[2] = new Square((char)(file + 1), rank + 1);
                moves[3] = new Square((char)(file + 1), rank);
                moves[4] = new Square((char)(file + 1), rank - 1);
                moves[5] = new Square((char)(file - 1), rank + 1);
                moves[6] = new Square((char)(file - 1), rank);
                moves[7] = new Square((char)(file - 1), rank - 1);
                foreach (Square move in moves)
                {
                    if (move.file >= 'a' && move.file <= 'h' && move.rank >= 1 && move.rank <= 8)
                    {
                        if (board[move.file, move.rank] <= 0)
                        {
                            tempboard = board.ShallowCopy();
                            tempboard[file, rank] = 0;
                            tempboard[move.file, move.rank] = usedpiece;
                            if (BlackKing.IsSafe(tempboard))
                            {
                                result.Add(tempboard);
                            }
                        }
                    }
                }
                //check Castling
                #region Castling0-0-0
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
                        if (board[tfile, rank] != 0 && board[tfile, rank] != usedpiece && board[tfile, rank] != (sbyte)DefaultPieces.BlackRook)
                        {
                            CastlingAvailable = false;
                            break;
                        }
                    }
                    for (char tfile = rookfile; tfile <= 'd'; tfile++)
                    {
                        if (board[tfile, rank] != 0 && board[tfile, rank] != usedpiece && board[tfile, rank] != (sbyte)DefaultPieces.BlackRook)
                        {
                            CastlingAvailable = false;
                            break;
                        }
                    }
                    for (char tfile = rookfile; tfile >= 'd'; tfile--)
                    {
                        if (board[tfile, rank] != 0 && board[tfile, rank] != usedpiece && board[tfile, rank] != (sbyte)DefaultPieces.BlackRook)
                        {
                            CastlingAvailable = false;
                            break;
                        }
                    }
                    for (char tfile = file; tfile >= 'c'; tfile--)
                    {

                        ChessBoard temp2board = board.ShallowCopy();
                        temp2board[file, rank] = 0;
                        temp2board[tfile, rank] = usedpiece;
                        if (!BlackKing.IsSafe(temp2board))
                        {
                            CastlingAvailable = false;
                            break;
                        }
                    }
                    if (file == 'b')
                    {
                        ChessBoard temp2board = board.ShallowCopy();
                        temp2board[file, rank] = 0;
                        temp2board['c', rank] = usedpiece;
                        if (!BlackKing.IsSafe(temp2board))
                        {
                            CastlingAvailable = false;
                        }
                    }
                    if (CastlingAvailable)
                    {
                        tempboard[file, rank] = 0;
                        tempboard['c', rank] = usedpiece;
                        tempboard[rookfile, rank] = 0;
                        tempboard['d', rank] = (sbyte)DefaultPieces.BlackRook;
                        result.Add(tempboard);

                    }
                }
                #endregion

                #region Castling0-0
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
                        if (board[tfile, rank] != 0 && board[tfile, rank] != usedpiece && board[tfile, rank] != (sbyte)DefaultPieces.BlackRook)
                        {
                            CastlingAvailable = false;
                            break;
                        }
                    }
                    for (char tfile = rookfile; tfile >= 'f'; tfile--)
                    {
                        if (board[tfile, rank] != 0 && board[tfile, rank] != usedpiece && board[tfile, rank] != (sbyte)DefaultPieces.BlackRook)
                        {
                            CastlingAvailable = false;
                            break;
                        }
                    }
                    for (char tfile = rookfile; tfile <= 'f'; tfile++)
                    {
                        if (board[tfile, rank] != 0 && board[tfile, rank] != usedpiece && board[tfile, rank] != (sbyte)DefaultPieces.BlackRook)
                        {
                            CastlingAvailable = false;
                            break;
                        }
                    }

                    for (char tfile = file; tfile <= 'g'; tfile++)
                    {

                        ChessBoard temp2board = board.ShallowCopy();
                        temp2board[file, rank] = 0;
                        temp2board[tfile, rank] = usedpiece;
                        if (!BlackKing.IsSafe(temp2board))
                        {
                            CastlingAvailable = false;
                            break;
                        }
                    }


                    if (CastlingAvailable)
                    {
                        tempboard[file, rank] = 0;
                        tempboard['g', rank] = usedpiece;
                        tempboard[rookfile, rank] = 0;
                        tempboard['f', rank] = (sbyte)DefaultPieces.BlackRook;
                        result.Add(tempboard);
                    }
                }
                #endregion
                return result;
            }
        }
    }

#endregion
