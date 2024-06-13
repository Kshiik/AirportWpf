using AirportWpf.Controllers;
using AirportWpf.Model;
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

namespace AirportWpf.View
{
    /// <summary>
    /// Логика взаимодействия для FlightPage.xaml
    /// </summary>
    public partial class FlightPage : Page
    {
        Core db = new Core();
        List<Flight> arrayFlight;

        int idRole = 0;
        int idCurrentPassenger = 0;
        public FlightPage(int idPassenger)
        {
            InitializeComponent();
            idRole = Properties.Settings.Default.idRole;

            idCurrentPassenger = idPassenger;

            if (idCurrentPassenger == 0)
            {
                if (idRole == 1)
                {
                    AddFlightButton.Visibility = Visibility.Visible;
                    FlightListViewDispetcher.Visibility = Visibility.Collapsed;
                    FlightListViewAdmin.Visibility = Visibility.Visible;
                    FlightListViewChoice.Visibility = Visibility.Collapsed;
                    arrayFlight = db.context.Flight.ToList();
                    FlightListViewAdmin.ItemsSource = arrayFlight;
                }
                if (idRole == 2)
                {
                    AddFlightButton.Visibility = Visibility.Visible;
                    FlightListViewAdmin.Visibility = Visibility.Collapsed;
                    FlightListViewDispetcher.Visibility = Visibility.Visible;
                    FlightListViewChoice.Visibility = Visibility.Collapsed;
                    arrayFlight = db.context.Flight.ToList();
                    FlightListViewDispetcher.ItemsSource = arrayFlight;
                }
            }
            else
            {
                AddFlightButton.Visibility = Visibility.Collapsed;
                FlightListViewAdmin.Visibility = Visibility.Collapsed;
                FlightListViewDispetcher.Visibility = Visibility.Collapsed;
                FlightListViewChoice.Visibility = Visibility.Visible;
                arrayFlight = db.context.Flight.ToList();
                FlightListViewChoice.ItemsSource = arrayFlight;
            }

        }

        private void AddFlightButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddFlightPage());
        }

        private void TicketsButton_Click(object sender, RoutedEventArgs e)
        {
            Button activeButton = sender as Button;
            Flight activeFlight = activeButton.DataContext as Flight;
            NavigationService.Navigate(new TicketsPage(activeFlight.IDFlight, 0));
        }

        private void PassengersButton_Click(object sender, RoutedEventArgs e)
        {
            Button activeButton = sender as Button;
            Flight activeFlight = activeButton.DataContext as Flight;
            NavigationService.Navigate(new PassengersPage(activeFlight.IDFlight));
        }

        private void DeleteFlightButton_Click(object sender, RoutedEventArgs e)
        {
            Button selectedButton = sender as Button;
            Flight activeFlight = selectedButton.DataContext as Flight;
            MessageBoxResult rez = MessageBox.Show($"Вы уверены, что хотите удалить рейс {activeFlight.NumberFlight}?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (rez == MessageBoxResult.Yes)
            {
                try
                {
                    FlightController.DeleteFlight(activeFlight.IDFlight);
                    MessageBox.Show("Данные успешно удалены. \nВозвращение на главную страницу", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.NavigationService.Navigate(new FlightPage(0));
                }
                catch (Exception)
                {
                    MessageBox.Show("Возникла ошибка при удалении. \nВозвращение на главную страницу", "Удаление", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.NavigationService.Navigate(new FlightPage(0));
                }
            }
        }

        private void AllPassengerButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PassengersPage(0));
        }

        private void RegistrationPassengerButton_Click(object sender, RoutedEventArgs e)
        {
            Button activeButton = sender as Button;
            Flight activeFlight = activeButton.DataContext as Flight;
            if (activeFlight.NumberOfAvailableSeats != 0)
                NavigationService.Navigate(new AddPassengersPage(activeFlight.IDFlight));
            else
                MessageBox.Show("На этот рейс нет свободных мест");
        }

        private void CancleTicketsButton_Click(object sender, RoutedEventArgs e)
        {
            Button selectedButton = sender as Button;
            Flight activeFlight = selectedButton.DataContext as Flight;
            MessageBoxResult rez = MessageBox.Show($"Вы уверены, что хотите отменить билеты пассажиров рейса {activeFlight.NumberFlight}?", "Отмена билетов", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (rez == MessageBoxResult.Yes)
            {
                try
                {
                    Ticket_PassengerController.CancleTickets(activeFlight.IDFlight);
                    MessageBox.Show("Билеты пассажиров отменены. \nВозвращение на главную страницу", "Отмена билетов", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.NavigationService.Navigate(new FlightPage(0));
                }
                catch (Exception)
                {
                    MessageBox.Show("Возникла ошибка при отмене билетов. \nВозвращение на главную страницу", "Отмена билетов", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.NavigationService.Navigate(new FlightPage(0));
                }
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var flightsResult = db.context.Flight.ToList();
            var airportResult = db.context.Airport.ToList();
            if (FilterCombBox.SelectedIndex == 0)
            {
                flightsResult = flightsResult.Where(x => x.NumberFlight.Contains(SearchTextBox.Text.ToUpper())).ToList();
                FlightListViewAdmin.ItemsSource = flightsResult;
                FlightListViewDispetcher.ItemsSource = flightsResult;
            }
            if (FilterCombBox.SelectedIndex == 1)
            {
                var airports = airportResult.Where(x => x.NameAirport.ToLower().Contains(SearchTextBox.Text.ToLower())).Select(x => x.IDAirport).ToList();
                flightsResult = flightsResult.Where(x => airports.Contains((int)x.DepartureAirportID)).ToList();
                FlightListViewAdmin.ItemsSource = flightsResult;
                FlightListViewDispetcher.ItemsSource = flightsResult;
            }
            if (FilterCombBox.SelectedIndex == 2)
            {
                var airports = airportResult.Where(x => x.NameAirport.ToLower().Contains(SearchTextBox.Text.ToLower())).Select(x => x.IDAirport).ToList();
                flightsResult = flightsResult.Where(x => airports.Contains((int)x.ArrivalAirportID)).ToList();
                FlightListViewAdmin.ItemsSource = flightsResult;
                FlightListViewDispetcher.ItemsSource = flightsResult;
            }
        }

        private void EditFlightButton_Click(object sender, RoutedEventArgs e)
        {
            Button activeButton = sender as Button;
            Flight activeFlight = activeButton.DataContext as Flight;
            NavigationService.Navigate(new EditFlightPage(activeFlight.IDFlight, activeFlight.NumberFlight, activeFlight.AirlineID, activeFlight.DepartureAirportID, activeFlight.ArrivalAirportID, activeFlight.DepartureDate, activeFlight.DepartureTime, activeFlight.TotalNumberOfSeats, activeFlight.NumberOfAvailableSeats));
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddUsersPage());
        }

        private void TicketPageButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TicketsPage( 0, 0));
        }

        private void FlightPageButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FlightPage(0));
        }

        private void FlightChoiceButton_Click(object sender, RoutedEventArgs e)
        {
            Button activeButton = sender as Button;
            Flight activeFlight = activeButton.DataContext as Flight;
            NavigationService.Navigate(new AddTicketPage(activeFlight.IDFlight, idCurrentPassenger));
        }
    }
}
