using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quoridor1 
{
    class Cell : Button
    {
        int _width;
        int _height;
        public Cell(int _width, int _height)
        {
            this._height = _height;
            this._width = _width;
        }
    }
}
