using BLL.Interfaces;
using BLL.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public partial class MainWindow : Window
    {
        private IKernel kernel;
        public MainWindow()
        {
            InitializeComponent();
            MainForm registration = new MainForm(kernel);
            registration.Show();
            var registrations = new NinjectRegistrations();
            this.kernel = new StandardKernel(registrations);
            
        }

        public MainWindow(IKernel kernel)
        {
            this.kernel = kernel;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            IUserService userService = kernel.Get<IUserService>();
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;
            if (email.Length == 0 || password.Length == 0)
            {
                ErrorLabel.Content = "The email or password field can not be empty.";
                return;
            }
            if (!userService.IsValidMail(email))
            {
                ErrorLabel.Content = "The email is not a valid email address.";
                return;
            }
            try
            {
                var user = userService.Login(email, password);
                MainForm mainForm = new MainForm(kernel);
                mainForm.Show();
                Close();
            }
            catch (ArgumentException exc)
            {
                ErrorLabel.Content = exc.Message;
            }
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration(kernel);
            registration.Show();
            Close();
        }
    }
}
