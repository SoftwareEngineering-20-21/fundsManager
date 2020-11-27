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
    /// Interaction logic for MainForm.xaml
    /// </summary>
    public partial class MainForm : Window
    {
        private IKernel kernel;
        public MainForm(IKernel kernel)
        {
            InitializeComponent();
            this.kernel = kernel;
           
        }

        private void SettingsChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            Change_Password CnahgePassword = new Change_Password(kernel);
            CnahgePassword.Show();
        }

        private void SettingsChangeEmailButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeEmail CnahgeEmail = new ChangeEmail(kernel);
            CnahgeEmail.Show();
        }

        private void SettingsChangePhoneNumberButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SettingsLogOutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win1 = new MainWindow();
            win1.Show();
            SystemCommands.CloseWindow(this);
        }
    }
}
