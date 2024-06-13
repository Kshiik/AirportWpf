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
    /// Логика взаимодействия для EditFlightPage.xaml
    /// </summary>
    public partial class EditFlightPage : Page
    {
        Core db = new Core();

        int idCurrentFlight = 0;
        public EditFlightPage(int idFlight, string numberFlight, int? idAirline, int? idDepartureAirport, int? idArrivalAirport, DateTime? departureDate, TimeSpan? departureTime, int? totalNumberOfSeats, int? numberOfAvailableSeats)
        {
            InitializeComponent();

            idCurrentFlight = idFlight;

            AirlineComboBox.ItemsSource = db.context.Airline.ToList();
            AirlineComboBox.DisplayMemberPath = "NameAirline";
            AirlineComboBox.SelectedValuePath = "AirlineID";

            DepartureAirportComboBox.ItemsSource = db.context.Airport.ToList();
            DepartureAirportComboBox.DisplayMemberPath = "NameAirport";
            DepartureAirportComboBox.SelectedValuePath = "DepartureAirportID";

            ArrivalAirportComboBox.ItemsSource = db.context.Airport.ToList();
            ArrivalAirportComboBox.DisplayMemberPath = "NameAirport";
            ArrivalAirportComboBox.SelectedValuePath = "ArrivalAirportID";

            NumberFlightTextBox.Text = numberFlight;
            AirlineComboBox.SelectedIndex = (int)idAirline - 1;
            DepartureAirportComboBox.SelectedIndex = (int)idDepartureAirport - 1;
            ArrivalAirportComboBox.SelectedIndex = (int)idArrivalAirport - 1;
            datePicker.SelectedDate = departureDate;
            DepartureTimeTextBox.Text = Convert.ToString(departureTime);
            TotalNumberOfSeatsTextBox.Text = Convert.ToString(totalNumberOfSeats);
            NumberOfAvailableSeatsTextBox.Text = Convert.ToString(numberOfAvailableSeats);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string numberFlight = NumberFlightTextBox.Text;
                int idAirline = AirlineComboBox.SelectedIndex;
                int idDepartureAirport = DepartureAirportComboBox.SelectedIndex;
                int idArrivalAirport = ArrivalAirportComboBox.SelectedIndex;
                string totalNumberOfSeats = TotalNumberOfSeatsTextBox.Text;
                string numberOfAvailableSeats = NumberOfAvailableSeatsTextBox.Text;
                DateTime departureDate = Convert.ToDateTime(datePicker.SelectedDate);
                string departureTime = DepartureTimeTextBox.Text;

                FlightController editFlight = new FlightController();
                bool result = editFlight.CheckEditFlight(numberFlight, idAirline, idDepartureAirport, idArrivalAirport, totalNumberOfSeats, numberOfAvailableSeats, departureDate, departureTime);
                if (result)
                {
                    editFlight.EditFlight(idCurrentFlight, numberFlight, idAirline, idDepartureAirport, idArrivalAirport, totalNumberOfSeats, numberOfAvailableSeats, departureDate, departureTime);
                    MessageBox.Show($"Вы отредактировали рейс. Возращение на страницу с рейсами");
                    this.NavigationService.Navigate(new FlightPage(0));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            datePicker.IsDropDownOpen = true;
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
    }
}
