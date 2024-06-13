using AirportWpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AirportWpf.Controllers
{
    public class PassengerController
    {
        static Core db = new Core();

        string regexNumberPasport = @"^\d{4}-\d{6}$";

        /// <summary>
        /// Проверяет корректность данных для добавления пассажира
        /// </summary>
        /// <param name="firstName">Имя пассажира</param>
        /// <param name="lastName">Фамилия пассажира</param>
        /// <param name="patronymic">Отчество пассажира</param>
        /// <param name="dateOfBirth">Дата рождения пассажира</param>
        /// <param name="numberPasport">Номер паспорта</param>
        /// <param name="placeOfIssuePassport">Место выдачи паспорта</param>
        /// <param name="dateOfIssuePassport">Дата выдачи паспорта.</param>
        /// <returns>
        /// Возвращает true, если данные корректны; в противном случае возвращает исключение.
        /// </returns>
        public bool CheckAddPassenger(string firstName, string lastName, string patronymic, DateTime? dateOfBirth, string numberPasport, string placeOfIssuePassport, DateTime? dateOfIssuePassport)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new Exception("Вы не ввели фамилию");
            if (string.IsNullOrWhiteSpace(firstName))
                throw new Exception("Вы не ввели имя");
            if (string.IsNullOrWhiteSpace(patronymic))
                throw new Exception("Вы не ввели отчество");
            if (dateOfBirth.Value.Date.ToShortDateString() == "01.01.0001")
                throw new Exception("Вы не выбрали дату рождения");

            if (string.IsNullOrWhiteSpace(numberPasport))
                throw new Exception("Вы не ввели номер паспорта");
            if (!Regex.IsMatch(numberPasport, regexNumberPasport, RegexOptions.IgnoreCase))
                throw new Exception("Вы не правели ввели номер паспорта");

            if (string.IsNullOrWhiteSpace(placeOfIssuePassport))
                throw new Exception("Вы не ввели место выдачи паспорта");
            if (dateOfIssuePassport.Value.Date.ToShortDateString() == "01.01.0001")
                throw new Exception("Вы не выбрали дату выдачи паспорта");

            else
                return true;
        }
        /// <summary>
        /// Дбовляет нового пассажира
        /// </summary>
        /// <param name="firstName">Имя пассажира</param>
        /// <param name="lastName">Фамилия пассажира</param>
        /// <param name="patronymic">Отчество пассажира</param>
        /// <param name="dateOfBirth">Дата рождения пассажира</param>
        /// <param name="numberPasport">Номер паспорта</param>
        /// <param name="placeOfIssuePassport">Место выдачи паспорта</param>
        /// <param name="dateOfIssuePassport">Дата выдачи паспорта.</param>
        /// <returns>
        public void AddPassenger(string firstName, string lastName, string patronymic, DateTime dateOfBirth, string numberPasport, string placeOfIssuePassport, DateTime dateOfIssuePassport)
        {
            Passenger newPassenger = new Passenger()
            {
                FirstName = firstName,
                LastName = lastName,
                Patronymic = patronymic,
                DateOfBirth = dateOfBirth,
                PasportNumber = numberPasport,
                PlaceOfIssuePassport = placeOfIssuePassport,
                DateOfIssuePassport = dateOfIssuePassport,
            };
            if (!(String.IsNullOrWhiteSpace(firstName)))
            {
                newPassenger.FirstName = firstName;
                newPassenger.LastName = lastName;
                newPassenger.Patronymic = patronymic;
                newPassenger.DateOfBirth = dateOfBirth;
                newPassenger.PasportNumber = numberPasport;
                newPassenger.PlaceOfIssuePassport = placeOfIssuePassport;
                newPassenger.DateOfIssuePassport = dateOfIssuePassport;
            }
            db.context.Passenger.Add(newPassenger);
            db.context.SaveChanges();
        }
        /// <summary>
        /// Проверяет корректность данных для редактирования пассажира
        /// </summary>
        /// <param name="firstName">Имя пассажира</param>
        /// <param name="lastName">Фамилия пассажира</param>
        /// <param name="patronymic">Отчество пассажира</param>
        /// <param name="dateOfBirth">Дата рождения пассажира</param>
        /// <param name="numberPasport">Номер паспорта</param>
        /// <param name="placeOfIssuePassport">Место выдачи паспорта</param>
        /// <param name="dateOfIssuePassport">Дата выдачи паспорта.</param>
        /// <returns>
        /// Возвращается исключения, если данные не корректны
        /// </returns>
        public bool CheckEditPassenger(string firstName, string lastName, string patronymic, DateTime? dateOfBirth, string numberPasport, string placeOfIssuePassport, DateTime? dateOfIssuePassport)
        {
            if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName) && string.IsNullOrEmpty(patronymic) && dateOfBirth.Value.Date.ToShortDateString() == "01.01.0001" && string.IsNullOrEmpty(numberPasport) && string.IsNullOrEmpty(placeOfIssuePassport) && dateOfIssuePassport.Value.Date.ToShortDateString() == "01.01.0001")
                throw new Exception("Вы не ввели никакие данные");

            if (!string.IsNullOrEmpty(numberPasport))
            {
                if (!Regex.IsMatch(numberPasport, regexNumberPasport, RegexOptions.IgnoreCase))
                    throw new Exception("Вы не правели ввели номер паспорта");
            }

            return true;
        }
        /// <summary>
        /// Редактирует нового пассажира
        /// </summary>
        /// <param name="firstName">Имя пассажира</param>
        /// <param name="lastName">Фамилия пассажира</param>
        /// <param name="patronymic">Отчество пассажира</param>
        /// <param name="dateOfBirth">Дата рождения пассажира</param>
        /// <param name="numberPasport">Номер паспорта</param>
        /// <param name="placeOfIssuePassport">Место выдачи паспорта</param>
        /// <param name="dateOfIssuePassport">Дата выдачи паспорта.</param>
        /// <returns>
        public void EditPassenger(int idPassenger, string firstName, string lastName, string patronymic, DateTime? dateOfBirth, string numberPasport, string placeOfIssuePassport, DateTime? dateOfIssuePassport)
        {
            if (!string.IsNullOrEmpty(firstName))
            {
                var objFirstName = db.context.Passenger.Where(x => x.IDPassenger == idPassenger).FirstOrDefault();
                objFirstName.FirstName = firstName;
            }
            if (!string.IsNullOrEmpty(lastName))
            {
                var objLastName = db.context.Passenger.Where(x => x.IDPassenger == idPassenger).FirstOrDefault();
                objLastName.LastName = lastName;
            }
            if (!string.IsNullOrEmpty(patronymic))
            {
                var objPatronymic = db.context.Passenger.Where(x => x.IDPassenger == idPassenger).FirstOrDefault();
                objPatronymic.Patronymic = patronymic;
            }
            if (dateOfBirth.Value.Date.ToShortDateString() != "01.01.0001")
            {
                var objDateOfBirth = db.context.Passenger.Where(x => x.IDPassenger == idPassenger).FirstOrDefault();
                objDateOfBirth.DateOfBirth = dateOfBirth;
            }
            if (!string.IsNullOrEmpty(numberPasport))
            {
                var objPasportNumber = db.context.Passenger.Where(x => x.IDPassenger == idPassenger).FirstOrDefault();
                objPasportNumber.PasportNumber = numberPasport;
            }
            if (!string.IsNullOrEmpty(placeOfIssuePassport))
            {
                var objPlaceOfIssuePassport = db.context.Passenger.Where(x => x.IDPassenger == idPassenger).FirstOrDefault();
                objPlaceOfIssuePassport.PlaceOfIssuePassport = placeOfIssuePassport;
            }
            if (dateOfIssuePassport.Value.Date.ToShortDateString() != "01.01.0001")
            {
                var objDateOfIssuePassport = db.context.Passenger.Where(x => x.IDPassenger == idPassenger).FirstOrDefault();
                objDateOfIssuePassport.DateOfIssuePassport = dateOfIssuePassport;
            }
            db.context.SaveChanges();
        }
        /// <summary>
        /// Удаляет пассажира по id пассажира
        /// </summary>
        /// <param name="idPassenger">Идентификатор пассажира</param>
        public static void DeletePassenger(int idPassenger)
        {
            db.context.Passenger.Remove(db.context.Passenger.Single(x => x.IDPassenger == idPassenger));
            db.context.SaveChanges();
        }
        /// <summary>
        /// Отменяет регистрацию по id билета
        /// </summary>
        /// <param name="idTicket">Идентификатор билета</param>
        public static void CancleRegis(int idTicket)
        {
            db.context.Ticket_Passenger.Remove(db.context.Ticket_Passenger.Single(x => x.IDTicket == idTicket));
            db.context.SaveChanges();
        }
    }
}
