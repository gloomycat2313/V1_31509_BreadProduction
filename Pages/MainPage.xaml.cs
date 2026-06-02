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
using V_31509_BreadProduction.Pages.LoginPages;
using V_31509_BreadProduction.Pages.UserPages;

namespace V_31509_BreadProduction.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            Manager.PageFrame = MainPageFrame;

            if (UserSession.CurrentUser.RoleId == 1)
            {
                OpenUsers.Visibility = Visibility.Visible;
            }
            else
            {
                OpenUsers.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenUsers_Click(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Navigate(new UserListPage());
        }

        private void OpenOrders_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Функционал не реализован");
            return;
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            if (UserSession.CurrentUser != null)
            {
                UserSession.Logout();
                Manager.WindowFrame.Navigate(new LoginPage());
            }
        }
    }
}
