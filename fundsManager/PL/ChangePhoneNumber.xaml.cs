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
    /// Interaction logic for ChangePhoneNumber.xaml
    /// </summary>
    public partial class ChangePhoneNumber : Window
    {
        private IKernel kernel;
        public ChangePhoneNumber(IKernel kernel)
        {
            InitializeComponent();
            this.kernel = kernel;
        }

        private void ChangePhoneNumberCancelButton_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }
    }
}
