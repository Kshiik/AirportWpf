using AirportLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportLibrary
{
    public class Ticket_PassengerClass
    {
        static Core db = new Core();

        /// <summary>
        /// Отменяет регистрацию по id билета
        /// </summary>
        /// <param name="idTicket">Идентификатор билета</param>
        public static void CancleRegis(int idTicket)
        {
            db.context.Ticket_Passenger.Remove(db.context.Ticket_Passenger.Single(x => x.IDTicket == idTicket));
            db.context.SaveChanges();
        }
        /// <summary>
        /// Проверяет корректность данных для добавления билета
        /// </summary>
        /// <param name="numberTicket">Номер билета</param>
        /// <param name="price">Цена билета</param>
        /// <returns>
        /// Возвращает true, если данные корректны; в противном случае возвращает исключение
        /// </returns>
        public static bool CheckAddTicket(string numberTicket, string price)
        {
            if (string.IsNullOrEmpty(numberTicket))
                throw new Exception("Вы не ввели номер билета");
            if (!int.TryParse(numberTicket, out int numb1))
                throw new Exception("Вы ввели не корректный тип данных в номере билета");
            if (numberTicket.Length != 5)
                throw new Exception("Вы ввели номер билета неправильного формата");

            if (string.IsNullOrEmpty(price))
                throw new Exception("Вы не ввели цену билета");
            if (!int.TryParse(price, out int numb2))
                throw new Exception("Вы ввели не корректный тип данных в для цены билета");

            else
                return true;
        }
        /// <summary>
        /// Добавляет новый билет
        /// </summary>
        /// <param name="numberTicket">Номер билета</param>
        /// <param name="idFlight">Идентификатор рейса</param>
        /// <param name="idPassenger">Идентификатор пассажира</param>
        /// <param name="price">Цена билета</param>
        public static void AddTicket(string numberTicket, int idFlight, int idPassenger, int price)
        {
            //Добавление номера бронирования в таблицу Booking
            string codeBooking = GenerateBookingCode();
            Booking newBooking = new Booking()
            {
                CodeBooking = codeBooking,
            };
            db.context.Booking.Add(newBooking);
            db.context.SaveChanges();
            int idBooking = db.context.Booking.Where(x => x.CodeBooking == codeBooking).FirstOrDefault().IDBooking;

            //Уменьшение количество свободных мест
            var objFlghtNumberOfAvailableSeats = db.context.Flight.Where(x => x.IDFlight == idFlight).FirstOrDefault();
            objFlghtNumberOfAvailableSeats.NumberOfAvailableSeats -= 1;

            //Добавление билета
            Ticket_Passenger newTicket = new Ticket_Passenger()
            {
                NumberTicket = numberTicket,
                BookingID = idBooking,
                PassengerID = idPassenger,
                FlightID = idFlight,
                Price = price,
            };
            if (!string.IsNullOrEmpty(numberTicket))
            {
                newTicket.NumberTicket = numberTicket;
                newTicket.BookingID = idBooking;
                newTicket.PassengerID = idPassenger;
                newTicket.FlightID = idFlight;
                newTicket.Price = price;
            }
            db.context.Ticket_Passenger.Add(newTicket);
            db.context.SaveChanges();
        }

        /// <summary>
        /// Формирует код бронирования
        /// </summary>
        /// <returns>Сгенерированный код бронирования</returns>
        static string GenerateBookingCode()
        {
            Random random = new Random();
            char getRandomLetter() => (char)random.Next('A', 'Z' + 1);
            char getRandomDigit() => (char)random.Next('0', '9' + 1);

            char[] code = new char[5];
            code[0] = getRandomLetter();
            code[1] = getRandomDigit();
            code[2] = getRandomDigit();
            code[3] = getRandomLetter();
            code[4] = getRandomLetter();

            return new string(code);
        }
        /// <summary>
        /// Отменяет все билеты для указанного рейса
        /// </summary>
        /// <param name="idFlight">Идентификатор рейса</param>
        public static void CancleTickets(int idFlight)
        {
            foreach (var ticketsPassenger in db.context.Ticket_Passenger.Where(x => x.FlightID == idFlight).ToList())
            {
                db.context.Ticket_Passenger.Remove(ticketsPassenger);
            }

            int addNumberOfAvailableSeats = db.context.Ticket_Passenger.Where(x => x.FlightID == idFlight).Count();
            var objFlghtNumberOfAvailableSeats = db.context.Flight.Where(x => x.IDFlight == idFlight).FirstOrDefault();
            objFlghtNumberOfAvailableSeats.NumberOfAvailableSeats += addNumberOfAvailableSeats;
            db.context.SaveChanges();
        }
    }
}
