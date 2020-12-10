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
using LiveCharts;
using LiveCharts.Wpf;
using BLL.Interfaces;
using System.Linq;
using BLL.Models;
using DAL.Interfaces;
using DAL.Domain;
using DAL.Enums;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainForm.xaml
    /// </summary>
    public partial class MainForm : Window
    {
        private IKernel kernel;

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public MainForm(IKernel kernel)
        {
            InitializeComponent();
            this.kernel = kernel;
            var statsService = kernel.Get<IStatisticsService>();
            statsService.CurrentUser = kernel.Get<IUserService>().CurrentUser;
            var bankAccountService = kernel.Get<IBankAccountService>();
            bankAccountService.CurrentUser = kernel.Get<IUserService>().CurrentUser;

            TransactionsTypeComboBox.DropDownClosed += new System.EventHandler(TransactionTypeChanged);

            SeriesCollection = new SeriesCollection
                {
                    new LineSeries
                    {
                        Values = new ChartValues<double> { 3, 5, 7, 4 }
                    }
                };
            SeriesCollection.Add(new LineSeries { Values = new ChartValues<double> { 1, 8, 2, 5 } });
            DataContext = this;
            //AccountControl accountControl = new AccountControl(kernel);
            //AccSPanel.Items.Add(accountControl);
            //AccountControl accountControl1 = new AccountControl(kernel);
            //AccSPanel.Items.Add(accountControl1);
            //AccountControl accountControl2 = new AccountControl(kernel);
            //AccSPanel.Items.Add(accountControl2);
            //AccountControl accountControl13 = new AccountControl(kernel);
            //AccSPanel.Items.Add(accountControl13);
            //AccountControl accountControl4 = new AccountControl(kernel);
            //AccSPanel.Items.Add(accountControl4);
            //AccountControl accountControl15 = new AccountControl(kernel);
            //AccSPanel.Items.Add(accountControl15);
            //AccountControl accountControl6 = new AccountControl(kernel);
            //AccSPanel.Items.Add(accountControl6);
            //AccountControl accountControl7 = new AccountControl(kernel);
            //AccSPanel.Items.Add(accountControl7);
            //AccountControl accountControl8 = new AccountControl(kernel);
            //AccSPanel.Items.Add(accountControl8);
            //AccountControl accountControl19 = new AccountControl(kernel);
            //AccSPanel.Items.Add(accountControl19);
            AccountControl accountControl = new AccountControl(kernel);
            AccSPanel.Children.Add(accountControl);
            AccountControl accountControl1 = new AccountControl(kernel);
            AccSPanel.Children.Add(accountControl1);
            AccountControl accountControl2 = new AccountControl(kernel);
            AccSPanel.Children.Add(accountControl2);
            AccountControl accountControl13 = new AccountControl(kernel);
            AccSPanel.Children.Add(accountControl13);
            AccountControl accountControl4 = new AccountControl(kernel);
            AccSPanel.Children.Add(accountControl4);
            AccountControl accountControl15 = new AccountControl(kernel);
            AccSPanel.Children.Add(accountControl15);
            AccountControl accountControl6 = new AccountControl(kernel);
            AccSPanel.Children.Add(accountControl6);
            AccountControl accountControl7 = new AccountControl(kernel);
            AccSPanel.Children.Add(accountControl7);
            AccountControl accountControl8 = new AccountControl(kernel);
            AccSPanel.Children.Add(accountControl8);
            AccountControl accountControl19 = new AccountControl(kernel);
            AccSPanel.Children.Add(accountControl19);
            Load_graphic();
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
            ChangePhoneNumber ChangePhoneNumber = new ChangePhoneNumber(kernel);
            ChangePhoneNumber.Show();
        }

        private void SettingsLogOutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win1 = new MainWindow();
            win1.Show();
            SystemCommands.CloseWindow(this);
        }

        private void AddAccountButton_Click(object sender, RoutedEventArgs e)
        {
            AddAccount AddAccount = new AddAccount(kernel);
            AddAccount.Show();
        }


        private void MainForm1_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.Height / this.Width != 0.5625)
            {
                this.Height = this.Width * 0.5625;
            }
        }
        private void Load_graphic()
        {
            IStatisticsService statsService = kernel.Get<IStatisticsService>();
            var stats = statsService.GetIncomeStatisticsFullPeriod().ToList<StatisticsItem>();
            var stats2 = statsService.GetExpenceStatisticsFullPeriod().ToList<StatisticsItem>();
            ChartValues<decimal> chartIncome = new ChartValues<decimal>();
            ChartValues<decimal> chartExpence = new ChartValues<decimal>();
            stats.ForEach(x => chartIncome.Add(x.Value));
            stats2.ForEach(x => chartExpence.Add(x.Value));
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Income",
                    Values = chartIncome
                },
                  new LineSeries
                {
                    Title = "Expence",
                    Values = chartExpence
                }
            };

            DataContext = this;
        }

        private void TransactionConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            var service = kernel.Get<IBankAccountService>();
            string FromName = TransactionsFromComboBox.Text;
            string ToName = TransactionsToComboBox.Text;
            if (FromName == "" || ToName == "" || !Decimal.TryParse(TransactionsAmountTextBox.Text, out decimal amount))
            {
                MessageBox.Show("Fields can not be empty");
                return;
            }
            if (amount <= 0)
            {
                MessageBox.Show("Amount can not be negative or equal to 0");
                return;
            }
            var all = service.GetAllUserAccounts();
            BankAccount from = all.FirstOrDefault(x => x.Name == TransactionsFromComboBox.Text);
            BankAccount to = all.FirstOrDefault(x => x.Name == TransactionsToComboBox.Text);
            DateTime dateTime = (DateTime)TransactionsDatePicker.SelectedDate;
            try
            {
                service.MakeTransaction(from, to, amount, dateTime, "");
                MessageBox.Show("Done");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void TransactionTypeChanged(object sender, System.EventArgs e)
        {
            var service = kernel.Get<IBankAccountService>();
            var all = service.GetAllUserAccounts();
            if (TransactionsTypeComboBox.Text == "Income")
            {
                TransactionsToComboBox.ItemsSource = all.Where(x => x.Type == AccountType.Current).Select(x => x.Name).ToList<string>();
                TransactionsFromComboBox.ItemsSource = all.Where(x => x.Type == AccountType.Income).Select(x => x.Name).ToList<string>();
            }
            else if (TransactionsTypeComboBox.Text == "Expences") 
            {
                TransactionsToComboBox.ItemsSource = all.Where(x => x.Type == AccountType.Expence).Select(x => x.Name).ToList<string>();
                TransactionsFromComboBox.ItemsSource = all.Where(x => x.Type == AccountType.Current).Select(x => x.Name).ToList<string>();
            }
        }
    }
}
