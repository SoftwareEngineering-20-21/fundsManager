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
            MainForm win2 = new MainForm(kernel);
            win2.Show();
            Close();
        }
        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            Registration win2 = new Registration(kernel);
            win2.Show();
            Close();
        }
    }
}
