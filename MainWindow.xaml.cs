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

namespace V_31509_BreadProduction
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<User> Users;
        public static List<Role> Roles;

        public MainWindow()
        {
            InitializeComponent();
            Manager.WindowFrame = MainFrame;
            MainFrame.Navigate(new LoginPage());

            Users = new List<User>
            {
                new User { Id = 1, Login = "admin", Password = "admin", RoleId = 1, IsBlocked = false },
                new User { Id = 2, Login = "user", Password = "user", RoleId = 2, IsBlocked = false },
                new User { Id = 3, Login = "user1", Password = "user2", RoleId = 2, IsBlocked = false },
                new User { Id = 4, Login = "blockuser", Password = "user", RoleId = 2, IsBlocked = true },
                new User { Id = 5, Login = "moder", Password = "moder", RoleId = 2, IsBlocked = false },
            };

            Roles = new List<Role>
            {
                new Role {Id = 1, Name = "Администратор"},
                new Role {Id = 1, Name = "Пользователь"}
            };
        }
    }
}
