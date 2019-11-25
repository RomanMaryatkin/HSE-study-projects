using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using TransportSchedule.Classes;
using TransportSchedule.Classes.Interfaces;

namespace TransportSchedule.UI {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
    {
		IRepository _repo = Factory.Instance.GetDbRepository();

        public MainWindow()
        {
            InitializeComponent();

			BuildStationsList();
		}

		private void BuildStationsList() {
			// Combine favourites with all stations to build a single list
			var stations = _repo.Favourites.OrderBy(f => f.Description).Select(f => new StationViewModel { Favourite = f, Station = f.Station })
				.Concat(_repo.Stations.OrderBy(st => st.Name).Select(st => new StationViewModel { Station = st }))
				.ToList();
			comboBoxStations.ItemsSource = stations;
		}

		private void comboBoxStations_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			var selection = comboBoxStations.SelectedItem as StationViewModel;
			if (comboBoxStations.SelectedItem != null)
				dataGridSchedule.ItemsSource = _repo.GetSchedule(selection.Station);
		}
	}
}
