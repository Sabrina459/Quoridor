using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quoridor1
{
    public partial class Form1 : Form
    {
        int left = 0;
        int top = 0;
        int lastLeft = 0;
        int lastTop = 0;
        int _cellHeight = 60;
        int _cellWidth = 60;
        int _wallHeight = 60;
        int _wallWidth = 10;


        public Form1()
        {
            InitializeComponent();
            top = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Button cell = new Button();
                    cell.Width = _cellWidth;
                    cell.Height = _cellHeight;
                    cell.Left = left;
                    cell.Top = top;
                    this.Controls.Add(cell);
                    left += cell.Width + _wallWidth;

                }
                left = 0;
                top += _cellHeight + _wallWidth;
            }
            top = _cellHeight;
            for (int i = 0; i < 10; i++)
            {

                for (int j = 0; j < 9; j++)
                {
                    Button wallH = new Button();
                    wallH.MouseHover += new EventHandler(OnMouseOverH);
                    wallH.MouseLeave += new EventHandler(OnMouseLeaveH);
                    wallH.FlatStyle = FlatStyle.Flat;
                    wallH.FlatAppearance.BorderSize = 0;
                    wallH.BackColor = Color.Transparent;
                    wallH.Height = _wallWidth;
                    wallH.Top = top;
                    wallH.Left = left;
                    if (j == 8)
                    {
                        wallH.Width = 130;
                        lastLeft = wallH.Left;
                    }
                    else wallH.Width = _wallHeight;
                    
                    this.Controls.Add(wallH);
                    left += _wallWidth + _cellWidth;

                }
                left = 0;
                top += _cellHeight + _wallWidth;
            }
            left = _cellWidth;
            top = 0;
            for (int i = 0; i < 9; i++)
            {

                for (int j = 0; j < ; j++)
                {
                    Button wallV = new Button();
                    wallV.MouseHover += new EventHandler(OnMouseOverV);
                    wallV.MouseLeave += new EventHandler(OnMouseLeaveV);
                    wallV.FlatStyle = FlatStyle.Flat;
                    wallV.FlatAppearance.BorderSize = 0;
                    wallV.BackColor = Color.Transparent;
                    wallV.Top = top;
                    wallV.Width = _wallWidth;
                    wallV.Left = left;
                    if (i == 8)
                    {
                        wallV.Height = 130;
                        lastTop = wallV.Top;
                    }else wallV.Height = _wallHeight;

                    this.Controls.Add(wallV);
                    left += _wallWidth + _cellWidth;

                }
                left = _cellWidth;
                top += _cellHeight + _wallWidth;
            }
        }

        private void OnMouseOverH(object sender, EventArgs e )
        {
            Button btn = (Button)sender;
            btn.Width = 130;
            btn.BringToFront();
        }
        private void OnMouseOverV(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Height = 130;
            btn.BringToFront();


        }
        private void OnMouseLeaveV(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Top == lastTop)
            {
                btn.Height = 130;
            }else btn.Height = _wallHeight;


        }
        private void OnMouseLeaveH(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Left == lastLeft)
            {
                btn.Width = 130;
            }
            else btn.Width = _wallHeight;

        }
    }
}
