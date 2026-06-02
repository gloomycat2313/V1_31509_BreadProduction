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
    /// Логика взаимодействия для UserListPage.xaml
    /// </summary>
    public partial class UserListPage : Page
    {
        public UserListPage()
        {
            InitializeComponent();
            DgUsers.ItemsSource = MainWindow.Users.ToList();
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            var usersForRemoving = DgUsers.SelectedItems.Cast<User>().ToList();

            if (MessageBox.Show($"Вы уверены что хотите удалить {usersForRemoving.Count} элементов?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    foreach (var user in usersForRemoving)
                    {
                        MainWindow.Users.Remove(user);
                    }

                    MessageBox.Show("Данные успешно удалены!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            DgUsers.ItemsSource = MainWindow.Users.ToList();
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            Manager.WindowFrame.Navigate(new UserAddEditPage(null));
        }

        private void UserEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.WindowFrame.Navigate(new UserAddEditPage((sender as Button).DataContext as User));
        }

        private void UnBlockuser_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = (sender as Button).DataContext as User;

            if (selectedUser == null)
            {
                MessageBox.Show("Выберите пользователя!");
                return;
            }

            var existingUser = MainWindow.Users.FirstOrDefault(u => u.Id == selectedUser.Id);

            if (existingUser == null)
            {
                MessageBox.Show("Пользователь не существует");
                return;
            }

            if (existingUser.IsBlocked)
            {
                try
                {
                    existingUser.IsBlocked = false;
                    MessageBox.Show("Пользователь разблокирован");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Пользователь не азблокирован");
                return;
            }

            DgUsers.ItemsSource = MainWindow.Users.ToList();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                DgUsers.ItemsSource = MainWindow.Users.ToList();
            }
        }
    }
}
