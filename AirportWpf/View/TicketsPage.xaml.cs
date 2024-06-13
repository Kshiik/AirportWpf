using AirportWpf.Model;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
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
using Page = System.Windows.Controls.Page;
using Word = Microsoft.Office.Interop.Word;

namespace AirportWpf.View
{
    /// <summary>
    /// Логика взаимодействия для TicketsPage.xaml
    /// </summary>
    public partial class TicketsPage : Page
    {
        Core db = new Core();
        List<Ticket_Passenger> arrayTicket;
        List<Ticket_Passenger> arrayAllTicket = new List<Ticket_Passenger>();

        int idCurrentFlight = 0;
        int idCurrentPassenger = 0;
        public TicketsPage(int idFlight, int idPassenger)
        {
            InitializeComponent();

            idCurrentFlight = idFlight;
            idCurrentPassenger = idPassenger;
            if (idCurrentPassenger == 0)
            {
                arrayTicket = db.context.Ticket_Passenger.Where(x => x.FlightID == idCurrentFlight).ToList();
                TicketsListView.ItemsSource = arrayTicket;
            }
            if(idCurrentFlight == 0)
            {
                arrayTicket = db.context.Ticket_Passenger.Where(x => x.PassengerID == idCurrentPassenger).ToList();
                TicketsListView.ItemsSource = arrayTicket;
            }
            if (idCurrentPassenger == 0 && idCurrentFlight == 0)
            {
                arrayAllTicket = db.context.Ticket_Passenger.ToList();
                TicketsListView.ItemsSource = arrayAllTicket;
            }
        }

        private void WordDocumentButton_Click(object sender, RoutedEventArgs e)
        {
            Button selectedButton = sender as Button;
            Ticket_Passenger activeTicket = selectedButton.DataContext as Ticket_Passenger;

            Word.Application application = new Word.Application();
            Word.Document document = application.Documents.Add();

            //таблица
            //первая строчка
            Word.Paragraph tableParagraph = document.Paragraphs.Add();
            Word.Range tableRange = tableParagraph.Range;
            Word.Table lineTicket1 = document.Tables.Add(tableRange, 1, 1);

            //2, 3 строчка
            Word.Paragraph tableParagraph2 = document.Paragraphs.Add();
            Word.Range tableRange2 = tableParagraph2.Range;
            Word.Table lineTicket2 = document.Tables.Add(tableRange2, 2, 2);

            //4 строчка
            Word.Paragraph tableParagraph3 = document.Paragraphs.Add();
            Word.Range tableRange3 = tableParagraph3.Range;
            Word.Table lineTicket3 = document.Tables.Add(tableRange3, 1, 1);

            //5, 6 строчка
            Word.Paragraph tableParagraph4 = document.Paragraphs.Add();
            Word.Range tableRange4 = tableParagraph4.Range;
            Word.Table lineTicket4 = document.Tables.Add(tableRange4, 2, 3);

            //7 строчка
            Word.Paragraph tableParagraph5 = document.Paragraphs.Add();
            Word.Range tableRange5 = tableParagraph5.Range;
            Word.Table lineTicket5 = document.Tables.Add(tableRange5, 1, 1);

            //8, 9 строчка
            Word.Paragraph tableParagraph6 = document.Paragraphs.Add();
            Word.Range tableRange6 = tableParagraph6.Range;
            Word.Table lineTicket6 = document.Tables.Add(tableRange6, 2, 6);

            //10 строчка
            Word.Paragraph tableParagraph7 = document.Paragraphs.Add();
            Word.Range tableRange7 = tableParagraph7.Range;
            Word.Table lineTicket7 = document.Tables.Add(tableRange7, 1, 1);

            //11 строчка
            Word.Paragraph tableParagraph8 = document.Paragraphs.Add();
            Word.Range tableRange8 = tableParagraph8.Range;
            Word.Table lineTicket8 = document.Tables.Add(tableRange8, 1, 3);

            //заполение
            Word.Range cellRange;
            cellRange = lineTicket8.Cell(1, 1).Range;
            cellRange.Text = "ЭЛЕКТРОННЫЙ БИЛЕТ (МАРШРУТ/КВИТАНЦИЯ)";

            cellRange = lineTicket8.Cell(2, 1).Range;
            cellRange.Text = "ДАТА РОЖДЕНИЯ:";
            cellRange = lineTicket8.Cell(2, 2).Range;
            DateTime? dateOfBirth = db.context.Passenger.Where(x => x.IDPassenger == activeTicket.PassengerID).FirstOrDefault().DateOfBirth;
            cellRange.Text = String.Format($"{dateOfBirth:d}");

            cellRange = lineTicket8.Cell(3, 1).Range;
            cellRange.Text = "ФАМИЛИЯ ИМЯ ОТЧЕСТВО:";
            cellRange = lineTicket8.Cell(3, 2).Range;
            string fio = db.context.Passenger.Where(x => x.IDPassenger== activeTicket.PassengerID).FirstOrDefault().FIO;
            cellRange.Text = fio;

            cellRange = lineTicket8.Cell(5, 1).Range;
            cellRange.Text = "НОМЕР БИЛЕТА:";
            cellRange = lineTicket8.Cell(5, 2).Range;
            cellRange.Text = activeTicket.NumberTicket;

            cellRange = lineTicket8.Cell(6, 1).Range;
            cellRange.Text = "КОД БРОНИРОВАНИЯ:";
            cellRange = lineTicket8.Cell(6, 2).Range;
            string numberBooking = db.context.Booking.Where(x => x.IDBooking == activeTicket.BookingID).FirstOrDefault().CodeBooking;
            cellRange.Text = numberBooking;

            cellRange = lineTicket8.Cell(8, 1).Range;
            cellRange.Text = "ОТ:";
            cellRange = lineTicket8.Cell(9, 1).Range;
            int idArrivalAirport = (int)db.context.Flight.Where(x => x.IDFlight == activeTicket.FlightID).FirstOrDefault().ArrivalAirportID + 1;
            string arrivalAirport = db.context.Airport.Where(x => x.IDAirport == idArrivalAirport).FirstOrDefault().NameAirport;
            cellRange.Text = arrivalAirport;

            cellRange = lineTicket8.Cell(8, 2).Range;
            cellRange.Text = "ДО:";
            cellRange = lineTicket8.Cell(9, 2).Range;
            int idDepartureAirport = (int)db.context.Flight.Where(x => x.IDFlight == activeTicket.FlightID).FirstOrDefault().DepartureAirportID + 1;
            string departureAirport = db.context.Airport.Where(x => x.IDAirport == idDepartureAirport).FirstOrDefault().NameAirport;
            cellRange.Text = departureAirport;

            cellRange = lineTicket8.Cell(8, 4).Range;
            cellRange.Text = "РЕЙС:";
            cellRange = lineTicket8.Cell(9,4).Range;
            string numberFlight = db.context.Flight.Where(x => x.IDFlight == activeTicket.FlightID).FirstOrDefault().NumberFlight;
            cellRange.Text = numberFlight;

            cellRange = lineTicket8.Cell(8, 5).Range;
            cellRange.Text = "ДАТА:";
            cellRange = lineTicket8.Cell(9, 5).Range;
            DateTime? dateFlight = db.context.Flight.Where(x => x.IDFlight == activeTicket.FlightID).FirstOrDefault().DepartureDate;
            cellRange.Text = String.Format($"{dateFlight:d}");

            cellRange = lineTicket8.Cell(8, 6).Range;
            cellRange.Text = "ВРЕМЯ:";
            cellRange = lineTicket8.Cell(9, 6).Range;
            TimeSpan? timeFlight = db.context.Flight.Where(x => x.IDFlight == activeTicket.FlightID).FirstOrDefault().DepartureTime;
            cellRange.Text = Convert.ToString(timeFlight);

            cellRange = lineTicket8.Cell(11, 1).Range;
            cellRange.Text = "ИТОГ:";
            cellRange = lineTicket8.Cell(11, 2).Range;
            cellRange.Text = $"{Convert.ToString(activeTicket.Price)} руб.";

            //форматирование
            lineTicket8.Range.Font.Size = 12;
            lineTicket8.Range.Font.Name = "Times New Roman";
            lineTicket8.Range.Paragraphs.SpaceAfter = 0;
            lineTicket8.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            lineTicket8.Cell(2, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
            lineTicket8.Cell(3, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;


            //границы
            lineTicket8.Rows[8].Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleDashLargeGap;
            lineTicket8.Rows[7].Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleNone;
            lineTicket8.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleNone;
            //lineTicket8.Borders.InsideLineStyle = lineTicket8.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;

            application.Visible = true;

            //сохранение документа
            document.SaveAs2($"{Directory.GetCurrentDirectory()}\\Docs\\Билет {activeTicket.NumberTicket}.docx");
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
            NavigationService.Navigate(new TicketsPage( 0, 0));
        }
    }
}
