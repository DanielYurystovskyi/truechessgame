using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueChessGame.GameEngine
{
    class FIDEnotation
    {
        public delegate List<ChessBoard> GetPiecePositionsType(ChessBoard board, char file, sbyte rank);

        public static GetPiecePositionsType GetWhitePiecePositionsType (char letter)
        {
            GetPiecePositionsType result;
            switch (letter)
            {
                case 'K':
                    result=WhiteKing.GetPossiblePositions;
                    break;
                case 'Q':
                    result=WhiteQueen.GetPossiblePositions;
                    break;
                case 'R':
                    result=WhiteRook.GetPossiblePositions;
                    break;
                case 'B':
                    result=WhiteBishop.GetPossiblePositions;
                    break;
                case 'N':
                    result=WhitekNight.GetPossiblePositions;
                    break;
                default:
                    throw new ArgumentException("wrong notation");
            }
            return result;
        }

        public static sbyte GetSbyteFromWhitePieceLetter(char letter)
        {
            switch(letter)
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
                default:
                    throw new ArgumentException("wrong notation");
            }
        }

        public static sbyte GetSbyteFromBlackPieceLetter(char letter)
        {
            switch (letter)
            {
                case 'K':
                    return (sbyte)DefaultPieces.BlackKing;
                case 'Q':
                    return (sbyte)DefaultPieces.BlackQueen;
                case 'R':
                    return (sbyte)DefaultPieces.BlackRook;
                case 'B':
                    return (sbyte)DefaultPieces.BlackBishop;
                case 'N':
                    return (sbyte)DefaultPieces.BlackkNight;
                default:
                    throw new ArgumentException("wrong notation");
            }
        }

        public static List<Square> GetPossibleBlackMoversToSquare(ChessBoard board, char file, sbyte rank)
        {
            List<Square> result = new List<Square>();
            return result;
        }

        public static List<Square> GetPossibleWhiteMoversToSquare(ChessBoard board, char file, sbyte rank)
        {
            List<Square> result = new List<Square>();
            return result;
        }
        // Code Review: Надто об'ємний метод.
        // Code Review: Потрібно розділяти умови дужками () в операторах if.
        public static ChessBoard PerformWhiteMove(ChessBoard board, string notation)
        {
            ChessBoard tempboard = board.ShallowCopy();
            #region King-castling
            if (notation == "O-O" && DefaultInfo.WhiteKingIsUnMoved && DefaultInfo.WhiteHsideRookIsUnMoved)
            {
                char kingfile = 'e';
                for (char tfile = 'b'; tfile <= 'g'; tfile++)
                {
                    if (board[tfile, 1] == (sbyte)DefaultPieces.WhiteKing)
                    {
                        kingfile = tfile;
                        break;
                    }
                }
                char Hrookfile = 'h';
                for (char tfile = 'h'; tfile >= 'c'; tfile++)
                {
                    if (board[tfile, 1] == (sbyte)DefaultPieces.WhiteRook)
                    {
                        Hrookfile = tfile;
                        break;
                    }
                }
                tempboard[kingfile, 1] = 0;
                tempboard[Hrookfile, 1] = 0;
                tempboard['g', 1] = (sbyte)DefaultPieces.WhiteKing;
                tempboard['f', 1] = (sbyte)DefaultPieces.WhiteRook;
                if (WhiteKing.GetPossiblePositions(board, kingfile, 1).Contains(tempboard))
                {
                    return tempboard;
                }
                else
                {
                    throw new ArgumentException("Wrong move");
                }
            }
            #endregion
            #region Queen-castling
            else if (notation == "O-O-O" && DefaultInfo.WhiteKingIsUnMoved && DefaultInfo.WhiteAsideRookIsUnMoved)
            {
                char kingfile = 'e';
                for (char tfile = 'b'; tfile <= 'g'; tfile++)
                {
                    if (board[tfile, 1] == (sbyte)DefaultPieces.WhiteKing)
                    {
                        kingfile = tfile;
                        break;
                    }
                }
                char Arookfile = 'h';
                for (char tfile = 'a'; tfile <= 'f'; tfile++)
                {
                    if (board[tfile, 1] == (sbyte)DefaultPieces.WhiteRook)
                    {
                        Arookfile = tfile;
                        break;
                    }
                }
                tempboard[kingfile, 1] = 0;
                tempboard[Arookfile, 1] = 0;
                tempboard['c', 1] = (sbyte)DefaultPieces.WhiteKing;
                tempboard['d', 1] = (sbyte)DefaultPieces.WhiteRook;
                if (WhiteKing.GetPossiblePositions(board, kingfile, 1).Contains(tempboard))
                {
                    return tempboard;
                }
                else
                {
                    throw new ArgumentException("Wrong move");
                }
            }
            #endregion
            #region Pawn
            else if (notation[0]<='h' && notation[0]>='a')
            {
                #region simple move
                if (notation[1]<='8' && notation[1]>='3' && notation.Length == 2)
                {
                        //simple move (not 4 rank)
                        if (notation[1] != 4)
                        {
                            sbyte pawnrank = (sbyte)(Char.GetNumericValue(notation[1]) - 1);
                            char pawnfile = notation[0];
                            tempboard[pawnfile, pawnrank] = 0;
                            tempboard[pawnfile, pawnrank + 1] = (sbyte)DefaultPieces.WhitePawn;
                            if (WhitePawn.GetPossiblePositions(board, pawnfile, pawnrank).Contains(tempboard))
                            {
                                return tempboard;
                            }
                            else
                            {
                                throw new ArgumentException("Wrong move");
                            }
                        }
                        //simple move (4 rank)
                        else
                        {
                            char pawnfile = notation[0];
                            sbyte pawnrank;
                            if (board[pawnfile, (sbyte)(Char.GetNumericValue(notation[1]) - 1)] == (sbyte)DefaultPieces.WhitePawn)
                            {
                                pawnrank = (sbyte)(Char.GetNumericValue(notation[1]) - 1);
                            }
                            else if (board[pawnfile, (sbyte)(Char.GetNumericValue(notation[1]) - 2)] == (sbyte)DefaultPieces.WhitePawn)
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
                                return tempboard;
                            }
                            else
                            {
                                throw new ArgumentException("Wrong move");
                            }
                        }
                    
                }
                #endregion
                #region capture move
                else if (notation.Length == 4 && notation[1] == 'x' && notation[2] <= 'h' && notation[2] >= 'a' && notation[3] <= '8' && notation[3] >= '3')
                {
                    char pawnfile = notation[0];
                    char enemyfile = notation[2];
                    sbyte enemyrank = (sbyte)Char.GetNumericValue(notation[3]);
                    sbyte pawnrank = (sbyte)(enemyrank - 1);
                    tempboard[pawnfile, pawnrank] = 0;
                    tempboard[enemyfile, enemyrank] = (sbyte)DefaultPieces.WhitePawn;
                    if (WhitePawn.GetPossiblePositions(board, pawnfile, pawnrank).Contains(tempboard))
                    {
                        return tempboard;
                    }
                    else
                    {
                        throw new ArgumentException("Wrong move");
                    }
                }
                #endregion
                //To Do en passant!!!
                else
                {
                    throw new ArgumentException("Wrong move");
                }
            }
            #endregion
            #region Pieces (not Pawn)
            else if (notation[0]<='Z' && notation[0]>='A')
            {
                #region Non-ambiguous
                if ( (notation.Length==3 && notation[1]<='h' && notation[1]>='a' && notation[2]>='0' && notation[2]<='8') || (notation.Length==4 && notation[1]=='x' && notation[2]<='h' && notation[2]>='a' && notation[2]>='0' && notation[2]<='8') )
                {
                    char goalfile;
                    sbyte goalrank;
                    if (notation.Length==3)
                    {
                        goalfile = notation[1];
                        goalrank = (sbyte)Char.GetNumericValue(notation[2]);
                    }
                    else
                    {
                        goalfile = notation[2];
                        goalrank = (sbyte)Char.GetNumericValue(notation[3]);
                    }
                    //Check if piece is not-ambigious and get piece square
                    List<Square> PossibleMovers=GetPossibleBlackMoversToSquare(board, goalfile, goalrank);
                    int NumberOfSamePieces=0;
                    Square PieceSquare = new Square();
                    foreach (Square tempsquare in PossibleMovers)
                    {
                        if (board[tempsquare.file, tempsquare.rank]==GetSbyteFromWhitePieceLetter(notation[0]))
                        {
                            NumberOfSamePieces++;
                            PieceSquare = tempsquare;
                        }
                    }
                    if (NumberOfSamePieces!=1)
                    {
                        throw new ArgumentException("wrong notation");
                    }
                    tempboard[PieceSquare.file, PieceSquare.rank] = 0;
                    tempboard[goalfile, goalrank] = GetSbyteFromWhitePieceLetter(notation[0]);
                    GetPiecePositionsType piecefunction = GetWhitePiecePositionsType(notation[0]);
                    if (piecefunction(board, goalfile, goalrank).Contains(tempboard))
                    {
                        return tempboard;
                    }
                    else
                    {
                        throw new ArgumentException("wrong move");
                    }
                }
                #endregion
                #region Ambiguous
                else if (notation.Length<=6 && notation.Length>=4 && notation[2]!='x')
                {
                    //&& ( (notation.Length==4 && notation[1]>='a' && notation[1]<='h' && notation[2]>='a' && notation[2]<='h' && notation[3]<='8' && notation[3]>='1' ) || ( (notation.Length==4 && notation[1]>='a' && notation[1]<='h' && notation[2]>='a' && notation[2]<='h' && notation[3]<='8' && notation[3]>='1' )  ) )
                    char piecefile;
                    sbyte piecerank;
                    char goalfile;
                    sbyte goalrank;
                    sbyte piecetype = GetSbyteFromWhitePieceLetter(notation[0]);
                    //Ngf3
                    if (notation.Length==4 && notation[1]>='a' && notation[1]<='h' && notation[2]>='a' && notation[2]<='h' && notation[3]>='1' && notation[3]<='8')
                    {
                        goalfile = notation[2];
                        goalrank = (sbyte)Char.GetNumericValue(notation[3]);
                        piecefile = notation[1];
                        for (sbyte i=1; i<=8; i++)
                        {
                            if (board[piecefile, i]==piecetype)
                            {
                                piecerank = i;
                                break;
                            }
                        }
                    }
                    //Ngxf3
                    else if(notation.Length==5 && notation[1]>='a' && notation[1]<='h' && notation[2]=='x' && notation[3]>='a' && notation[3]<='h' && notation[4]>='1' && notation[4]<='8')
                    {
                        goalfile = notation[3];
                        goalrank = (sbyte)Char.GetNumericValue(notation[4]);
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
                    else if (notation.Length==4 && notation[1]>='1' && notation[1]<='8' && notation[2]>='a' && notation[2]<='h' && notation[3]>='1' && notation[3]<='8')
                    {
                        goalfile = notation[2];
                        goalrank = (sbyte)Char.GetNumericValue(notation[4]);
                        piecerank = (sbyte)Char.GetNumericValue(notation[1]);
                        for (char tfile='a'; tfile<='h'; tfile++)
                        {
                            if (board[tfile, piecerank]==piecetype)
                            {
                                piecefile = tfile;
                                break;
                            }
                        }
                    }
                    //N5xf4
                    else if (notation.Length==5 && notation[1]>='1' && notation[1]<='8' && notation[2]=='x' && notation[3]>='a' && notation[3]<='h' && notation[4]>='1' && notation[4]<='8')
                    {
                        goalfile = notation[3];
                        goalrank = (sbyte)Char.GetNumericValue(notation[5]);
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
                    else if (notation.Length==5 && notation[1]>='a' && notation[1]<='h' && notation[2]>='1' && notation[2]<='8' && notation[3]>='a' && notation[3]<='h' && notation[4]>='1' && notation[4]<='8')
                    {
                        goalfile = notation[3];
                        goalrank = (sbyte)Char.GetNumericValue(notation[4]);
                        piecefile = notation[1];
                        piecerank = (sbyte)Char.GetNumericValue(notation[2]);
                    }
                    //Ng5xf3
                    else if (notation.Length==6 && notation[1]>='a' && notation[1]<='h' && notation[2]>='1' && notation[2]<='8' && notation[3]=='x' && notation[4]>='a' && notation[4]<='h' && notation[5]>='1' && notation[5]<='h')
                    {
                        goalfile = notation[4];
                        goalrank = (sbyte)Char.GetNumericValue(notation[5]);
                        piecefile = notation[1];
                        piecerank = (sbyte)Char.GetNumericValue(notation[2]);
                    }
                }
                #endregion
            }
            #endregion
            else
            {
                throw new ArgumentException("Wrong notation");
            }
        }
    }
}
