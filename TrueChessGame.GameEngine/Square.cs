using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueChessGame.GameEngine
{
    public struct Square
    {
        /*			
			Review VV:
			    1) поля не слід робити публічними
                2) імена полів повинні розпочинатися з символу підкреслення
		*/

        public char _file;
       
       public sbyte _rank;

       public Square(char file, int rank)
       {
           this._file = file;
           this._rank = (sbyte)rank;
       }

       public Square(char file, sbyte rank)
       {
           this._file = file;
           this._rank = rank;
       }

        public bool IsOK()
       {
           bool FileIsOK = _file >= 'a' && _file <= 'h';
           bool RankIsOK = _rank >= 1 && _rank <= 8;
           return FileIsOK && RankIsOK;
       }

        public static sbyte ReverseRank(sbyte rank)
        {
            /*			
			    Review VV:
			        рекомендую реалізувати цю функцію більш раціонально - однією формулою
		    */
            return (sbyte)(9 - rank);
            //switch (rank)
            //{
            //    case 1: return 8;
            //    case 2: return 7;
            //    case 3: return 6;
            //    case 4: return 5;
            //    case 5: return 4;
            //    case 6: return 3;
            //    case 7: return 2;
            //    case 8: return 1;
            //    default: return 0;
            //}
        }

        public void Reverse()
        {
            this._rank = ReverseRank(this._rank);
        }
    };

}
