using NLog;
using System;
using System.Windows;
using UIModule.ViewModels;

namespace UIModule.Views
{
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
            AuthorizationWindowViewModel.CloseAction = new Action(this.Close);
        }
    }
}
