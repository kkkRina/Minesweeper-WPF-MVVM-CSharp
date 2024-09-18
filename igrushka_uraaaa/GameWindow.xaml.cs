using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace igrushka_uraaaa
{
    public partial class GameWindow : Window
    {
        private readonly GameViewModel _viewModel;
        public GameWindow()
        {
            _viewModel = new();
            InitializeComponent();
            DataContext = _viewModel;
            MineField.ItemsSource = _viewModel.Field;
            BtnStart.Command = _viewModel.StartCommand;
        }


        private void MineField_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            _viewModel.Field.Width = (int)e.NewSize.Width;
            _viewModel.Field.Height = (int)e.NewSize.Height;
        }
    }
}
