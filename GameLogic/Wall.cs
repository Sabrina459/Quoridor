using System;
using System.Collections.Generic;
using System.Text;

namespace QuoridorGameLogic
{
    public class Wall
    {
        private bool _state = false;
        public bool State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }
        private int[] _pos = new int[2];
        public int[] Pos
        {
            get
            {
                return _pos;
            }
        }
        private int _dir;
        public int Dir
        {
            get
            {
                return _dir;
            }
        }
        private int _len;
        public int Len
        {
            get
            {
                return _len;
            }
        }

        public Wall(int dir, int h, int v, int l)
        {
            _pos[0] = h;
            _pos[0] = v;
            _len = l;
            _dir = dir;
        }
    }
}
