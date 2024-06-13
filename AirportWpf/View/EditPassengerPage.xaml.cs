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
    /// Логика взаимодействия для EditPassengerPage.xaml
    /// </summary>
    public partial class EditPassengerPage : Page
    {
        Core db = new Core();

        int idCurrentPassenger = 0;
        public EditPassengerPage(int idPassenger, string firstName, string lastName, string patronymic, DateTime? dateOfBirth, string numberPasport, string placeOfIssuePassport, DateTime? dateOfIssuePassport)
        {
            InitializeComponent();

            idCurrentPassenger = idPassenger;

            FirstNameTextBox.Text = firstName;
            LastNameTextBox.Text = lastName;
            PatronymicTextBox.Text = patronymic;
            datePicker1.SelectedDate = dateOfBirth;
            PasportNumberTextBox.Text = numberPasport;
            PlaceOfIssuePassportTextBox.Text = placeOfIssuePassport;
            datePicker2.SelectedDate = dateOfIssuePassport;
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            datePicker1.IsDropDownOpen = true;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string firstName = FirstNameTextBox.Text;
                string lastName = LastNameTextBox.Text;
                string patronymic = PatronymicTextBox.Text;
                DateTime dateOfBirth = Convert.ToDateTime(datePicker1.SelectedDate);
                string numberPasport = PasportNumberTextBox.Text;
                string placeOfIssuePassport = PlaceOfIssuePassportTextBox.Text;
                DateTime dateOfIssuePassport = Convert.ToDateTime(datePicker2.SelectedDate);

                PassengerController editPassenger = new PassengerController();
                bool result = editPassenger.CheckEditPassenger(firstName, lastName, patronymic, dateOfBirth, numberPasport, placeOfIssuePassport, dateOfIssuePassport);
                if (result)
                {
                    editPassenger.EditPassenger(idCurrentPassenger, firstName, lastName, patronymic, dateOfBirth, numberPasport, placeOfIssuePassport, dateOfIssuePassport);
                    MessageBox.Show($"Вы отредактировали пассажира. Возращение на страницу с пассажирами");
                    this.NavigationService.Navigate(new PassengersPage(0));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DatePickerTextBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            datePicker2.IsDropDownOpen = true;
        }
    }
}
