using System;
using System.Collections.Generic;
using System.Text;

namespace QuoridorGameLogic
{
    public class Token
    {
        private int[] _pos = new int[2];
        public int[] Pos
        {
            private set
            {
                _pos = value;
            }

            get
            {
                return _pos;
            }
        }

        private int _amWalls = 10;
        public int Walls
        {
            get
            {
                return _amWalls;
            }
        }

        private bool _turn;
        public bool Turn
        {
            get
            {
                return _turn;
            }
        }

        public Token(int h, int v, bool t)
        {
            _pos[0] = h;
            _pos[1] = v;
            _turn = t;
        }

        public virtual void MakeTurn()
        {
            // _pos[1] += 1;
        }
    }
}
