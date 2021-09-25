using System;
using System.Collections.Generic;
using System.Text;

namespace QuoridorGameLogic
{
    public class Board
    {
        private int _size;
        public int Size
        {
            get
            {
                return _size;
            }
        }
        private Cell[,] cells;

        private Wall[,,] walls;
        public Cell this [int h, int v]
        {
            get
            {
                return cells[h, v];
            }
        }
        public Wall this[int dir, int h, int v]
        {
            get
            {
                return walls[dir, h, v];
            }
        }
        private Token[] players;
        public Token getToken(int i) => players[i];
        private readonly int wallsize;

        private bool _turn;
        public bool Turn
        {
            get
            {
                return _turn;
            }
        }
        public Board(int size, int wallsize, Token[] players)
        {
            _size = size;
            cells = new Cell[size, size];
            walls = new Wall[2, size - 1, size - 1];
            for (int h = 0; h < size; h++)
            {
                for (int v = 0; v < size; v++)
                {
                    cells[h, v] = new Cell(h, v);
                }
            }
            for (int d = 0; d < 2; d++)
            {
                for (int h = 0; h < size - 1; h++)
                {
                    for (int v = 0; v < size - 1; v++)
                    {
                        walls[d, h, v] = new Wall(d, h, v, wallsize);
                    }
                }
            }
            //
            PutWall(0, 0, 1);
            PutWall(0, 0, 0);
            PutWall(0, 1, 1);
            PutWall(0, 2, 1);
            PutWall(0, 1, 0);
            PutWall(7, 0, 0);
            //
            this.players = players;
            this.wallsize = wallsize;
            _turn = false;
        }

        public bool IsCanReachEnd()
        {
            return true;
        }

        public bool IsEnd()
        {
            return (players[0].Pos[1] == _size-1 || players[1].Pos[1] == 0);
        }

        public void MakeTurn()
        {
            players[_turn ? 1 : 0].MakeTurn();
            _turn = !_turn;
        }

        public bool PutWall(int h, int v, int dir)
        {
            if (walls[0, h, v].State || walls[1, h, v].State
                || (dir == 0 && h - 1 >= 0 && walls[dir, h-1, v].State)
                || (dir == 1 && v - 1 >= 0 && walls[dir, h, v-1].State))
                return false;
            else
                walls[dir, h, v].State = true;
            return true;
        }
    }
}
