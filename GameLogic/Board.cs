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
            get// board[h, v] => Cell
            {
                return cells[h, v];
            }
        }
        public Wall this[int dir, int h, int v]
        {
            get// board[0/1, h, v] => Wall
            {
                return walls[dir, h, v];
            }
        }
        private Token[] players;

        delegate int doTurn();
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
            
            this.players = players;
            this.wallsize = wallsize;
            _turn = false;
        }

        public bool IsCanReachEnd()
        {
            return true;
        }

        public int IsEnd()
        {
            if (players[0][1] == 0)
                return 1;
            else if (players[1][1] == _size - 1)
                return 2;
            return 0;
        }

        public void MakeTurn()
        {
            bool done = false;
            Token currT = players[_turn ? 1 : 0];
            while (!done)
            {
                var res = currT.makeTurn(this);
                if (res.Item1)
                    done = MoveToken(res.Item2, res.Item3);
                else
                    done = PutWall(res.Item2, res.Item3, res.Item4);
            }
            _turn = !_turn;
        }

        public bool PutWall(int h, int v, int dir)
        {
            if (h > _size - 1 || v > _size - 1)
                return false;
            if (walls[0, h, v].State || walls[1, h, v].State
                || (dir == 0 && h - 1 >= 0 && walls[dir, h-1, v].State)
                || (dir == 1 && v - 1 >= 0 && walls[dir, h, v-1].State))
                return false;
            else
            {
                walls[dir, h, v].State = true;
                if (!IsCanReachEnd())
                {
                    walls[dir, h, v].State = false;
                    return false;
                }
                return true;
            }
        }

        public bool MoveToken(int h, int v)
        {
            Token currT = players[_turn ? 1 : 0];
            if (h == currT[0] && v == currT[1])
                return false;
            if (h >= 0 && h < _size && v >= 0 && v < _size)
            {
                if (h != currT[0] && v != currT[1])
                    return false;
                if (h != currT[0])
                {
                    if (h > currT[0])
                    {
                        if (currT[1] == 0)
                        {
                            if (walls[1, currT[0], currT[1]].State)
                                return false;
                        }
                        else if (currT[1] == _size - 1)
                        {
                            if (walls[1, currT[0], currT[1] - 1].State)
                                return false;
                        }
                        else
                            if (walls[1, currT[0], currT[1]].State || walls[1, currT[0], currT[1] - 1].State)
                                return false;
                    } 
                    else
                    {
                        if (currT[1] == 0)
                        {
                            if (walls[1, currT[0] - 1, currT[1]].State)
                                return false;
                        }
                        else if (currT[1] == _size - 1)
                        {
                            if (walls[1, currT[0] - 1, currT[1] - 1].State)
                                return false;
                        }
                        else
                            if (walls[1, currT[0] - 1, currT[1]].State || walls[1, currT[0] - 1, currT[1] - 1].State)
                            return false;
                    }
                }
                else if (v != currT[1])
                {
                    if (v > currT[1])
                    {
                        if (currT[0] == 0)
                        {
                            if (walls[0, currT[0], currT[1]].State)
                                return false;
                        }
                        else if (currT[0] == _size - 1)
                        {
                            if (walls[0, currT[0] - 1, currT[1]].State)
                                return false;
                        }
                        else
                            if (walls[0, currT[0], currT[1]].State || walls[0, currT[0] - 1, currT[1]].State)
                            return false;
                    }
                    else
                    {
                        if (currT[0] == 0)
                        {
                            if (walls[0, currT[0], currT[1] - 1].State)
                                return false;
                        }
                        else if (currT[0] == _size - 1)
                        {
                            if (walls[0, currT[0] - 1, currT[1] - 1].State)
                                return false;
                        }
                        else
                            if (walls[0, currT[0], currT[1] - 1].State || walls[0, currT[0] - 1, currT[1] - 1].State)
                            return false;
                    }
                }
                if (((currT[0] > h) ? currT[0] - h : h - currT[0]) <= 1 && ((currT[1] > v) ? currT[1] - v : v - currT[1]) <= 1)
                {
                    currT[0] = h;
                    currT[1] = v;
                    return true;
                }
            }
            return false;
        }
    }
}
