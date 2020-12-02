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
    /// <summary>
    /// Interaction logic for ShareAccount.xaml
    /// </summary>
    public partial class ShareAccount : Window
    {
        private IKernel kernel;
        public ShareAccount(IKernel kernel)
        {
            InitializeComponent();
            this.kernel = kernel;
        }

        private void ChangeEmailCancelButton_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }
    }
}
