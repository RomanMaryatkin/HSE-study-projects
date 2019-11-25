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
using System.Windows.Shapes;
using TransportSchedule.Classes;
using TransportSchedule.Classes.Interfaces;

namespace TransportSchedule.UI {
	/// <summary>
	/// Interaction logic for LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window {
		IRepository _repo = Factory.Instance.GetDbRepository();

		public LoginWindow() {
			InitializeComponent();
		}

		private void ButtonRegister_Click(object sender, RoutedEventArgs e) {
			var registerWindow = new RegisterWindow();
			registerWindow.RegistrationFinished += RegisterWindow_RegistrationFinished;
			registerWindow.Show();

			Hide();
		}

		private void RegisterWindow_RegistrationFinished() {
			Show();
		}

		private void ButtonLogin_Click(object sender, RoutedEventArgs e) {
			if (_repo.Authorize(textBoxLogin.Text, passwordBox.Password)) {
				var mainWindow = new MainWindow();
				mainWindow.Show();
				Close();
			}
			else {
				MessageBox.Show("Incorrect login/password");
			}
		}
	}
}
