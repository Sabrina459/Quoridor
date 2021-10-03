using System;
using System.Collections.Generic;
using System.Text;

namespace QuoridorGameLogic
{
    public class Cell
    {
        public int Filled { get; set; }
        public Cell() { }

        public Cell(int p) => Filled = p;
    }
}
