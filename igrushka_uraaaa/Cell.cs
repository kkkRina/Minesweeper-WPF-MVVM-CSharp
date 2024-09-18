using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace igrushka_uraaaa
{
    public class Cell(byte row, byte col)
    {
        public byte Row { get => row; set => row = value; }
        public byte Col { get => col; set => col = value; }
        public bool IsOpen { get; set; }
        public CellType Type { get; set; }
        public bool IsMarked { get; set; }
        public byte MineCount {  get; set; }
    }
    
}
