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

        private int _amWalls = 10;
        public int Walls
        {
            get
            {
                return _amWalls;
            }
        }

        public delegate (bool, int, int, int) MakeTurn(Board board, params int[] data);

        public MakeTurn makeTurn;

        private bool _turn;
        public bool Turn
        {
            get
            {
                return _turn;
            }
        }

        public Token(int h, int v, bool t, MakeTurn func)
        {
            _pos[0] = h;
            _pos[1] = v;
            _turn = t;
            makeTurn = func;
        }
    }
}
