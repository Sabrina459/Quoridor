using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quoridor1
{
    class Wall : Button
    {
        int _width;
        int _height;
        bool isHorizontal;
        public Wall(int _width, int _height, bool isHorizontal)
        {
            this.isHorizontal = isHorizontal;
            if (this.isHorizontal)
            {
                this._height = _width;
                this._width = _height;
            }
            else
            {
                this._height = _height;
                this._width = _width;
            }
            
        }
    }
}
