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
using V_31509_BreadProduction.ApplicationData.Utilities;

namespace V_31509_BreadProduction.Pages.UserPages
{
    /// <summary>
    /// Логика взаимодействия для UserAddEditPage.xaml
    /// </summary>
    public partial class UserAddEditPage : Page
    {
        private User _currentUser;

        public UserAddEditPage(User selectedUser)
        {
            InitializeComponent();

            if (selectedUser != null)
            {
                _currentUser = selectedUser;
            }
            else
            {
                _currentUser = new User();
            }

            DataContext = _currentUser;

            CbRoles.ItemsSource = MainWindow.Roles.ToList();
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentUser.Login))
                errors.AppendLine("Укажите логин");

            if (_currentUser.Id == 0)
            {
                if (string.IsNullOrWhiteSpace(PsbPassword.Password))
                    errors.AppendLine("Укажите пароль");
            }

            if (_currentUser.Role == null)
                errors.AppendLine("Укажите роль");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            var IsLoginExist = MainWindow.Users.Any(u => u.Login == _currentUser.Login && u.Id != _currentUser.Id);
            if (IsLoginExist)
            {
                MessageBox.Show("Логин занят");
                return; 
            }

            if (_currentUser.Id == 0)
            {
                int roleId = MainWindow.Roles.FirstOrDefault(r => r.Id == _currentUser.Role.Id).Id;
                int newId = MainWindow.Users.Count + 1;

                _currentUser.Id = newId;
                _currentUser.RoleId = roleId;
                MainWindow.Users.Add(_currentUser);
            }
            else
            {
                var existingUser = MainWindow.Users.FirstOrDefault(u => u.Id == _currentUser.Id);

                if (existingUser == null)
                {
                    MessageBox.Show("Пользователь не найден");
                    return;
                }

                existingUser.Id = _currentUser.Id;
                existingUser.RoleId = _currentUser.RoleId;
                existingUser.IsBlocked = _currentUser.IsBlocked;
                existingUser.Login = _currentUser.Login;
                existingUser.Password = string.IsNullOrEmpty(PsbPassword.Password) ? _currentUser.Password : PsbPassword.Password;

            }

            MessageBox.Show("Данные сохранены");
            Manager.WindowFrame.GoBack();
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.WindowFrame.GoBack();
        }
    }
}
