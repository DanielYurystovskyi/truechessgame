using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueChessGame.GameEngine
{
    /*			
			Review VV:
	            1) рекомендую реалізувати функції цього класу як нестатичні
                2) слід розділити ігрову логіку і шахову нотацію на різні нестатичні класи
	*/
    public class FIDEnotation
    {
        public delegate List<ChessBoard> GetPiecePositionsType(ChessBoard board, char file, sbyte rank);
        /*			
			Review VV:
			    таку залежність краще реалізувати як Dictionary
		*/

        public static char GetLetter(sbyte piece)
        {
            switch (piece)
            {
                case (sbyte)DefaultPieces.WhitePawn:
                    return 'P';
                case (sbyte)DefaultPieces.WhitekNight:
                    return 'N';
                case (sbyte)DefaultPieces.WhiteBishop:
                    return 'B';
                case (sbyte)DefaultPieces.WhiteRook:
                    return 'R';
                case (sbyte)DefaultPieces.WhiteQueen:
                    return 'Q';
                case (sbyte)DefaultPieces.WhiteKing:
                    return 'K';
                case (sbyte)DefaultPieces.BlackPawn:
                    return 'P';
                case (sbyte)DefaultPieces.BlackkNight:
                    return 'N';
                case (sbyte)DefaultPieces.BlackBishop:
                    return 'B';
                case (sbyte)DefaultPieces.BlackRook:
                    return 'R';
                case (sbyte)DefaultPieces.BlackQueen:
                    return 'Q';
                case (sbyte)DefaultPieces.BlackKing:
                    return 'K';
                default:
                    return ' ';
            }
        }
        /*			
			Review VV:
			    таку залежність краще реалізувати як Dictionary
		*/
        public static GetPiecePositionsType GetWhitePiecePositionsType(char letter)
        {
            GetPiecePositionsType result;
            switch (letter)
            {
                case 'K':
                    result = WhiteKing.GetPossiblePositions;
                    break;
                case 'Q':
                    result = WhiteQueen.GetPossiblePositions;
                    break;
                case 'R':
                    result = WhiteRook.GetPossiblePositions;
                    break;
                case 'B':
                    result = WhiteBishop.GetPossiblePositions;
                    break;
                case 'N':
                    result = WhitekNight.GetPossiblePositions;
                    break;
                case 'P':
                    result = WhitePawn.GetPossiblePositions;
                    break;
                default:
                    throw new ArgumentException("wrong notation");
            }
            return result;
        }
        /*			
			Review VV:
			    таку залежність краще реалізувати як Dictionary
		*/
        public static GetPiecePositionsType GetBlackPiecePositionsType(char letter)
        {
            GetPiecePositionsType result;
            switch (letter)
            {
                case 'K':
                    result = BlackKing.GetPossiblePositions;
                    break;
                case 'Q':
                    result = BlackQueen.GetPossiblePositions;
                    break;
                case 'R':
                    result = BlackRook.GetPossiblePositions;
                    break;
                case 'B':
                    result = BlackBishop.GetPossiblePositions;
                    break;
                case 'N':
                    result = BlackkNight.GetPossiblePositions;
                    break;
                case 'P':
                    result = BlackPawn.GetPossiblePositions;
                    break;
                default:
                    throw new ArgumentException("wrong notation");
            }
            return result;
        }
        /*			
			Review VV:
			    таку залежність краще реалізувати як Dictionary
		*/
        public static sbyte GetSbyteFromWhitePieceLetter(char letter)
        {
            switch (letter)
            {
                case 'K':
                    return (sbyte)DefaultPieces.WhiteKing;
                case 'Q':
                    return (sbyte)DefaultPieces.WhiteQueen;
                case 'R':
                    return (sbyte)DefaultPieces.WhiteRook;
                case 'B':
                    return (sbyte)DefaultPieces.WhiteBishop;
                case 'N':
                    return (sbyte)DefaultPieces.WhitekNight;
                case 'P':
                    return (sbyte)DefaultPieces.WhitePawn;
                default:
                    throw new ArgumentException("wrong notation");
            }
        }

        public static sbyte GetSbyteFromBlackPieceLetter(char letter)
        {
            return (sbyte)(-1 * GetSbyteFromWhitePieceLetter(letter));
        }
        
        // Code Review: Аргументи методу повинні починатися з малої літери.
        public static bool CheckIfArePossibleMoves(ChessBoard board, bool isWhite)
        {
            // Code Review: Назва локальної змінної повинна починатися з малої літери.
           // int comparerresult = isWhite ? 1 : -1;
            List<Square> pieces = new List<Square>();
            //Console.WriteLine(comparerresult);
            for (char tfile = 'a'; tfile <= 'h'; tfile++)
            {
                for (sbyte trank = 1; trank <= 8; trank++)
                {
                    if ( (isWhite && board[tfile, trank]>0) || (!isWhite && board[tfile, trank]<0))
                    {
                        pieces.Add(new Square(tfile, trank));
                        //Console.Write(board[tfile, trank].ToString() + " ");
                    }
                }
            }
            foreach (Square temp in pieces)
            {
                char piecetype = GetLetter(board[temp]);
                GetPiecePositionsType function = isWhite ? GetWhitePiecePositionsType(piecetype) : GetBlackPiecePositionsType(piecetype);
                if (function(board, temp._file, temp._rank).Count > 0)
                {
                    return true;
                }
            }
            return false;
        }
        
        // Code Review: Надто об'ємний метод.
        // Code Review: Аргументи методу повинні починатися з малої літери.
        private static ChessBoard PerformCastling(ChessBoard board, bool isWhite, bool isKingCastling, out char kingfile)
        {
            ChessBoard tempboard = board.DeepCopy();
            sbyte castlingrank;
            sbyte kingsbyte;
            sbyte rooksbyte;
            char rookfile;
            if (isWhite)
            {
                castlingrank = 1;
                kingsbyte = (sbyte)DefaultPieces.WhiteKing;
                rooksbyte = (sbyte)DefaultPieces.WhiteRook;
            }
            else
            {
                castlingrank = 8;
                kingsbyte = (sbyte)DefaultPieces.BlackKing;
                rooksbyte = (sbyte)DefaultPieces.BlackRook;
            }
            kingfile = Piece.GetPosition(board, kingsbyte)._file;
            if (isKingCastling)
            {
                rookfile = 'h';
                for (char tfile = 'h'; tfile >= 'c'; tfile--)
                {
                    if (board[tfile, castlingrank] == rooksbyte)
                    {
                        rookfile = tfile;
                        break;
                    }
                }
                tempboard = Piece.PerformMove(tempboard, new Square(kingfile, castlingrank), new Square('g', castlingrank));
                tempboard = Piece.PerformMove(tempboard, new Square(rookfile, castlingrank), new Square('f', castlingrank));
            }
            else
            {
                rookfile = 'a';
                for (char tfile = 'a'; tfile <= 'f'; tfile++)
                {
                    if (board[tfile, castlingrank] == rooksbyte)
                    {
                        rookfile = tfile;
                        break;
                    }
                }
                tempboard = Piece.PerformMove(tempboard, new Square(kingfile, castlingrank), new Square('c', castlingrank));
                tempboard = Piece.PerformMove(tempboard, new Square(rookfile, castlingrank), new Square('d', castlingrank));
            }
            return tempboard;
        }

        public static ChessBoard PerformWhiteMove(ChessBoard board, string notation)
        {
            ChessBoard tempboard = board.DeepCopy();
            if (notation == "0-0" && DefaultInfo.WhiteKingIsUnMoved && DefaultInfo.WhiteHsideRookIsUnMoved)
            {
                return PerformWhiteKingCastling(board, ref tempboard);
            }
            else if (notation == "0-0-0" && DefaultInfo.WhiteKingIsUnMoved && DefaultInfo.WhiteAsideRookIsUnMoved)
            {
                return PerformWhiteQueenCastling(board, ref tempboard);
            }
            else if (notation.Length > 0 && notation[0] <= 'h' && notation[0] >= 'a')
            {
                if ((notation.Length == 2 && notation[1] <= '7' && notation[1] >= '3') || (notation.Length == 3 && notation[1] == '8' && notation[2] >= 'A' && notation[2] <= 'Z'))
                {
                    return PerformWhitePawnMove(board, notation, tempboard);
                }
                //ed4 
                else if (notation.Length >= 3 && notation.Length <= 4 && notation[1] <= 'h' && notation[1] >= 'a' && notation[2] <= '8' && notation[2] >= '3')
                {
                    return PerformWhitePawnCapture(board, notation, tempboard);
                }
                else
                {
                    throw new ArgumentException("Wrong move");
                }
            }
            else if (notation.Length > 0 && notation[0] <= 'Z' && notation[0] >= 'A')
            {
                //Ne4 
                if (notation.Length == 3 && notation[1] <= 'h' && notation[1] >= 'a' && notation[2] >= '0' && notation[2] <= '8')
                {
                    return PerformWhiteNonAmbiguousMove(board, notation, tempboard);
                }
                //Nbd2 Nb5d2
                else if (notation.Length <= 5 && notation.Length >= 4 && notation[2] != 'x')
                {
                    return PerformWhiteAmbiguousMove(board, notation, tempboard);
                }
                else
                {
                    throw new ArgumentException("wrong notation");
                }
            }
            else
            {
                throw new ArgumentException("Wrong notation");
            }
        }
        
        // Code Review: Надто об'ємний метод.
        // Code Review: Потрібно розділяти умови дужками () в операторах if.
        private static ChessBoard PerformWhiteAmbiguousMove(ChessBoard board, string notation, ChessBoard tempboard)
        {
            char piecefile = default(char);
            sbyte piecerank = default(sbyte);
            char goalfile;
            sbyte goalrank;
            sbyte piecetype = GetSbyteFromWhitePieceLetter(notation[0]);
            //Ngf3
            if (notation.Length == 4 && notation[1] >= 'a' && notation[1] <= 'h' && notation[2] >= 'a' && notation[2] <= 'h' && notation[3] >= '1' && notation[3] <= '8')
            {
                goalfile = notation[2];
                goalrank = (sbyte)Char.GetNumericValue(notation[3]);
                piecefile = notation[1];
                for (sbyte i = 1; i <= 8; i++)
                {
                    if (board[piecefile, i] == piecetype)
                    {
                        piecerank = i;
                        break;
                    }
                }
            }
            //N5f3
            else if (notation.Length == 4 && notation[1] >= '1' && notation[1] <= '8' && notation[2] >= 'a' && notation[2] <= 'h' && notation[3] >= '1' && notation[3] <= '8')
            {
                goalfile = notation[2];
                goalrank = (sbyte)Char.GetNumericValue(notation[3]);
                piecerank = (sbyte)Char.GetNumericValue(notation[1]);
                for (char tfile = 'a'; tfile <= 'h'; tfile++)
                {
                    if (board[tfile, piecerank] == piecetype)
                    {
                        piecefile = tfile;
                        break;
                    }
                }
            }
            //Ng5f3
            else if (notation.Length == 5 && notation[1] >= 'a' && notation[1] <= 'h' && notation[2] >= '1' && notation[2] <= '8' && notation[3] >= 'a' && notation[3] <= 'h' && notation[4] >= '1' && notation[4] <= '8')
            {
                goalfile = notation[3];
                goalrank = (sbyte)Char.GetNumericValue(notation[4]);
                piecefile = notation[1];
                piecerank = (sbyte)Char.GetNumericValue(notation[2]);
            }
            else
            {
                throw new ArgumentException("wrong notation");
            }

            tempboard[goalfile, goalrank] = piecetype;
            tempboard[piecefile, piecerank] = 0;
            GetPiecePositionsType piecefunction = GetWhitePiecePositionsType(notation[0]);
            if (piecefunction(board, piecefile, piecerank).Contains(tempboard))
            {
                if (board[piecefile, piecerank] == (sbyte)DefaultPieces.WhiteRook && (DefaultInfo.WhiteAsideRookIsUnMoved || DefaultInfo.WhiteHsideRookIsUnMoved))
                {
                    if (DefaultInfo.WhiteAsideRookIsUnMoved && !DefaultInfo.WhiteHsideRookIsUnMoved)
                    {
                        DefaultInfo.WhiteAsideRookIsUnMoved = false;
                    }
                    else if (!DefaultInfo.WhiteAsideRookIsUnMoved && DefaultInfo.WhiteHsideRookIsUnMoved)
                    {
                        DefaultInfo.WhiteHsideRookIsUnMoved = false;
                    }
                    else
                    {
                        char rookfile;
                        for (rookfile = 'a'; rookfile <= 'h'; rookfile++)
                        {
                            if (board[rookfile, 1] == (sbyte)DefaultPieces.WhiteRook)
                            {

                                break;
                            }
                        }
                        if (rookfile < piecefile)
                        {
                            DefaultInfo.WhiteHsideRookIsUnMoved = false;
                        }
                        else
                        {
                            DefaultInfo.WhiteAsideRookIsUnMoved = false;
                        }
                    }
                }
                return tempboard;
            }
            else
            {
                throw new ArgumentException("wrong move");
            }
        }

        private static ChessBoard PerformWhiteNonAmbiguousMove(ChessBoard board, string notation, ChessBoard tempboard)
        {
            char goalfile = notation[1];
            sbyte goalrank = (sbyte)Char.GetNumericValue(notation[2]);

            //Check if piece is not-ambigious and get piece square
            List<Square> PossibleMovers = BlackPiece.GetPossibleWhiteAttackersToSquare(board, new Square(goalfile, goalrank));
            Console.WriteLine(PossibleMovers.Count);
            int NumberOfSamePieces = 0;
            Square PieceSquare = new Square();
            foreach (Square tempsquare in PossibleMovers)
            {
                if (board[tempsquare] == GetSbyteFromWhitePieceLetter(notation[0]))
                {
                    NumberOfSamePieces++;
                    PieceSquare = tempsquare;
                }
            }
            if (NumberOfSamePieces != 1)
            {
                // Console.WriteLine(NumberOfSamePieces);
                throw new ArgumentException("wrong notation");
            }
            tempboard[PieceSquare._file, PieceSquare._rank] = 0;
            tempboard[goalfile, goalrank] = GetSbyteFromWhitePieceLetter(notation[0]);
            GetPiecePositionsType piecefunction = GetWhitePiecePositionsType(notation[0]);
            //Console.WriteLine(notation[0]);

            if (piecefunction(board, PieceSquare._file, PieceSquare._rank).Contains(tempboard))
            {
                if (board[PieceSquare._file, PieceSquare._rank] == (sbyte)DefaultPieces.WhiteRook && (DefaultInfo.WhiteAsideRookIsUnMoved || DefaultInfo.WhiteHsideRookIsUnMoved))
                {
                    if (DefaultInfo.WhiteAsideRookIsUnMoved && !DefaultInfo.WhiteHsideRookIsUnMoved)
                    {
                        DefaultInfo.WhiteAsideRookIsUnMoved = false;
                    }
                    else if (!DefaultInfo.WhiteAsideRookIsUnMoved && DefaultInfo.WhiteHsideRookIsUnMoved)
                    {
                        DefaultInfo.WhiteHsideRookIsUnMoved = false;
                    }
                    else
                    {
                        char rookfile;
                        for (rookfile = 'a'; rookfile <= 'h'; rookfile++)
                        {
                            if (board[rookfile, 1] == (sbyte)DefaultPieces.WhiteRook)
                            {

                                break;
                            }
                        }
                        if (rookfile < PieceSquare._file)
                        {
                            DefaultInfo.WhiteHsideRookIsUnMoved = false;
                        }
                        else
                        {
                            DefaultInfo.WhiteAsideRookIsUnMoved = false;
                        }
                    }
                }
                return tempboard;
            }
            else
            {
                throw new ArgumentException("wrong move");
            }
        }

        private static ChessBoard PerformWhitePawnCapture(ChessBoard board, string notation, ChessBoard tempboard)
        {
            char pawnfile = notation[0];
            char enemyfile = notation[1];
            sbyte enemyrank = (sbyte)Char.GetNumericValue(notation[2]);
            sbyte pawnrank = (sbyte)(enemyrank - 1);
            if (board[pawnfile, pawnrank] != 1)
            {
                throw new ArgumentException("wrong notation");
            }
            tempboard[pawnfile, pawnrank] = 0;

            if (DefaultInfo.BlackEnPassantEndangered && enemyfile == DefaultInfo.EnPassantPossibleCapture._file && enemyrank == DefaultInfo.EnPassantPossibleCapture._rank)
            {
                tempboard[enemyfile, enemyrank - 1] = 0;
            }
            if (notation.Length == 3)
            {
                tempboard[enemyfile, enemyrank] = (sbyte)DefaultPieces.WhitePawn;
            }
            else
            {
                tempboard[enemyfile, enemyrank] = GetSbyteFromWhitePieceLetter(notation[3]);
            }
            if (WhitePawn.GetPossiblePositions(board, pawnfile, pawnrank).Contains(tempboard))
            {
                return tempboard;
            }
            else
            {
                throw new ArgumentException("Wrong move");
            }
        }

        private static ChessBoard PerformWhitePawnMove(ChessBoard board, string notation, ChessBoard tempboard)
        {
            //simple move (not 4 _rank) and promotion
            if (notation[1] != '4')
            {
                sbyte pawnrank = (sbyte)(Char.GetNumericValue(notation[1]) - 1);
                char pawnfile = notation[0];
                if (board[pawnfile, pawnrank] != (sbyte)DefaultPieces.WhitePawn)
                {
                    throw new ArgumentException("wrong move");
                }
                tempboard[pawnfile, pawnrank] = 0;
                if (notation.Length == 2)
                {
                    tempboard[pawnfile, pawnrank + 1] = (sbyte)DefaultPieces.WhitePawn;
                }
                else
                {
                    tempboard[pawnfile, pawnrank + 1] = GetSbyteFromWhitePieceLetter(notation[2]);
                }
                if (WhitePawn.GetPossiblePositions(board, pawnfile, pawnrank).Contains(tempboard))
                {
                    return tempboard;
                }
                else
                {
                    throw new ArgumentException("Wrong move");
                }
            }
            //simple move (4 _rank)
            else
            {
                char pawnfile = notation[0];
                sbyte pawnrank;
                if (board[pawnfile, (sbyte)(Char.GetNumericValue(notation[1]) - 1)] == (sbyte)DefaultPieces.WhitePawn)
                {
                    pawnrank = (sbyte)(Char.GetNumericValue(notation[1]) - 1);
                }
                else if (board[pawnfile, (sbyte)(Char.GetNumericValue(notation[1]) - 2)] == (sbyte)DefaultPieces.WhitePawn && board[pawnfile, (sbyte)(Char.GetNumericValue(notation[1]) - 1)] == 0)
                {
                    pawnrank = (sbyte)(Char.GetNumericValue(notation[1]) - 2);
                }
                else
                {
                    throw new ArgumentException("Wrong move");
                }
                tempboard[pawnfile, pawnrank] = 0;
                tempboard[pawnfile, (sbyte)Char.GetNumericValue(notation[1])] = (sbyte)DefaultPieces.WhitePawn;
                if (WhitePawn.GetPossiblePositions(board, pawnfile, pawnrank).Contains(tempboard))
                {
                    //en passant
                    if (pawnrank == (sbyte)(Char.GetNumericValue(notation[1]) - 2))
                    {
                        DefaultInfo.EnPassantPossibleCapture = new Square(notation[0], (sbyte)(Char.GetNumericValue(notation[1]) - 1));
                        DefaultInfo.WhiteEnPassantEndangered = true;
                    }
                    return tempboard;
                }
                else
                {
                    throw new ArgumentException("Wrong move");
                }
            }
        }

        private static ChessBoard PerformWhiteQueenCastling(ChessBoard board, ref ChessBoard tempboard)
        {
            char kingfile;
            tempboard = PerformCastling(board, true, false, out kingfile);
            if (WhiteKing.GetPossiblePositions(board, kingfile, 1).Contains(tempboard))
            {
                DefaultInfo.WhiteKingIsUnMoved = false;
                DefaultInfo.WhiteAsideRookIsUnMoved = false;
                return tempboard;
            }
            else
            {
                throw new ArgumentException("Wrong move");
            }
        }

        private static ChessBoard PerformWhiteKingCastling(ChessBoard board, ref ChessBoard tempboard)
        {
            char kingfile;
            tempboard = PerformCastling(board, true, true, out kingfile);
            if (WhiteKing.GetPossiblePositions(board, kingfile, 1).Contains(tempboard))
            {
                DefaultInfo.WhiteKingIsUnMoved = false;
                DefaultInfo.WhiteHsideRookIsUnMoved = false;
                return tempboard;
            }
            else
            {
                throw new ArgumentException("Wrong move");
            }
        }

        public static ChessBoard PerformBlackMove(ChessBoard board, string notation)

        {
            ChessBoard tempboard = board.DeepCopy();
            if (notation == "0-0" && DefaultInfo.BlackKingIsUnMoved && DefaultInfo.BlackHsideRookIsUnMoved)
            {
                return PerformBlackKingCastling(board, ref tempboard);
            }
            else if (notation == "0-0-0" && DefaultInfo.BlackKingIsUnMoved && DefaultInfo.BlackAsideRookIsUnMoved)
            {
                return PerformBlackQueenCastling(board, ref tempboard);
            }
            else if (notation.Length > 0 && notation[0] <= 'h' && notation[0] >= 'a')
            {
                //e5 e8Q
                if ((notation.Length == 2 && notation[1] <= '7' && notation[1] >= '2') || (notation.Length == 3 && notation[1] == '1' && notation[2] <= 'Z' && notation[2] >= 'A'))
                {
                    return PerformBlackPawnMove(board, notation, tempboard);
                }
                //ed4 ed1Q
                else if (notation.Length >= 3 && notation.Length <= 4 && notation[1] <= 'h' && notation[1] >= 'a' && notation[2] <= '8' && notation[2] >= '1')
                {
                    return PerformBlackPawnCapture(board, notation, tempboard);
                }
                else
                {
                    throw new ArgumentException("Wrong move");
                }
            }
            else if (notation.Length > 0 && notation[0] <= 'Z' && notation[0] >= 'A')
            {
                //Ne4 
                if (notation.Length == 3 && notation[1] <= 'h' && notation[1] >= 'a' && notation[2] >= '0' && notation[2] <= '8')
                {
                    return PerformBlackNonAmbiguousMove(board, notation, tempboard);
                }
                //Nbd2 Nb5d2
                else if (notation.Length <= 5 && notation.Length >= 4 && notation[2] != 'x')
                {
                    return PerformBlackAmbiguousMove(board, notation, tempboard);
                }
                else
                {
                    throw new ArgumentException("wrong notation");
                }
            }
            else
            {
                throw new ArgumentException("Wrong notation");
            }
        }

        // Code Review: Надто об'ємний метод.
        private static ChessBoard PerformBlackAmbiguousMove(ChessBoard board, string notation, ChessBoard tempboard)
        {
            char piecefile = default(char);
            sbyte piecerank = default(sbyte);
            char goalfile;
            sbyte goalrank;
            sbyte piecetype = GetSbyteFromBlackPieceLetter(notation[0]);
            //Ngf3
            if (notation.Length == 4 && notation[1] >= 'a' && notation[1] <= 'h' && notation[2] >= 'a' && notation[2] <= 'h' && notation[3] >= '1' && notation[3] <= '8')
            {
                goalfile = notation[2];
                goalrank = (sbyte)Char.GetNumericValue(notation[3]);
                piecefile = notation[1];
                for (sbyte i = 1; i <= 8; i++)
                {
                    if (board[piecefile, i] == piecetype)
                    {
                        piecerank = i;
                        break;
                    }
                }
            }
            //N5f3
            else if (notation.Length == 4 && notation[1] >= '1' && notation[1] <= '8' && notation[2] >= 'a' && notation[2] <= 'h' && notation[3] >= '1' && notation[3] <= '8')
            {
                goalfile = notation[2];
                goalrank = (sbyte)Char.GetNumericValue(notation[3]);
                piecerank = (sbyte)Char.GetNumericValue(notation[1]);
                for (char tfile = 'a'; tfile <= 'h'; tfile++)
                {
                    if (board[tfile, piecerank] == piecetype)
                    {
                        piecefile = tfile;
                        break;
                    }
                }
            }
            //Ng5f3
            else if (notation.Length == 5 && notation[1] >= 'a' && notation[1] <= 'h' && notation[2] >= '1' && notation[2] <= '8' && notation[3] >= 'a' && notation[3] <= 'h' && notation[4] >= '1' && notation[4] <= '8')
            {
                goalfile = notation[3];
                goalrank = (sbyte)Char.GetNumericValue(notation[4]);
                piecefile = notation[1];
                piecerank = (sbyte)Char.GetNumericValue(notation[2]);
            }
            else
            {
                throw new ArgumentException("wrong notation");
            }

            tempboard[goalfile, goalrank] = piecetype;
            tempboard[piecefile, piecerank] = 0;
            GetPiecePositionsType piecefunction = GetBlackPiecePositionsType(notation[0]);
            if (piecefunction(board, piecefile, piecerank).Contains(tempboard))
            {
                if (board[piecefile, piecerank] == (sbyte)DefaultPieces.BlackRook && (DefaultInfo.BlackAsideRookIsUnMoved || DefaultInfo.BlackHsideRookIsUnMoved))
                {
                    if (DefaultInfo.BlackAsideRookIsUnMoved && !DefaultInfo.BlackHsideRookIsUnMoved)
                    {
                        DefaultInfo.BlackAsideRookIsUnMoved = false;
                    }
                    else if (!DefaultInfo.BlackAsideRookIsUnMoved && DefaultInfo.BlackHsideRookIsUnMoved)
                    {
                        DefaultInfo.BlackHsideRookIsUnMoved = false;
                    }
                    else
                    {
                        char rookfile;
                        for (rookfile = 'a'; rookfile <= 'h'; rookfile++)
                        {
                            if (board[rookfile, 8] == (sbyte)DefaultPieces.BlackRook)
                            {

                                break;
                            }
                        }
                        if (rookfile < piecefile)
                        {
                            DefaultInfo.BlackHsideRookIsUnMoved = false;
                        }
                        else
                        {
                            DefaultInfo.BlackAsideRookIsUnMoved = false;
                        }
                    }
                }
                return tempboard;
            }
            else
            {
                throw new ArgumentException("wrong move");
            }
        }

        private static ChessBoard PerformBlackNonAmbiguousMove(ChessBoard board, string notation, ChessBoard tempboard)
        {
            char goalfile = notation[1];
            sbyte goalrank = (sbyte)Char.GetNumericValue(notation[2]);

            //Check if piece is not-ambigious and get piece square
            List<Square> PossibleMovers = WhitePiece.GetPossibleBlackAttackersToSquare(board, new Square(goalfile, goalrank));
            int NumberOfSamePieces = 0;
            Square PieceSquare = new Square();
            foreach (Square tempsquare in PossibleMovers)
            {
                if (board[tempsquare._file, tempsquare._rank] == GetSbyteFromBlackPieceLetter(notation[0]))
                {
                    NumberOfSamePieces++;
                    PieceSquare = tempsquare;
                }
            }
            if (NumberOfSamePieces != 1)
            {
                throw new ArgumentException("wrong notation");
            }
            tempboard[PieceSquare._file, PieceSquare._rank] = 0;
            tempboard[goalfile, goalrank] = GetSbyteFromBlackPieceLetter(notation[0]);
            GetPiecePositionsType piecefunction = GetBlackPiecePositionsType(notation[0]);
            if (piecefunction(board, PieceSquare._file, PieceSquare._rank).Contains(tempboard))
            {
                if (board[PieceSquare._file, PieceSquare._rank] == (sbyte)DefaultPieces.BlackRook && (DefaultInfo.BlackAsideRookIsUnMoved || DefaultInfo.BlackHsideRookIsUnMoved))
                {
                    if (DefaultInfo.BlackAsideRookIsUnMoved && !DefaultInfo.BlackHsideRookIsUnMoved)
                    {
                        DefaultInfo.BlackAsideRookIsUnMoved = false;
                    }
                    else if (!DefaultInfo.BlackAsideRookIsUnMoved && DefaultInfo.BlackHsideRookIsUnMoved)
                    {
                        DefaultInfo.BlackHsideRookIsUnMoved = false;
                    }
                    else
                    {
                        char rookfile;
                        for (rookfile = 'a'; rookfile <= 'h'; rookfile++)
                        {
                            if (board[rookfile, 8] == (sbyte)DefaultPieces.BlackRook)
                            {

                                break;
                            }
                        }
                        if (rookfile < PieceSquare._file)
                        {
                            DefaultInfo.BlackHsideRookIsUnMoved = false;
                        }
                        else
                        {
                            DefaultInfo.BlackAsideRookIsUnMoved = false;
                        }
                    }
                }
                return tempboard;
            }
            else
            {
                throw new ArgumentException("wrong move");
            }
        }

        private static ChessBoard PerformBlackPawnCapture(ChessBoard board, string notation, ChessBoard tempboard)
        {
            char pawnfile = notation[0];
            char enemyfile = notation[1];
            sbyte enemyrank = (sbyte)Char.GetNumericValue(notation[2]);
            sbyte pawnrank = (sbyte)(enemyrank + 1);
            if (board[pawnfile, pawnrank] != -1)
            {
                throw new ArgumentException("wrong notation");
            }
            tempboard[pawnfile, pawnrank] = 0;
            if (DefaultInfo.WhiteEnPassantEndangered && enemyfile == DefaultInfo.EnPassantPossibleCapture._file && enemyrank == DefaultInfo.EnPassantPossibleCapture._rank)
            {
                tempboard[enemyfile, enemyrank + 1] = 0;
            }
            if (notation.Length == 3)
            {
                tempboard[enemyfile, enemyrank] = (sbyte)DefaultPieces.BlackPawn;
            }
            else
            {
                tempboard[enemyfile, enemyrank] = GetSbyteFromBlackPieceLetter(notation[3]);
            }
            if (BlackPawn.GetPossiblePositions(board, pawnfile, pawnrank).Contains(tempboard))
            {
                return tempboard;
            }
            else
            {
                throw new ArgumentException("Wrong move");
            }
        }

        private static ChessBoard PerformBlackPawnMove(ChessBoard board, string notation, ChessBoard tempboard)
        {
            //simple move (not 5 _rank) and promotion
            if (notation[1] != '5')
            {
                sbyte pawnrank = (sbyte)(Char.GetNumericValue(notation[1]) + 1);
                char pawnfile = notation[0];
                if (board[pawnfile, pawnrank] != (sbyte)DefaultPieces.BlackPawn)
                {
                    throw new ArgumentException("wrong move");
                }
                tempboard[pawnfile, pawnrank] = 0;
                if (notation.Length == 2)
                {
                    tempboard[pawnfile, pawnrank - 1] = (sbyte)DefaultPieces.BlackPawn;
                }
                else
                {
                    tempboard[pawnfile, pawnrank - 1] = GetSbyteFromBlackPieceLetter(notation[2]);
                }

                if (BlackPawn.GetPossiblePositions(board, pawnfile, pawnrank).Contains(tempboard))
                {

                    return tempboard;
                }
                else
                {
                    throw new ArgumentException("Wrong move");
                }
            }
            //simple move (5 _rank)
            else
            {
                char pawnfile = notation[0];
                sbyte pawnrank;
                if (board[pawnfile, (sbyte)(Char.GetNumericValue(notation[1]) + 1)] == (sbyte)DefaultPieces.BlackPawn)
                {
                    pawnrank = (sbyte)(Char.GetNumericValue(notation[1]) + 1);
                }
                else if (board[pawnfile, (sbyte)(Char.GetNumericValue(notation[1]) + 2)] == (sbyte)DefaultPieces.BlackPawn && board[pawnfile, (sbyte)(Char.GetNumericValue(notation[1]) + 1)] == 0)
                {
                    pawnrank = (sbyte)(Char.GetNumericValue(notation[1]) + 2);
                }
                else
                {
                    throw new ArgumentException("Wrong move");
                }
                tempboard[pawnfile, pawnrank] = 0;
                tempboard[pawnfile, (sbyte)Char.GetNumericValue(notation[1])] = (sbyte)DefaultPieces.BlackPawn;

                if (BlackPawn.GetPossiblePositions(board, pawnfile, pawnrank).Contains(tempboard))
                {
                    //en passant
                    if (pawnrank == (sbyte)(Char.GetNumericValue(notation[1]) + 2))
                    {

                        DefaultInfo.EnPassantPossibleCapture = new Square(notation[0], (sbyte)(Char.GetNumericValue(notation[1]) + 1));
                        DefaultInfo.BlackEnPassantEndangered = true;
                    }
                    return tempboard;
                }
                else
                {
                    throw new ArgumentException("Wrong move");
                }
            }
        }

        private static ChessBoard PerformBlackQueenCastling(ChessBoard board, ref ChessBoard tempboard)
        {
            char kingfile;
            tempboard = PerformCastling(board, false, false, out kingfile);
            if (BlackKing.GetPossiblePositions(board, kingfile, 8).Contains(tempboard))
            {
                DefaultInfo.BlackKingIsUnMoved = false;
                DefaultInfo.BlackAsideRookIsUnMoved = false;
                return tempboard;
            }
            else
            {
                throw new ArgumentException("Wrong move");
            }
        }

        private static ChessBoard PerformBlackKingCastling(ChessBoard board, ref ChessBoard tempboard)
        {
            char kingfile;
            tempboard = PerformCastling(board, false, true, out kingfile);
            if (BlackKing.GetPossiblePositions(board, kingfile, 8).Contains(tempboard))
            {
                DefaultInfo.BlackKingIsUnMoved = false;
                DefaultInfo.BlackHsideRookIsUnMoved = false;
                return tempboard;
            }
            else
            {
                throw new ArgumentException("Wrong move");
            }
        }
    }
}
