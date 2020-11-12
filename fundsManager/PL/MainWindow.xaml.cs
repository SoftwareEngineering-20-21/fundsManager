using BLL.Services;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        public void TextBoxGotFocus(object sender, RoutedEventArgs e)
        {
           
            //TextBox tb = (TextBox)sender;
            //tb.Text = string.Empty;
            //tb.GotFocus -= TextBoxGotFocus;
        }
        public void PasswordBoxGotFocus(object sender, RoutedEventArgs e)
        {
            //PasswordBox tb = (PasswordBox)sender;
            //tb.Password = string.Empty;
            //tb.GotFocus -= PasswordBoxGotFocus;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Registration win2 = new Registration();
            MainWindow win1 = new MainWindow();
            SystemCommands.CloseWindow(this);
            win2.Show();
            
           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
