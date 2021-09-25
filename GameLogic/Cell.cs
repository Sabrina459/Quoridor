using System;
using System.Collections.Generic;
using System.Text;

namespace QuoridorGameLogic
{
    public class Cell
    {
        private int[] _pos = new int[2];
        public int[] Pos
        {
            get
            {
                return _pos;
            }
        }
        public Cell(int h, int v)
        {
            _pos[0] = h;
            _pos[1] = v;
        }
    }
}
