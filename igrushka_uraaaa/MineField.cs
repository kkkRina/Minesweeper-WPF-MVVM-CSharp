using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace igrushka_uraaaa
{
    public class MineField : List<CellInfo>, INotifyPropertyChanged, INotifyCollectionChanged
    {
        private int _width;
        private int _height;

        private readonly byte _rows;
        private readonly byte _cols;
        public byte Rows
        {
            get => _rows;
            init => _rows = byte.Min(byte.Max(value, 6), 50);
        }

        public byte Cols
        {
            get => _cols;
            init => _cols = byte.Min(byte.Max(value, 6), 50);
        }

        public int Width
        {
            get => _width;
            set
            {
                SetField(ref _width, value);
                UpdateCells();
            }
        }

        public int Height
        {
            get => _height;
            set
            {
                SetField(ref _height, value);
                UpdateCells();
            }
        }
        private int CellSize => int.Min(Height / Rows, Width / Cols);
        private int LShift => (Width - CellSize * Cols) / 2;
        private int TShift => (Height - CellSize * Rows) / 2;
        private CellInfo? this[int row, int col]
        {
            get
            {
                if (row < 0 || row >= Rows || col < 0 || col >= Cols) return null;
                return this[row * Cols + col];
            }
        }
        public MineField(byte rows = 15, byte cols = 15)
        {
            Rows = rows;
            Cols = cols;
            FillField();
        }
        private void FillField()
        {
            Clear();
            for (byte i = 0; i < Rows; i++)
            {
                for (byte j = 0; j < Cols; j++)
                {
                    Add(new CellInfo(i, j));
                }
            }
        }
        private List<Cell> GetNeighbours(Cell cell)
        {
            List<Cell> neighbours = new List<Cell>();
            for (var i = -1; i <= 1; i++)
            {
                for (var j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;
                    var t = this[cell.Row + i, cell.Col + j];
                    if (t != null)
                    {
                        neighbours.Add(t);
                    }
                }
            }

            return neighbours;
        }
        private Random _rand = new Random();
        private int _mineCount = 20;
        public void InitializeMineField() // минирует поле
        {
            FillField();
            for (int i=0; i<_mineCount; i++)
            {
                int row, col;
                do
                {
                    col = _rand.Next(Cols);
                    row = _rand.Next(Rows);
                } while (this[row, col].Type == CellType.Mine);
                this[row, col].Type = CellType.Mine;
                GetNeighbours(this[row, col]).ForEach(nc => {
                    if (nc.Type == CellType.Empty)
                    {
                        nc.Type = CellType.Number;
                    }
                    nc.MineCount++;
                });
            }
            UpdateCells();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void UpdateCells()
        {
            if (Height > 0 && Width > 0)
            {
                foreach (var cell in this)
                {
                    cell.Left = CellSize * cell.Col + LShift;
                    cell.Top = CellSize * cell.Row + TShift;
                    cell.Size = CellSize;
                }
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }
        public void OpenCell(string cellId)
        {
            Find(c => c.Id == cellId).IsOpen = true;
            UpdateCells();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public void OpenNeighbours(Cell cell)
        {
            if (cell.Type == CellType.Empty)
            {
                var n = GetNeighbours(cell);
                foreach (var item in n)
                {
                    
                    if (!item.IsOpen)
                    {
                        item.IsOpen = true;
                        OpenNeighbours(item);
                    }
                }
            }
        }

        public event NotifyCollectionChangedEventHandler? CollectionChanged;

    }
}
