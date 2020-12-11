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
using DAL.Domain;
using BLL.Interfaces;

namespace PL
{
    /// <summary>
    /// Interaction logic for DeleteAccount.xaml
    /// </summary>
    public partial class DeleteAccount : Window
    {
        private IKernel kernel;
        private BankAccount account;
        public DeleteAccount(IKernel kernel, BankAccount acc)
        {
            InitializeComponent();
            this.kernel = kernel;
            this.account = acc;
        }

        private void ChangeEmailCancelButton_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        private void DeleteAccountOKButton_Click(object sender, RoutedEventArgs e)
        {
            kernel.Get<IBankAccountService>().DeleteAccount(account);
            Close();
        }
    }
}
