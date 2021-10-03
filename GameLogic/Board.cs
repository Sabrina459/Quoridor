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
        private byte[,] matW;
        byte[,] weights;
        byte[,] paths;

        private Cell[,] cells;

        private Wall[,,] walls;
        public Cell this [int h, int v]
        {
            get// board[h, v] => Cell
            {
                return cells[h, v];
            }
        }
        public bool this[int dir, int h, int v]
        {
            get// board[0/1, h, v] => Wall.State
            {
                return walls[dir, h, v].State;
            }
        }
        private Token[] players;
        public Token[] Players
        {
            get
            {
                return players;
            }
        }
        public Token this[int i]
        {
            get
            {
                return players[i];
            }
        }

        private int _turn;
        public int Turn
        {
            get
            {
                return _turn;
            }
        }
        public Board(int size, Token[] players)
        {
            if (players.Length != 2 && players.Length != 4)
                throw new Exception();
            _size = size;
            matW = new byte[size * size, size * size];
            weights = new byte[_size * _size, _size * _size];
            paths = new byte[_size * _size, _size * _size];

            cells = new Cell[size, size];
            walls = new Wall[2, size - 1, size - 1];

            for (int v = 0; v < size; v++)
            {
                for (int h = 0; h < size; h++)
                {
                    for (int i = 0; i < players.Length; i++)
                    {
                        if (players[i][0] == h && players[i][1] == v) {
                            cells[h, v] = new Cell(i+1);
                            break;
                        }
                        if (i == players.Length - 1) cells[h, v] = new Cell(0);
                    }
                    
                    for (int k = 0; k < size * size; k++)
                    {
                        if (h + v * size == k) matW[k, k] = 0;
                        else matW[h + v * size, k] = 82;
                    }
                    //edge martix
                    int edge = h + v * size;
                    if (v == 0)
                    {
                        if (h == 0)
                        {
                            matW[edge, edge + 1] = 1;
                            matW[edge, edge + size] = 1;
                        }
                        else if (h == size - 1)
                        {
                            matW[edge, edge - 1] = 1;
                            matW[edge, edge + size] = 1;
                        }
                        else
                        {
                            matW[edge, edge - 1] = 1;
                            matW[edge, edge + 1] = 1;
                            matW[edge, edge + size] = 1;
                        }
                    }
                    else if (v == size - 1)
                    {
                        if (h == 0)
                        {
                            matW[edge, edge + 1] = 1;
                            matW[edge, edge - size] = 1;
                        }
                        else if (h == size - 1)
                        {
                            matW[edge, edge - 1] = 1;
                            matW[edge, edge - size] = 1;
                        }
                        else
                        {
                            matW[edge, edge - 1] = 1;
                            matW[edge, edge + 1] = 1;
                            matW[edge, edge - size] = 1;
                        }
                    }
                    else if (h == 0)
                    {
                        matW[edge, edge + 1] = 1;
                        matW[edge, edge + size] = 1;
                        matW[edge, edge - size] = 1;
                    }
                    else if (h == _size - 1)
                    {
                        matW[edge, edge - 1] = 1;
                        matW[edge, edge + size] = 1;
                        matW[edge, edge - size] = 1;
                    }
                    else
                    {
                        matW[edge, edge - 1] = 1;
                        matW[edge, edge + 1] = 1;
                        matW[edge, edge + size] = 1;
                        matW[edge, edge - size] = 1;
                    }
                }
            }

            for (int d = 0; d < 2; d++)
            {
                for (int h = 0; h < size - 1; h++)
                {
                    for (int v = 0; v < size - 1; v++)
                    {
                        walls[d, h, v] = new Wall();
                    }
                }
            }
            //
            this.players = players;
            _turn = 0;
            IsCanReachEnd();
        }

        private bool IsCanReachEnd()
        {
            for (int i = 0; i < _size * _size; i++)
            {
                for (int j = 0; j < _size * _size; j++)
                {
                    weights[i, j] = matW[i, j];
                    paths[i, j] = 81;
                }
            }


            for (byte k = 0; k < _size * _size; k++)
            {
                for (byte i = 0; i < _size * _size; i++)
                {
                    for (byte j = 0; j < _size * _size; j++)
                    {
                        if (weights[i, j] > weights[i, k] + weights[k, j])
                        {
                            weights[i, j] = (byte)(weights[i, k] + weights[k, j]);
                            paths[i, j] = k;
                        }
                    }
                }
            }
            int allC = 0;
            foreach (Token p in players)
            {
                switch (p.StrtPos)
                {
                    case 0:
                        for (int i = 0; i < _size; i++)
                        {
                            if (weights[p[0] + p[1] * _size, (_size - 1) + i * _size] < 82)
                            {
                                allC += 1;
                                break;
                            }
                        }
                        break;
                    case 1:
                        for (int i = 0; i < _size; i++)
                        {
                            if (weights[p[0] + p[1] * _size, i + (_size - 1) * _size] < 82)
                            {
                                allC += 1;
                                break;
                            }
                        }
                        break;
                    case 2:
                        for (int i = 0; i < _size; i++)
                        {
                            if (weights[p[0] + p[1] * _size, 0 + i * _size] < 82)
                            {
                                allC += 1;
                                break;
                            }
                        }
                        break;
                    case 3:
                        for (int i = 0; i < _size; i++)
                        {
                            if (weights[p[0] + p[1] * _size, i + 0 * _size] < 82)
                            {
                                allC += 1;
                                break;
                            }
                        }
                        break;
                }
            }
            if (allC < players.Length) return false;
            return true;
        }

        public int IsEnd()
        {
            for (int i = 0; i < players.Length; i++)
            {
                if ((   players[i].StrtPos == 0 && players[i][0] == _size - 1)
                    || (players[i].StrtPos == 1 && players[i][1] == _size - 1)
                    || (players[i].StrtPos == 2 && players[i][0] == 0)
                    || (players[i].StrtPos == 3 && players[i][1] == 0))
                    return i+1;
            }
            return 0;
        }

        private void NextTurn()
        {
            _turn++;
            if (_turn >= players.Length)
                _turn = 0;
        }

        public void MakeTurn()
        {
            bool done = false;
            Token currT = players[_turn];
            while (!done)
            {
                var res = currT.makeTurn(this);
                if (res.Item1)
                    done = MoveToken(res.Item2, res.Item3);
                else
                    done = PutWall(res.Item2, res.Item3, res.Item4);
            }
            NextTurn();
        }

        private void ActivateWall(int dir, int h, int v)
        {
            int edge = h + v * _size;
            walls[dir, h, v].State = true;
            if (dir == 0)
            {
                matW[edge, edge + _size] = 82;
                matW[edge + _size, edge] = 82;
                matW[edge + 1, edge + 1 + _size] = 82;
                matW[edge + 1 + _size, edge + 1] = 82;
            } 
            else
            {
                matW[edge, edge + 1] = 82;
                matW[edge + 1, edge] = 82;
                matW[edge + _size, edge + _size + 1] = 82;
                matW[edge + _size + 1, edge + _size] = 82;
            }
        }

        private void DisActivateWall(int dir, int h, int v)
        {
            int edge = h + v * _size;
            walls[dir, h, v].State = false;
            if (dir == 0)
            {
                matW[edge, edge + _size] = 1;
                matW[edge + _size, edge] = 1;
                matW[edge + 1, edge + 1 + _size] = 1;
                matW[edge + 1 + _size, edge +1] = 1;
            }
            else
            {
                matW[edge, edge + 1] = 1;
                matW[edge + 1, edge] = 1;
                matW[edge + _size, edge + _size + 1] = 1;
                matW[edge + _size + 1, edge + _size] = 1;
            }
        }

        private bool PutWall(int h, int v, int dir)
        {
            Token currT = players[_turn];
            if (currT.Walls == 0)
                return false;
            if (h > _size - 2 || v > _size - 2)
                return false;
            if (this[0, h, v] || this[1, h, v]
                || (dir == 0 && h - 1 >= 0 && this[dir, h-1, v])
                || (dir == 0 && h + 1 < _size - 1 && this[dir, h + 1, v])
                || (dir == 1 && v - 1 >= 0 && this[dir, h, v - 1])
                || (dir == 1 && v + 1 < _size - 1 && this[dir, h, v + 1]))
                return false;
            else
            {
                ActivateWall(dir, h, v);
                if (!IsCanReachEnd())
                {
                    DisActivateWall(dir, h, v);
                    return false;
                }
                currT.wallPlaced();
                return true;
            }
        }

        private bool IsObstacleLEFT(int h, int v)
        {
            if (h == 0)
                return true;
            if (v == 0)
            {
                if (this[1, h - 1, v]) return true;
                return false;
            }
            else if (v == _size - 1)
            {
                if (this[1, h - 1, v - 1]) return true;
                return false;
            }
            if (this[1, h - 1, v] || this[1, h - 1, v - 1])
                return true;
            
            return false;
        }

        private bool IsObstacleUP(int h, int v)
        {
            if (v == 0)
                return true;
            if (h == 0)
            {
                if (this[0, h, v - 1]) return true;
                return false;
            }
            else if (h == _size - 1)
            {
                if (this[0, h - 1, v - 1]) return true;
                return false;
            }
            if (this[0, h - 1, v - 1] || this[0, h - 1, v - 1])
                return true;

            return false;
        }

        private bool IsObstacleRIGHT(int h, int v)
        {
            if (h == _size - 1)
                return true;
            if (v == 0)
            {
                if (this[1, h + 1, v]) return true;
                return false;
            }
            else if (v == _size - 1)
            {
                if (this[1, h + 1, v - 1]) return true;
                return false;
            }
            if (this[1, h, v] || this[1, h, v - 1] )
                return true;
            return false;
        }

        private bool IsObstacleDOWN(int h, int v)
        {
            if (v == _size - 1)
                return true;
            if (h == 0)
            {
                if (this[0, h, v]) return true;
                return false;
            }
            else if (v == _size - 1)
            {
                if (this[0, h - 1, v]) return true;
                return false;
            }
            if (this[0, h, v] || this[0, h - 1, v] )
                return true;
            return false;
        }

        private bool MoveToken(int h, int v)
        {
            Token currT = players[_turn];

            if (h == currT[0] && v == currT[1])
                return false;
            if (h < 0 || h >= _size || v < 0 || v >= _size)
                return false;
            //if (h != currT[0] && v != currT[1])
            //    return false;

            if (h != currT[0])
            {
                if (h > currT[0])
                {
                    if (IsObstacleRIGHT(currT[0], currT[1]))
                        return false;
                }
                else
                {

                    if (IsObstacleLEFT(currT[0], currT[1]))
                        return false;
                }
            }

            else if (v != currT[1])
            {
                if (v > currT[1])
                {
                    if (IsObstacleDOWN(currT[0], currT[1]))
                        return false;
                }
                else
                {
                    if (IsObstacleUP(currT[0], currT[1]))
                        return false;
                }
            }


            if (weights[currT[0] + currT[1] * _size, h + v * _size] == 1)//((currT[0] - h <= 1 && currT[0] - h >= -1) || (currT[1] - v <= 1 && currT[1] - v >= -1))
            {
                if (this[h, v].Filled != 0) return false;

                ShiftToken(h, v);
                return true;
            }
            else if (weights[currT[0] + currT[1] * _size, h + v * _size] == 2)
            {
                if (this[h, v].Filled != 0) return false;
                
                int edge = paths[currT[0] + currT[1] * _size, h + v * _size];
                int h0 = edge % _size;
                int v0 = edge / _size;

                if (h == currT[0] || v == currT[1])
                {
                    if (this[h0, v0].Filled != _turn + 1)
                    {
                        ShiftToken(h, v);
                        return true;
                    }
                }
                else
                {
                    if (this[h0, v0].Filled != _turn + 1 && (IsObstacleUP(h0, v0) || IsObstacleDOWN(h0, v0)))
                    {
                        if ((!IsObstacleLEFT(h0, v0) && h0 > h) || (!IsObstacleRIGHT(h0, v0) && h0 < h))
                        {
                            ShiftToken(h, v);
                            return true;
                        }
                    }
                    //else if (this[h0, v0].Filled != _turn + 1 && IsObstacleDOWN(h0, v0))
                    //{

                    //}
                }
            }
            return false;
        }

        private void ShiftToken(int h, int v)
        {
            Token currT = players[_turn];
            this[currT[0], currT[1]].Filled = 0;
            this[h, v].Filled = _turn + 1;
            currT[0] = h;
            currT[1] = v;
        }
    }
}
