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
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        Core db = new Core();
        public AuthPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox1.Password;

            if (string.IsNullOrEmpty(login))
                ErrorLogin.Fill = new SolidColorBrush(Color.FromRgb(216, 52, 21));
            else
                ErrorLogin.Fill = Brushes.White;
            if (string.IsNullOrEmpty(password))
                ErrorPassword.Fill = new SolidColorBrush(Color.FromRgb(216, 52, 21));
            else
                ErrorPassword.Fill = Brushes.White;

            try
            {
                if (UsersController.CheckAuth(login, password))
                {
                    foreach (var user in db.context.User.ToList().Where(x => x.Login == login && x.Password == password))
                    {
                        Properties.Settings.Default.idRole = (int)user.RoleID;
                        Properties.Settings.Default.Save();
                    }
                }
                this.NavigationService.Navigate(new FlightPage(0));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
