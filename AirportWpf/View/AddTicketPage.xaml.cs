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
    /// Логика взаимодействия для AddTicketPage.xaml
    /// </summary>
    public partial class AddTicketPage : Page
    {

        int idCurrentFlight = 0;
        int idCurrentPassenger = 0;
        public AddTicketPage(int idFlight, int idPassenger)
        {
            InitializeComponent();

            idCurrentFlight = idFlight;
            idCurrentPassenger = idPassenger;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string numberTicket = NumberTicketTextBox.Text;
                string price = PriceTextBox.Text;

                Ticket_PassengerController newTicket = new Ticket_PassengerController();
                bool result = newTicket.CheckAddTicket(numberTicket, price);
                if (result)
                {
                    newTicket.AddTicket(numberTicket, idCurrentFlight, idCurrentPassenger, Convert.ToInt32(price));
                    MessageBox.Show("Вы добавили билет пассажиру. Переход на страницу билетов");
                    this.NavigationService.Navigate(new TicketsPage(0, 0));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
