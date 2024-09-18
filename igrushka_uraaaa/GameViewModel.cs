using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace igrushka_uraaaa
{
    public class GameViewModel
    {
        public MineField Field { get; } = new();
        private Command? _startCommand;
        public Command StartCommand => _startCommand ??= new Command(
            _ => { Field.InitializeMineField(); });

        public GameViewModel()
        {
            CellInfo.CellUpdateNeeded += Field.UpdateCells;
            CellInfo.CellOpened += Field.OpenNeighbours;
        }
    }

}
