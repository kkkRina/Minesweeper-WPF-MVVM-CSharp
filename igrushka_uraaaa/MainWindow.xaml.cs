﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace igrushka_uraaaa
{
    public partial class MainWindow : Window
    {
        private LoginViewModel _loginViewModel = new();
        public MainWindow()
        {
            InitializeComponent();

            Binding bNick = new Binding(nameof(User.Nick))
            {
                Source =_loginViewModel.CurUser
            };
            TbNick.SetBinding(TextBox.TextProperty, bNick);

            Binding bName = new Binding(nameof(User.Name))
            {
                Source = _loginViewModel.CurUser
            };
            TbName.SetBinding(TextBox.TextProperty, bName);

            Binding bBirth = new Binding(nameof(User.Birth))
            {
                Source = _loginViewModel.CurUser
            };
            DpBirth.SetBinding(DatePicker.SelectedDateProperty, bBirth);

            BtnLogin.Command =_loginViewModel.LoginCommand;
            BtnDel.Command = _loginViewModel.RemoveCommand;
        }
    }
}