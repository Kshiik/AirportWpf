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
    /// Логика взаимодействия для AddPassengersPage.xaml
    /// </summary>
    public partial class AddPassengersPage : Page
    {
        Core db = new Core();

        int idCurrentFlight = 0;
        int idctreatedPassenger = 0;
        public AddPassengersPage(int idFlight)
        {
            InitializeComponent();

            idCurrentFlight = idFlight;
            idctreatedPassenger = db.context.Passenger.Max(item => item.IDPassenger) + 1;
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DatePicker1.IsDropDownOpen = true;
            
        }

        private void AddButon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string firstName = FirstNameTextBox.Text;
                string lastName = LastNameTextBox.Text;
                string patronymic = PatronymicTextBox.Text;
                string numberPasport = PasportNumber.Text;
                string placeOfIssuePassport = PlaceOfIssuePassport.Text;

                PassengerController newPassenger = new PassengerController();
                bool result = newPassenger.CheckAddPassenger(firstName, lastName, patronymic,  Convert.ToDateTime(DatePicker1.SelectedDate), numberPasport, placeOfIssuePassport, Convert.ToDateTime(DatePicker2.SelectedDate));
                if (result)
                {
                    newPassenger.AddPassenger(firstName, lastName, patronymic, Convert.ToDateTime(DatePicker1.SelectedDate), numberPasport, placeOfIssuePassport, Convert.ToDateTime(DatePicker2.SelectedDate));
                    MessageBox.Show("Вы добавили нового пассажира. Переход на страницу регистрации билета пассажиру");
                    this.NavigationService.Navigate(new AddTicketPage(idCurrentFlight, idctreatedPassenger));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DatePickerTextBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DatePicker2.IsDropDownOpen = true;
        }
    }
}
