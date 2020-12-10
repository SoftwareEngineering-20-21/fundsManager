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
using System.Threading.Tasks;

namespace PL
{
    /// <summary>
    /// Interaction logic for ShareAccount.xaml
    /// </summary>
    public partial class ShareAccount : Window
    {
        private IKernel kernel;
        private BankAccount account;

        public ShareAccount(IKernel kernel, BankAccount acc)
        {
            InitializeComponent();
            this.kernel = kernel;
            this.account = acc;
        }

        private void ChangeEmailCancelButton_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        private void ShareAccountOKButton_Click(object sender, RoutedEventArgs e)
        {
            string email = ShareAccountEmailTextBox.Text;
            
            if (email == "" || !kernel.Get<IUserService>().IsValidMail(email))
            {
                MessageBox.Show("Please enter valid email");
                return;
            }
            var answer = kernel.Get<IBankAccountService>().ShareAccount(account, email);
            answer.Wait();
            if (!answer.Result)
            {
                MessageBox.Show("User not found");
                return;
            }
            Close();
        }
    }
}
