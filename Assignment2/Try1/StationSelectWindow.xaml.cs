using Core;
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
using Try1.Core;

namespace Try1.UI
{
    /// <summary>
    /// Логика взаимодействия для StationSelectWindow.xaml
    /// </summary>
    public partial class StationSelectWindow : Window
    {
       
        Repository _repository { get; set; }
        User _user { get; set; }
        Station _station = new Station();
        TimeCalculator _timeCalculator = new TimeCalculator();
        //List<string> FavStationsNames = new List<string>();

        public StationSelectWindow(Repository repository, User user/*, TimeCalculator timeCalculator*/)
        {
            InitializeComponent();

            _repository = repository;
            _user = user;
            //_timeCalculator = timeCalculator;

            //foreach (var favStId in _user.FavStationsIds)
            //{
            //    foreach (var station in _repository.Stations)
            //    {
            //        if (station.Id == favStId)
            //        {
            //            FavStationsNames.Add(station.StationName);
            //        }
            //    }
            //}

            comboBoxStations.ItemsSource = _repository.Stations;
            comboBoxFav.ItemsSource = _user.FavStations;

            comboBoxStations.SelectionChanged += ComboBoxStation_SelectionChanged;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void comboBoxFav_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dataGridRoutes.ItemsSource = null;
            _station = comboBoxFav.SelectedItem as Station;
            if (_station != null)
            {
                dataGridRoutes.ItemsSource = _timeCalculator.TheAnswers(_repository, _station);
            }
        }

        private void ButtonAddToFovourites_Click(object sender, RoutedEventArgs e)
        {
            var selectedStation = comboBoxStations.SelectedItem as Station;

            if (_user.FavStations == null)
            {
                _user.FavStations = new List<Station>();
                _user.FavStations.Add(selectedStation);
                _repository.Save();
                comboBoxFav.ItemsSource = null;
                comboBoxFav.ItemsSource = _user.FavStations;
            }
            else
            {
                Station checkStation = _user.FavStations.FirstOrDefault(s => s == selectedStation);
                if(checkStation == null)
                {
                    _user.FavStations.Add(selectedStation);
                    _repository.Save();
                    comboBoxFav.ItemsSource = null;
                    comboBoxFav.ItemsSource = _user.FavStations;
                }
                else
                {
                    MessageBox.Show("You have already added this station to favourites");
                }
            }
        }

        private void ButtonRemoveFromFavourites_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxFav.SelectedItem != null)
            {
                var selectedFavouriteStation = comboBoxFav.SelectedItem as Station;
                //FavStationsNames.Remove(selectedFavouriteStation);
                _user.FavStations.Remove(selectedFavouriteStation);
                _repository.Save();
                comboBoxFav.ItemsSource = null;
                comboBoxFav.ItemsSource = _user.FavStations;
            }
        }

        private void ComboBoxStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dataGridRoutes.ItemsSource = null;
            _station = comboBoxStations.SelectedItem as Station;
            if (_station != null)
            {
                dataGridRoutes.ItemsSource = _timeCalculator.TheAnswers(_repository, _station);
            }
        }
    }
}
