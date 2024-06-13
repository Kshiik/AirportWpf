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
    /// Логика взаимодействия для PassengersPage.xaml
    /// </summary>
    public partial class PassengersPage : Page
    {
        Core db = new Core();
        List<Ticket_Passenger> arrayPassenger;
        List<Ticket_Passenger> arrayAllPassenger = new List<Ticket_Passenger>();
        List<Passenger> arrayUnegisteredPassenger;

        int idRole = 0;
        int idCurrentFlight = 0;
        public PassengersPage(int idFlight)
        {
            InitializeComponent();

            idRole = Properties.Settings.Default.idRole;
            idCurrentFlight = idFlight;

            if (idCurrentFlight == 0)
                CheckBoxStackPanel.Visibility = Visibility.Visible;
            if (idCurrentFlight > 0)
                CheckBoxStackPanel.Visibility = Visibility.Collapsed;

            PassengerCheckBox.IsChecked = true;

            if (idCurrentFlight == 0)
            {
                arrayAllPassenger = db.context.Ticket_Passenger.ToList();
                PassengerListViewAdmin.ItemsSource = arrayAllPassenger;
            }
            else
            {
                arrayPassenger = db.context.Ticket_Passenger.Where(x => x.FlightID == idCurrentFlight).ToList();
                PassengerListViewAdmin.ItemsSource = arrayPassenger;
            }

        }

        private void DeletPassengerButton_Click(object sender, RoutedEventArgs e)
        {
            Button selectedButton = sender as Button;
            Ticket_Passenger activePassenger = selectedButton.DataContext as Ticket_Passenger;
            MessageBoxResult rez = MessageBox.Show($"Вы уверены, что хотите удалить пассажира {activePassenger.Passenger.FirstName} {activePassenger.Passenger.LastName} {activePassenger.Passenger.Patronymic}?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (rez == MessageBoxResult.Yes)
            {
                try
                {
                    PassengerController.DeletePassenger(activePassenger.Passenger.IDPassenger);
                    MessageBox.Show("Данные успешно удалены.", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.NavigationService.Navigate(new PassengersPage(0));
                    PassengerCheckBox.IsChecked = true;
                }
                catch (Exception)
                {
                    MessageBox.Show("Возникла ошибка при удалении. \nВозвращение на главную страницу", "Удаление", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.NavigationService.Navigate(new FlightPage(0));
                }
            }
        }

        private void CancelOfRegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            Button selectedButton = sender as Button;
            Ticket_Passenger activePassenger = selectedButton.DataContext as Ticket_Passenger;
            MessageBoxResult rez = MessageBox.Show($"Вы уверены, что хотите отменить регистрацию пассажира {activePassenger.Passenger.FirstName} {activePassenger.Passenger.LastName} {activePassenger.Passenger.Patronymic}?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (rez == MessageBoxResult.Yes)
            {
                try
                {
                    PassengerController.CancleRegis(activePassenger.IDTicket);
                    MessageBox.Show("Регистрация отменена. \nВозвращение на главную страницу", "Отмена регестрации", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.NavigationService.Navigate(new FlightPage(0));
                }
                catch (Exception)
                {
                    MessageBox.Show("Возникла ошибка при отмене. \nВозвращение на главную страницу", "Отмена регистрации", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.NavigationService.Navigate(new FlightPage(0));
                }
            }
        }

        private void TicketButton_Click(object sender, RoutedEventArgs e)
        {
            Button activeButton = sender as Button;
            Ticket_Passenger activePassenger = activeButton.DataContext as Ticket_Passenger;
            NavigationService.Navigate(new TicketsPage(0, (int)activePassenger.PassengerID));
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var passangersResult = db.context.Ticket_Passenger.ToList();
            var passangers = db.context.Passenger.ToList();

            if (FilterCombBox.SelectedIndex == 0)
            {
                var passengersFIO = passangers.Where(x => x.FIO.ToLower().Contains(SearchTextBox.Text.ToLower())).Select(x => x.IDPassenger).ToList();
                passangersResult = passangersResult.Where(x => passengersFIO.Contains((int)x.PassengerID)).ToList();
                PassengerListViewAdmin.ItemsSource = passangersResult;
                PassengerListViewDispetcher.ItemsSource = passangersResult;
            }
            if (FilterCombBox.SelectedIndex == 1)
            {
                var passengersPasportNumber = passangers.Where(x => x.PasportNumber.Contains(SearchTextBox.Text)).Select(x => x.IDPassenger).ToList();
                passangersResult = passangersResult.Where(x => passengersPasportNumber.Contains((int)x.PassengerID)).ToList();
                PassengerListViewAdmin.ItemsSource = passangersResult;
                PassengerListViewDispetcher.ItemsSource = passangersResult;
            }
        }

        private void EditPassengerButton_Click(object sender, RoutedEventArgs e)
        {
            Button activeButton = sender as Button;
            Ticket_Passenger activePassenger = activeButton.DataContext as Ticket_Passenger;
            NavigationService.Navigate(new EditPassengerPage((int)activePassenger.PassengerID, activePassenger.Passenger.FirstName, activePassenger.Passenger.LastName,  activePassenger.Passenger.Patronymic, activePassenger.Passenger.DateOfBirth, activePassenger.Passenger.PasportNumber, activePassenger.Passenger.PlaceOfIssuePassport, activePassenger.Passenger.DateOfIssuePassport));
        }

        private void AllPassengerButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PassengersPage(0));
        }

        private void FlightPageButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FlightPage(0));
        }

        private void TicketPageButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TicketsPage(0, 0));
        }

        private void PassengerCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (idRole == 1)
            {
                UnregisteredPassengerListViewAdmin.Visibility = Visibility.Collapsed;
                PassengerListViewAdmin.Visibility = Visibility.Visible;
                if (idCurrentFlight == 0)
                {
                    arrayAllPassenger = db.context.Ticket_Passenger.ToList();
                    PassengerListViewAdmin.ItemsSource = arrayAllPassenger;
                }
                else
                {
                    arrayPassenger = db.context.Ticket_Passenger.Where(x => x.FlightID == idCurrentFlight).ToList();
                    PassengerListViewAdmin.ItemsSource = arrayPassenger;
                }
            }
            if (idRole == 2)
            {
                UnregisteredPassengerListViewDispetcher.Visibility = Visibility.Collapsed;
                PassengerListViewDispetcher.Visibility = Visibility.Visible;
                if (idCurrentFlight == 0)
                {
                    arrayAllPassenger = db.context.Ticket_Passenger.ToList();
                    PassengerListViewDispetcher.ItemsSource = arrayAllPassenger;
                }
                else
                {
                    arrayPassenger = db.context.Ticket_Passenger.Where(x => x.FlightID == idCurrentFlight).ToList();
                    PassengerListViewDispetcher.ItemsSource = arrayPassenger;
                }
            }
        }

        private void PassengerCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var idRegisteredPassengers = db.context.Ticket_Passenger.Select(x => x.PassengerID).ToList();
            arrayUnegisteredPassenger = db.context.Passenger.Where(x => !idRegisteredPassengers.Contains(x.IDPassenger)).ToList();

            if (idRole == 1)
            {
                UnregisteredPassengerListViewAdmin.ItemsSource = arrayUnegisteredPassenger;

                UnregisteredPassengerListViewAdmin.Visibility = Visibility.Visible;
                PassengerListViewAdmin.Visibility = Visibility.Collapsed;
                if (idCurrentFlight == 0)
                {
                    arrayAllPassenger = db.context.Ticket_Passenger.ToList();
                    PassengerListViewAdmin.ItemsSource = arrayAllPassenger;
                }
                else
                {
                    arrayPassenger = db.context.Ticket_Passenger.Where(x => x.FlightID == idCurrentFlight).ToList();
                    PassengerListViewAdmin.ItemsSource = arrayPassenger;
                }
            }
            if (idRole == 2)
            {
                UnregisteredPassengerListViewDispetcher.ItemsSource = arrayUnegisteredPassenger;
                UnregisteredPassengerListViewDispetcher.Visibility = Visibility.Visible;
                PassengerListViewDispetcher.Visibility = Visibility.Collapsed;
                if (idCurrentFlight == 0)
                {
                    arrayAllPassenger = db.context.Ticket_Passenger.ToList();
                    PassengerListViewDispetcher.ItemsSource = arrayAllPassenger;
                }
                else
                {
                    arrayPassenger = db.context.Ticket_Passenger.Where(x => x.FlightID == idCurrentFlight).ToList();
                    PassengerListViewDispetcher.ItemsSource = arrayPassenger;
                }
            }
        }

        private void RegisterPassenger_Click(object sender, RoutedEventArgs e)
        {
            Button activeButton = sender as Button;
            Passenger activePassenger = activeButton.DataContext as Passenger;
            NavigationService.Navigate(new FlightPage((int)activePassenger.IDPassenger));
        }

        private void EditPassengerButton2_Click(object sender, RoutedEventArgs e)
        {
            Button activeButton = sender as Button;
            Passenger activePassenger = activeButton.DataContext as Passenger;
            NavigationService.Navigate(new EditPassengerPage((int)activePassenger.IDPassenger, activePassenger.FirstName, activePassenger.LastName, activePassenger.Patronymic, activePassenger.DateOfBirth, activePassenger.PasportNumber, activePassenger.PlaceOfIssuePassport, activePassenger.DateOfIssuePassport));
        }

        private void DeletPassengerButton2_Click(object sender, RoutedEventArgs e)
        {
            Button selectedButton = sender as Button;
            Passenger activePassenger = selectedButton.DataContext as Passenger;
            MessageBoxResult rez = MessageBox.Show($"Вы уверены, что хотите удалить пассажира {activePassenger.FirstName} {activePassenger.LastName} {activePassenger.Patronymic}?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (rez == MessageBoxResult.Yes)
            {
                try
                {
                    PassengerController.DeletePassenger(activePassenger.IDPassenger);
                    MessageBox.Show("Данные успешно удалены.", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.NavigationService.Navigate(new PassengersPage(0));
                    PassengerCheckBox.IsChecked = false;
                }
                catch (Exception)
                {
                    MessageBox.Show("Возникла ошибка при удалении. \nВозвращение на главную страницу", "Удаление", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.NavigationService.Navigate(new FlightPage(0));
                }
            }
        }
    }
}
