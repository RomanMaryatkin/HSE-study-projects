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
using System.Windows.Shapes;
using Try1.Core;

namespace Try1.UI
{
    /// <summary>
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        Repository _repository { get; set; }
        User _user { get; set; }

        public RegisterWindow(Repository repository)
        {
            InitializeComponent();
            _repository = repository;
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
            if(!string.IsNullOrWhiteSpace((TextBoxFullName.Text))&& !string.IsNullOrWhiteSpace((TextBoxLogin.Text))&& !string.IsNullOrWhiteSpace((PasswordBoxPassword.Password)))
            {
                if (_repository.Users == null)
                {
                    _user = new User();
                    _repository.Users = new List<User>();
                    _user.FullName = TextBoxFullName.Text;
                    _user.Login = TextBoxLogin.Text;
                    _user.Password = GetHash(PasswordBoxPassword.Password);
                    _repository.Users.Add(_user);
                    _repository.Save();
                    MessageBox.Show("You have successfully registered, now navigate no main window and log in");
                }
                else
                {
                    _user = new User();
                    //_repository.Users = new List<User>();
                    _user.FullName = TextBoxFullName.Text;
                    _user.Login = TextBoxLogin.Text;
                    _user.Password = GetHash(PasswordBoxPassword.Password);
                    _repository.Users.Add(_user);
                    _repository.Save();
                    MessageBox.Show("You have successfully registered, now navigate no main window and log in");
                }
            }
            else
            {
                MessageBox.Show("You have left one of the field empty");
            }
        }

        private void TextBoxFullName_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBoxFullName.Text = "";
        }

        private void TextBoxLogin_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBoxLogin.Text = "";
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
