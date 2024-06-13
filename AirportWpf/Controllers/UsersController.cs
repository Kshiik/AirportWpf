using AirportWpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AirportWpf.Controllers
{
    public class UsersController
    {
        static Core db = new Core();
        /// <summary>
        /// Проверяет авторизацию пользователя по логину и паролю
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns>
        /// Возвращает если логин не введен, пароль не введен,
        /// пользователь не найден или пароль неверен.
        /// </returns>
        public static bool CheckAuth(string login, string password)
        {
            if (string.IsNullOrEmpty(login))
                throw new Exception("Вы не ввели логин");
            if (string.IsNullOrEmpty(password))
                throw new Exception("Вы не ввели пароль");
            int checkLogin = db.context.User.Where(x => x.Login == login).Count();
            int checkPassword = db.context.User.Where(x => x.Password == password).Count();
            if (checkLogin == 0)
                throw new Exception("Пользователь не найден");
            else
            {
                if (checkPassword == 0)
                    throw new Exception("Неправельный пароль");
            }

            return true;
        }
        /// <summary>
        /// Проверяет корректность данных для добавления пользователя
        /// </summary>
        /// <param name="idRole">Идентификатор роли</param>
        /// <param name="login">Логин пользователя</param>
        /// <param name="login">Пароль пользователя</param>
        /// <returns>
        /// True, если данные корректны; в противном случае возвращает исключение
        /// </returns>
        public bool CheckAddUser(int idRole, string login, string password)
        {
            if (idRole == -1)
                throw new Exception("Вы не выбрали роль пользователя");
            if (string.IsNullOrEmpty(login))
                throw new Exception("Вы не ввели логин пользователя");
            if (string.IsNullOrEmpty(password))
                throw new Exception("Вы не ввели пароль пользователя");

            else
                return true;
        }
        /// <summary>
        /// Добовляет
        /// </summary>
        /// <param name="idRole">Идентификатор пользователя</param>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        public void AddUser(int idRole, string login, string password)
        {

            User newUser = new User()
            {
                RoleID = idRole,
                Login = login,
                Password = password,
            };
            if (string.IsNullOrEmpty(login))
            {
                newUser.RoleID = idRole;
                newUser.Login = login;
                newUser.Password = password;
            }

            db.context.User.Add(newUser);
            db.context.SaveChanges();
        }
    }
}
