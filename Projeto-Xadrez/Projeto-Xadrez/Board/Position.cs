using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto_Xadrez.Board
{
    class Position
    {
        public int Line { get; set; }
        public int Column { get; set; }

        public Position(int line, int column)
        {
            Line = line;
            Column = column;
        }

        public override string ToString()
        {
            return $"{Line}, {Column}";
        }
    }
}
