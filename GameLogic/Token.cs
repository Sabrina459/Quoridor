using System;
using System.Collections.Generic;
using System.Text;

namespace QuoridorGameLogic
{
    public class Token
    {
        private int[] _pos = new int[2];
        public int this[int i]
        {
            set
            {
                _pos[i] = value;
            }

            get
            {
                return _pos[i];
            }
        }

        private int _strt;
        public int StrtPos
        {
            get
            {
                return _strt;
            }
        }

        private int _amWalls;
        public int Walls
        {
            get
            {
                return _amWalls;
            }
        }

        public void wallPlaced() => _amWalls -= 1;

        public delegate (bool, int, int, int) MakeTurn(Board board, params int[] data);

        public MakeTurn makeTurn;

        public Token(int h, int v, int amW, int strt, MakeTurn func)
        {
            _pos[0] = h;
            _pos[1] = v;
            _amWalls = amW;
            _strt = strt;
            makeTurn = func;
        }
    }
}
