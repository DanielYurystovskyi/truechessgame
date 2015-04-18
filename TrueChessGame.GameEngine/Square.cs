using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueChessGame
{
    public struct Square
    {
       public char file;
       
       public sbyte rank;

       public Square(char file, int rank)
       {
           this.file = file;
           this.rank = (sbyte)rank;
       }

       public Square(char file, sbyte rank)
       {
           this.file = file;
           this.rank = rank;
       }
    };
}
