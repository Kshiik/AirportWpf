using AirportWpf.Controllers;
using AirportWpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AirportWpf.View
{
    /// <summary>
    /// Логика взаимодействия для AddFlightPage.xaml
    /// </summary>
    public partial class AddFlightPage : Page
    {
        Core db = new Core();
        
        public AddFlightPage()
        {
            InitializeComponent();

            AirlineComboBox.ItemsSource = db.context.Airline.ToList();
            AirlineComboBox.DisplayMemberPath = "NameAirline";
            AirlineComboBox.SelectedValuePath = "IDAirline";

            DepartureAirportComboBox.ItemsSource = db.context.Airport.ToList();
            DepartureAirportComboBox.DisplayMemberPath = "NameAirport";
            DepartureAirportComboBox.SelectedValuePath = "IDAirport";

            ArrivalAirportComboBox.ItemsSource = db.context.Airport.ToList();
            ArrivalAirportComboBox.DisplayMemberPath = "NameAirport";
            ArrivalAirportComboBox.SelectedValuePath = "IDAirport";
        }

        private void AirlineComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DepartureAirportComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ArrivalAirportComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            datePicker.IsDropDownOpen = true;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idAirline = AirlineComboBox.SelectedIndex;
                int idDepartureAirport = DepartureAirportComboBox.SelectedIndex;
                int idArrivalAirport = ArrivalAirportComboBox.SelectedIndex;
                string totalNumberOfSeats = TotalNumberOfSeatsTextBox.Text;
                string numberOfAvailableSeats = NumberOfAvailableSeatsTextBox.Text;
                DateTime departureDate = Convert.ToDateTime(datePicker.SelectedDate);
                string departureTime = DepartureTimeTextBox.Text;

                FlightController newFlight = new FlightController();
                bool result = newFlight.CheckAddFlight(idAirline, idDepartureAirport, idArrivalAirport, totalNumberOfSeats, numberOfAvailableSeats, departureDate, departureTime);
                if (result)
                {
                    newFlight.AddFlight(idAirline+1, idDepartureAirport+1, idArrivalAirport+1, Convert.ToInt32(totalNumberOfSeats), Convert.ToInt32(numberOfAvailableSeats), departureDate, TimeSpan.Parse(departureTime));
                    MessageBox.Show("Вы добавили новый рейс. Возращение на страницу с рейсами");
                    this.NavigationService.Navigate(new FlightPage(0));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
