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
using System.Linq;
using DAL.Domain;
using DAL.Interfaces;
using DAL.Enums;

namespace PL
{
    /// <summary>
    /// Interaction logic for AddAccount.xaml
    /// </summary>
    public partial class AddAccount : Window
    {
        private IKernel kernel;
        public AddAccount(IKernel kernel)
        {
            InitializeComponent();
            this.kernel = kernel;
            AddAccountCurrencyComboBox.ItemsSource = kernel.Get<IUnitOfWork>().Repository<Currency>().Get().Select(x => x.Code).ToList<String>();
        }

        private void AddAccountCancelButton_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        private void AddAccountOKButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
