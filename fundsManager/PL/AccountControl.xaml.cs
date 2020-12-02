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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for AccountControl.xaml
    /// </summary>
    public partial class AccountControl : UserControl
    {
        private IKernel kernel;
        public AccountControl(IKernel kernel)
        {
            InitializeComponent();
            this.kernel = kernel;
        }

        private void AccountOptionsShare_Click(object sender, RoutedEventArgs e)
        {
           
        }
        private void AccountOptionsDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
