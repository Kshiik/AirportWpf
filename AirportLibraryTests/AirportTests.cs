using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AirportLibrary;
using System.Diagnostics;

namespace AirportLibraryTests
{
    [TestClass]
    public class AirportTests
    {

        /// <summary>
        /// Позитивный тест формы авторизации на корректый ввод данных, присутствующего пользователя в базе
        /// </summary>
        [TestMethod]
        public void CheckAutorization_IsCorrect_ResultTrue()
        {
            //Arrange
            string login = "admin";
            string password = "admin";
            //act
            bool result = UserClass.CheckAuth(login, password);
            //assert
            Assert.IsTrue(result);
        }
        /// <summary>
        /// Негативный тест формы авторизации на пустые стрики login и password
        /// </summary>
        [TestMethod]
        public void CheckAutorization_IsEmpty_ResultFalse()
        {
            //Arrange
            string login = "";
            string password = "";
            //act
            Action result = () => UserClass.CheckAuth(login, password);
            //assert
            Assert.ThrowsException<Exception>(result);
        }
        /// <summary>
        /// Негативный тест формы авторизации на пустую стрику password
        /// </summary>
        [TestMethod]
        public void CheckAutorization_PasswordIsEmpty_ResultFalse()
        {
            //Arrange
            string login = "admin";
            string password = "";
            //act
            Action result = () => UserClass.CheckAuth(login, password);
            //assert
            Assert.ThrowsException<Exception>(result);
        }
        /// <summary>
        /// Негативный тест формы авторизации на пустую стрику login
        /// </summary>
        [TestMethod]
        public void CheckAutorization_LoginIsEmpty_ResultFalse()
        {
            //Arrange
            string login = "";
            string password = "admin";
            //act
            Action result = () => UserClass.CheckAuth(login, password);
            //assert
            Assert.ThrowsException<Exception>(result);
        }
        /// <summary>
        /// Негативный тест формы авторизации на неверный login
        /// </summary>
        [TestMethod]
        public void CheckAutorization_LoginIsUncorrct_ResultFalse()
        {
            //Arrange
            string login = "adminn";
            string password = "admin";
            //act
            Action result = () => UserClass.CheckAuth(login, password);
            //assert
            Assert.ThrowsException<Exception>(result);
        }
        /// <summary>
        /// Негативный тест формы авторизации на неверный password
        /// </summary>
        [TestMethod]
        public void CheckAutorization_PasswordIsUncorrct_ResultFalse()
        {
            //Arrange
            string login = "admin";
            string password = "adminn";
            //act
            Action result = () => UserClass.CheckAuth(login, password);
            //assert
            Assert.ThrowsException<Exception>(result);
        }
        /// <summary>
        /// тест на удаление данных из таблицу Flight
        /// </summary>
        [TestMethod]
        public void DeleteFlight_IsCorrect_ResultTrue()
        {
            //Arrange
            int idFlight = 14;
            //act
            FlightClass.DeleteFlight(idFlight);
            //assert
        }
        /// <summary>
        /// тест на удаление данных из таблицу Passenger
        /// </summary>
        [TestMethod]
        public void DeletePassenger_IsCorrect_ResultTrue()
        {
            //Arrange
            int idPassenger = 12;
            //act
            PassengerClass.DeletePassenger(idPassenger);
            //assert
        }
        /// <summary>
        /// тест на проверку данных для добовления данных в таблицу Flight
        /// </summary>
        [TestMethod]
        public void CheckAddFlight_IsCorrect_ResultTrue()
        {
            //Arrange
            int idAirline = 1;
            int idDepartureAirport = 1;
            int idArrivalAirport = 2;
            string totalNumberOfSeats = "200";
            string numberOfAvailableSeats = "100";
            DateTime? departureDate = DateTime.Parse("01.01.2020");
            string departureTime = "18:00";
            //act
            bool result = FlightClass.CheckAddFlight(idAirline, idDepartureAirport, idArrivalAirport, totalNumberOfSeats, numberOfAvailableSeats, departureDate, departureTime);
            //assert
            Assert.IsTrue(result);
        }
        /// <summary>
        /// тест на добовления данных в таблицу Flight
        /// </summary>
        [TestMethod]
        public void AddFlight_IsCorrect_ResultTrue()
        {
            //Arrange
            int idAirline = 1;
            int idDepartureAirport = 1;
            int idArrivalAirport = 2;
            string totalNumberOfSeats = "200";
            string numberOfAvailableSeats = "100";
            DateTime? departureDate = DateTime.Parse("01.01.2020");
            string departureTime = "18:00";
            TimeSpan.TryParse(departureTime, out TimeSpan departureTimeFlight);
            //act
            FlightClass.AddFlight(idAirline, idDepartureAirport, idArrivalAirport, Convert.ToInt32(totalNumberOfSeats), Convert.ToInt32(numberOfAvailableSeats), departureDate, departureTimeFlight);
            //assert
        }
        /// <summary>
        /// тест на добовления данных в таблицу Passenger
        /// </summary>
        [TestMethod]
        public void AddPassenger_IsCorrect_ResultTrue()
        {
            //Arrange
            string firstName = "Ульяна";
            string lastName = "Важенина";
            string patronymic = "Романовна";
            DateTime dateOfBirth = DateTime.Parse("24.01.2005");
            string numberPasport = "1234-896547";
            string placeOfIssuePassport = "место";
            DateTime dateOfIssuePassport = DateTime.Parse("06.06.2019");
            //act
            PassengerClass.AddPassenger(firstName, lastName, patronymic, dateOfBirth, numberPasport, placeOfIssuePassport, dateOfIssuePassport);
            //assert
        }
        /// <summary>
        /// тест на добовления данных в таблицу Ticket_Passenger
        /// </summary>
        [TestMethod]
        public void AddTicket_IsCorrect_ResultTrue()
        {
            //Arrange
            string numberTicket = "12345";
            int idFlight = 1;
            int idPassenger = 12;
            int price = 10000;
            //act
            Ticket_PassengerClass.AddTicket(numberTicket, idFlight, idPassenger, price);
            //assert
        }
        /// <summary>
        /// тест на добовления данных в таблицу Users
        /// </summary>
        [TestMethod]
        public void AddUsers_IsCorrect_ResultTrue()
        {
            //Arrange
            int idRole = 1;
            string login = "ulyana";
            string password = "123";
            //act
            UserClass.AddUser(idRole, login, password);
            //assert
        }
        
    }
}
