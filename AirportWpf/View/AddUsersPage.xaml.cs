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
    /// Логика взаимодействия для AddUsersPage.xaml
    /// </summary>
    public partial class AddUsersPage : Page
    {
        Core db = new Core();
        public AddUsersPage()
        {
            InitializeComponent();

            RoleComboBox.ItemsSource = db.context.Role.ToList();
            RoleComboBox.DisplayMemberPath = "RoleName";
            RoleComboBox.SelectedValuePath = "IDRole";
        }

        private void RoleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idRole = RoleComboBox.SelectedIndex;
                string login = LoginTextBox.Text;
                string password = PasswordTextBox.Text;

                UsersController newUser = new UsersController();
                bool result = newUser.CheckAddUser(idRole, login, password);
                if (result)
                {
                    newUser.AddUser(idRole, login, password);
                    MessageBox.Show("Вы добавили нового пользователя. Возращение на страницу с рейсами");
                    this.NavigationService.Navigate(new FlightPage(0));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
