using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TrueChessGame.GameEngine
{
    public class ChessBoard : IEquatable<ChessBoard>
    {
        private byte height;

        private sbyte[,] squares;

        public byte Height
        {
            get
            {
                return height;
            }
        }

        public sbyte this[char file, int rank]
        {
            get
            {
                return squares[file - 97, rank-1];
            }
            set
            {
                squares[file - 97, rank-1] = value;
            }
        }

        public sbyte this[Square temp]
        {
            get
            {
                return squares[temp.file - 97, temp.rank - 1];
            }
            set
            {
                squares[temp.file - 97, temp.rank - 1] = value;
            }
        }

        public ChessBoard()
        {
            // Code Review: Неіменована константа
            height = 8;
            squares = new sbyte[height, height];

        }

        public ChessBoard(byte heightToSet)
        {
            // Code Review: Неіменована константа
            if (heightToSet > 26)
            {
                height = 26;
            }
            else
            {
                height = heightToSet;
            }
            squares = new sbyte[height, height];
        }

        public ChessBoard(ChessBoard other)
        {
            this.height = other.height;
            this.squares = (sbyte[,]) other.squares.Clone();
        }

        public bool Equals(ChessBoard other)
        {
            if (other==null)
            {
                return false;
            }
            bool areequal=true;
            for (int i = 0; i < height; i++ )
            {
                for (int j=0; j<height; j++)
                {
                    if (this.squares[i, j]!=other.squares[i, j])
                    {
                        areequal = false;
                        break;
                    }
                }
            }
            if (this.height == other.height && areequal)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public override bool Equals(object obj)
        {
            if (obj==null)
            {
                return false;
            }
            ChessBoard sndobj = obj as ChessBoard;
            return this.Equals(sndobj);
        }

        public override int GetHashCode()
        {
            int result = height;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    result += i * j * squares[i, j]; 
                }
            }
            return result;
        }
        /*			
			Review VV:
			    на мою думку, назва не відповідає призначенню методу
                оскільки метод робить глибоку копію (а не поверхневу)
		*/
        public ChessBoard ShallowCopy()
        {
            return new ChessBoard(this);
        }

        public void SetDefaultChessBoard()
        {
            SetDefaultWhitePosition();
            //now Black
            SetDefaultBlackPosition();
        }

        private void SetDefaultBlackPosition()
        {
            this['a', 8] = (sbyte)DefaultPieces.BlackRook;
            this['b', 8] = (sbyte)DefaultPieces.BlackkNight;
            this['c', 8] = (sbyte)DefaultPieces.BlackBishop;
            this['d', 8] = (sbyte)DefaultPieces.BlackQueen;
            this['e', 8] = (sbyte)DefaultPieces.BlackKing;
            this['f', 8] = (sbyte)DefaultPieces.BlackBishop;
            this['g', 8] = (sbyte)DefaultPieces.BlackkNight;
            this['h', 8] = (sbyte)DefaultPieces.BlackRook;
            for (char file = 'a'; file <= 'h'; file++)
            {
                this[file, 7] = (sbyte)DefaultPieces.BlackPawn;
            }
        }

        private void SetDefaultWhitePosition()
        {
            this['a', 1] = (sbyte)DefaultPieces.WhiteRook;
            this['b', 1] = (sbyte)DefaultPieces.WhitekNight;
            this['c', 1] = (sbyte)DefaultPieces.WhiteBishop;
            this['d', 1] = (sbyte)DefaultPieces.WhiteQueen;
            this['e', 1] = (sbyte)DefaultPieces.WhiteKing;
            this['f', 1] = (sbyte)DefaultPieces.WhiteBishop;
            this['g', 1] = (sbyte)DefaultPieces.WhitekNight;
            this['h', 1] = (sbyte)DefaultPieces.WhiteRook;
            for (char file = 'a'; file <= 'h'; file++)
            {
                this[file, 2] = (sbyte)DefaultPieces.WhitePawn;
            }
        }

        public void DebugConsoleSimpleDraw()
        {
            for (int trank = 8; trank >= 1; trank--)
            {
                Console.WriteLine();
                for (char tfile = 'a'; tfile <= 'h'; tfile++)
                {
                    //Console.WriteLine(tfile + " " + trank);
                    if (this[tfile, trank] < 0)
                    {
                        Console.Write(this[tfile, trank] + " ");
                    }
                    else if (this[tfile, trank] > 0)
                    {
                        Console.Write("+" + this[tfile, trank] + " ");
                    }
                    else
                    {
                        Console.Write(" 0 ");
                    }
                }

            }
        }

        public void ReverseSides()
        {
            ChessBoard tempboard = new ChessBoard();
            for (char tchar = 'a'; tchar <= 'h'; tchar++ )
            {
               for (sbyte trank=1; trank<=8; trank++)
               {
                   Square tempsquare = new Square(tchar, trank);
                   Square rsquare= tempsquare;
                   rsquare.Reverse();
                   tempboard[rsquare] =(sbyte)(-1* this[tempsquare]);
               }
            }
            this.squares = tempboard.squares;
        }
    }
}
