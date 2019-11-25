using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using Try1.Core;

namespace Try1.UI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Repository _repo = new Repository();
        public User _user { get; set; }
        public MainWindow()
        {
            InitializeComponent();


        }

        public static string GetHash(string password)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(
            password));
            return Convert.ToBase64String(hash);
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterWindow(_repo);
            registerWindow.ShowDialog();
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace((LoginTextBox.Text)) && !string.IsNullOrWhiteSpace((PasswordBoxPassword.Password)))
            {
                _user = _repo.Users.FirstOrDefault(u => ((u.Login == LoginTextBox.Text) && (u.Password == GetHash(PasswordBoxPassword.Password))));
                if (_repo.Users.Any(u => ((u.Login == LoginTextBox.Text) && (u.Password == GetHash(PasswordBoxPassword.Password)))))
                {
                    var stationSelectWindow = new StationSelectWindow(_repo, _user);
                    stationSelectWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Invalid Login or Password");
                }
            }
            else
            {
                MessageBox.Show("You have Left one of the fields empty");
            }
        }

        private void LoginTextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            LoginTextBox.Text = "";
        }
    }
}
