using BLL.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    public partial class Registration : Window
    {
        private IKernel kernel;
        public Registration(IKernel kernel)
        {
            InitializeComponent();
            this.kernel = kernel;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win1 = new MainWindow();
            win1.Show();
            SystemCommands.CloseWindow(this);
        }

        private void RegSignUpButton_Click(object sender, RoutedEventArgs e)
        {
            IUserService userService = kernel.Get<IUserService>();
            MainForm win2 = new MainForm(kernel);
            win2.Show();
            Close();
        }
    }
}
