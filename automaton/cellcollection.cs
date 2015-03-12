using System;
using System.Windows;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace automaton
{
    class CellCollection
    {
        private int _size;
        private cell[,] _cells;
        private bool[] _nextGeneration;

        public CellCollection(int size)
        {
            _size = size;
            _cells = new cell[_size,_size];
            _nextGeneration = new bool[_size];

            for (int row = 0; row < _size; row++)
            {
                for (int column = 0; column < _size; column++)
                {
                    _cells[row, column] = new cell();
                }
            }
            
        }


        public cell this[int row, int column]
        {
            get
            {
                if (row < 0 || row >= _size || column < 0 || column >= _size)
                {
                    throw new ArgumentOutOfRangeException();
                }

                return _cells[row, column];
            }
        }

        public int Size
        {
            get { return _size; }
            set { _size = value; }
        }

        private string getBase2(int i)
        {
            string s = Convert.ToString(i, 2);
            while (s.Length < 8)
            { s = "0" + s; }
            return s;
        }

        private int BoolToInt(bool b){

            return b == true ? 1 : 0;
        }

        private byte getCells(int gen, int index)
        {
            byte nb;
            int i1 = index - 1,
                i2 = index,
                i3 = index + 1;

            if (i1 < 0) i1 = _size - 1;
            if (i3 >= _size) i3 -= _size;

            nb = Convert.ToByte(
                4 * Convert.ToByte(BoolToInt(_cells[gen,i1].IsAlive)) +
                2 * Convert.ToByte(BoolToInt(_cells[gen,i2].IsAlive)) +
                Convert.ToByte(BoolToInt(_cells[gen,i3].IsAlive)));

            return nb;
        }



        public void UpdateLife(int rule)
        {

            string rl = getBase2(rule);
            _cells[0,_size / 2].IsAlive = true;
   

            for (int gen = 1; gen < _size; gen++)
            {

                int i = 0;
                while (true)
                {
                    byte b = getCells(gen-1,i);
                    _nextGeneration[i] = ('1' == (rl[7 - b])) ? true : false;
                    if (++i == _size) break;
                }

                i = 0;
                foreach (bool b in _nextGeneration)
                     _cells[gen,i++].IsAlive = b;
            }
            
        }

        public void kill()
        {
            for (int row = 0; row < _size; row++)
            {
                for (int column = 0; column < _size; column++)
                {
                    _cells[row, column].IsAlive=false;
                }
            }

        }


     
    }
}
