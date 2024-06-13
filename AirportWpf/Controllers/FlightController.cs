using AirportWpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportWpf.Controllers
{
    public class FlightController
    {
        static Core db = new Core();
        /// <summary>
        /// Удаляет рейс по идентификатору рейса.
        /// </summary>
        /// <param name="idFlight">Идентификатор рейса</param>
        public static void DeleteFlight(int idFlight)
        {
            db.context.Flight.Remove(db.context.Flight.Single(x => x.IDFlight == idFlight));
            db.context.SaveChanges();
        }
        /// <summary>
        /// Проверяет корректность данных для добавления рейса
        /// </summary>
        /// <param name="idAirline">Идентификатор авиакомпании</param>
        /// <param name="idDepartureAirport">Идентификатор аэропорта отправления</param>
        /// <param name="idArrivalAirport">Идентификатор аэропорта прибытия</param>
        /// <param name="totalNumberOfSeats">Всего количество мест</param>
        /// <param name="numberOfAvailableSeats">Количество свободных мест</param>
        /// <param name="departureDate">Дата отправления</param>
        /// <param name="departureTime">Время отправления</param>
        /// <returns>
        /// True, если данные корректны; в противном случае возвращает исключение
        /// </returns>
        public bool CheckAddFlight(int idAirline, int idDepartureAirport, int idArrivalAirport, string totalNumberOfSeats, string numberOfAvailableSeats, DateTime? departureDate, string departureTime)
        {
            if (idAirline == -1)
                throw new Exception("Вы не выбрали авивкомпанию");
            if (idDepartureAirport == -1)
                throw new Exception("Вы не выбрали аэропорт отправления");
            if (idArrivalAirport == -1)
                throw new Exception("Вы не выбрали аэропорт прибытия");
            if (idDepartureAirport == idArrivalAirport)
                throw new Exception("Аэропорт отправления не может совподать с аэропортом прибытия");

            if (string.IsNullOrEmpty(totalNumberOfSeats))
                throw new Exception("Вы не ввели количество всего мест");
            if (!int.TryParse(totalNumberOfSeats, out int numb1))
                throw new Exception("Вы ввели не корректный тип данных в 'Количество всего мест'");
            if (Convert.ToInt32(totalNumberOfSeats) > 1000)
                throw new Exception("Вы ввели слишкои большое число мест");

            if (string.IsNullOrEmpty(numberOfAvailableSeats))
                throw new Exception("Вы не ввели количество свобоных мест");
            if (!int.TryParse(numberOfAvailableSeats, out int numb2))
                throw new Exception("Вы ввели не корректный тип данных в 'Количество свобоных мест'");
            if (Convert.ToInt32(numberOfAvailableSeats) > Convert.ToInt32(totalNumberOfSeats))
                throw new Exception("Количество свободных мест не может превышать всего мест");

            if (departureDate.Value.Date.ToShortDateString() == "01.01.0001")
                throw new Exception("Вы не выбрали дату отправления");

            if (string.IsNullOrEmpty(departureTime))
                throw new Exception("Вы не ввели время отправления");
            if (!TimeSpan.TryParse(departureTime, out TimeSpan temp))
                throw new Exception("Вы ввели не корректный тип данных в 'Время отправления'");

            else
                return true;
        }
        /// <summary>
        /// Добовляет новый рейс
        /// </summary>
        /// <param name="idAirline">Идентификатор авиакомпании</param>
        /// <param name="idDepartureAirport">Идентификатор аэропорта отправления</param>
        /// <param name="idArrivalAirport">Идентификатор аэропорта прибытия</param>
        /// <param name="totalNumberOfSeats">Всего количество мест</param>
        /// <param name="numberOfAvailableSeats">Количество свободных мест</param>
        /// <param name="departureDate">Дата отправления</param>
        /// <param name="departureTime">Время отправления</param>
        public void AddFlight(int idAirline, int idDepartureAirport, int idArrivalAirport, int totalNumberOfSeats, int numberOfAvailableSeats, DateTime? departureDate, TimeSpan departureTime)
        {
            //формирование номера рейса
            string numberFlight = "";
            numberFlight = db.context.Airline.Where(x => x.IDAirline == idAirline).FirstOrDefault().CodeAirline;

            int idLastFlight = db.context.Flight.Max(item => item.IDFlight);
            string lastNumberFlight = db.context.Flight.Where(x => x.IDFlight == idLastFlight).FirstOrDefault().NumberFlight;
            string[] parts = lastNumberFlight.Split('-');
            string serialNumber = Convert.ToString(int.Parse(parts[1]) + 1);
            numberFlight = numberFlight + "-0" + serialNumber;

            Flight newFlight = new Flight()
            {
                NumberFlight = numberFlight,
                AirlineID = idAirline,
                DepartureAirportID = idDepartureAirport,
                ArrivalAirportID = idArrivalAirport,
                TotalNumberOfSeats = totalNumberOfSeats,
                NumberOfAvailableSeats = numberOfAvailableSeats,
                DepartureDate = departureDate,
                DepartureTime = departureTime,
            };
            if (!(idAirline == 0 || idDepartureAirport == 0 || idArrivalAirport == 0 || totalNumberOfSeats == 0 || numberOfAvailableSeats == 0))
            {
                newFlight.NumberFlight = numberFlight;
                newFlight.AirlineID = idAirline;
                newFlight.DepartureAirportID = idDepartureAirport;
                newFlight.ArrivalAirportID = idArrivalAirport;
                newFlight.TotalNumberOfSeats = totalNumberOfSeats;
                newFlight.NumberOfAvailableSeats = numberOfAvailableSeats;
                newFlight.DepartureDate = departureDate;
                newFlight.DepartureTime = departureTime;
            }

            db.context.Flight.Add(newFlight);
            db.context.SaveChanges();
        }
        /// <summary>
        /// Проверяет корректность данных для редактирования рейса
        /// </summary>
        /// /// <param name="numberFlight">Номер рейса</param>
        /// <param name="idAirline">Идентификатор авиакомпании</param>
        /// <param name="idDepartureAirport">Идентификатор аэропорта отправления</param>
        /// <param name="idArrivalAirport">Идентификатор аэропорта прибытия</param>
        /// <param name="totalNumberOfSeats">Всего количество мест</param>
        /// <param name="numberOfAvailableSeats">Количество свободных мест</param>
        /// <param name="departureDate">Дата отправления</param>
        /// <param name="departureTime">Время отправления</param>
        /// <returns>
        /// Возвращает true, если данные корректны; в противном случае будет исключение
        /// </returns>
        public bool CheckEditFlight(string numberFlight, int idAirline, int idDepartureAirport, int idArrivalAirport, string totalNumberOfSeats, string numberOfAvailableSeats, DateTime? departureDate, string departureTime)
        {
            if (string.IsNullOrEmpty(numberFlight) && idAirline == -1 && idDepartureAirport == -1 && idArrivalAirport == -1 && string.IsNullOrEmpty(totalNumberOfSeats) && string.IsNullOrEmpty(numberOfAvailableSeats) && departureDate.Value.Date.ToShortDateString() == "01.01.0001" && string.IsNullOrEmpty(departureTime))
                throw new Exception("Вы не ввели никаких данных");

            if (!string.IsNullOrEmpty(totalNumberOfSeats))
            {
                if (!int.TryParse(totalNumberOfSeats, out int numb1))
                    throw new Exception("Вы ввели не корректный тип данных в 'Количество всего мест'");
            }

            if (!string.IsNullOrEmpty(numberOfAvailableSeats))
            {
                if (!int.TryParse(numberOfAvailableSeats, out int numb2))
                    throw new Exception("Вы ввели не корректный тип данных в 'Количество свобоных мест'");
            }
            if (!string.IsNullOrEmpty(departureTime))
            {
                if (!TimeSpan.TryParse(departureTime, out TimeSpan temp))
                    throw new Exception("Вы ввели не корректный тип данных в 'Время отправления'");
            }

            return true;
        }
        /// <summary>
        /// Редактирует рейс по идентификатору рейса
        /// </summary>
        /// /// <param name="numberFlight">Номер рейса</param>
        /// <param name="idAirline">Идентификатор авиакомпании</param>
        /// <param name="idDepartureAirport">Идентификатор аэропорта отправления</param>
        /// <param name="idArrivalAirport">Идентификатор аэропорта прибытия</param>
        /// <param name="totalNumberOfSeats">Всего количество мест</param>
        /// <param name="numberOfAvailableSeats">Количество свободных мест</param>
        /// <param name="departureDate">Дата отправления</param>
        /// <param name="departureTime">Время отправления</param>
        /// <returns>
        public void EditFlight(int idFlight, string numberFlight, int idAirline, int idDepartureAirport, int idArrivalAirport, string totalNumberOfSeats, string numberOfAvailableSeats, DateTime? departureDate, string departureTime)
        {
            if (!string.IsNullOrEmpty(numberFlight))
            {
                var objFlightNumber = db.context.Flight.Where(x => x.IDFlight == idFlight).FirstOrDefault();
                objFlightNumber.NumberFlight = numberFlight;
            }
            if (idAirline > -1)
            {
                var objFlightAirline = db.context.Flight.Where(x => x.IDFlight == idFlight).FirstOrDefault();
                objFlightAirline.AirlineID = idAirline + 1;
            }
            if (idDepartureAirport > -1)
            {
                var objFlghtDepartureAirport = db.context.Flight.Where(x => x.IDFlight == idFlight).FirstOrDefault();
                objFlghtDepartureAirport.DepartureAirportID = idDepartureAirport + 1;
            }
            if (idArrivalAirport > -1)
            {
                var objFlghtArrivalAirport = db.context.Flight.Where(x => x.IDFlight == idFlight).FirstOrDefault();
                objFlghtArrivalAirport.ArrivalAirportID = idArrivalAirport + 1;
            }
            if (!string.IsNullOrEmpty(totalNumberOfSeats))
            {
                var objFlghtTotalNumberOfSeats = db.context.Flight.Where(x => x.IDFlight == idFlight).FirstOrDefault();
                objFlghtTotalNumberOfSeats.TotalNumberOfSeats = Convert.ToInt32(totalNumberOfSeats);
            }
            if (!string.IsNullOrEmpty(numberOfAvailableSeats))
            {
                var objFlghtNumberOfAvailableSeats = db.context.Flight.Where(x => x.IDFlight == idFlight).FirstOrDefault();
                objFlghtNumberOfAvailableSeats.NumberOfAvailableSeats = Convert.ToInt32(numberOfAvailableSeats);
            }
            if (departureDate.Value.Date.ToShortDateString() != "01.01.0001")
            {
                var objFlghtDepartureDate = db.context.Flight.Where(x => x.IDFlight == idFlight).FirstOrDefault();
                objFlghtDepartureDate.DepartureDate = departureDate;
            }
            if (!string.IsNullOrEmpty(departureTime))
            {
                TimeSpan.TryParse(departureTime, out TimeSpan departureTimeFlight);
                var objFlghtDepartureTime = db.context.Flight.Where(x => x.IDFlight == idFlight).FirstOrDefault();
                objFlghtDepartureTime.DepartureTime = departureTimeFlight;
            }
            db.context.SaveChanges();
        }
    }
}
