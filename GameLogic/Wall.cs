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

        public Wall() { }
    }
}
