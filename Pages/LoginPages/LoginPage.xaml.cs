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

namespace V_31509_BreadProduction.Pages.LoginPages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private const int MaxAttempts = 3;

        private int _failedLoginAttempts = 0;

        private bool IsCaptchaPassed = false;

        private ComboBox[] _comboBoxes;
        private Image[] _targetImages;
        private Image[] _sourceImage;

        public LoginPage()
        {
            InitializeComponent();

            _comboBoxes = new[] { Cb1, Cb2, Cb3, Cb4};
            _targetImages = new[] { TargImg1, TargImg2, TargImg3, TargImg4 };
            _sourceImage = new[] { SrcImage1, SrcImage2, SrcImage3, SrcImage4 };

        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(TbxLogin.Text))
                errors.AppendLine("Укажите логин");
            if (string.IsNullOrWhiteSpace(PsbPassword.Password))
                errors.AppendLine("Укажите пароль");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            var exsitingUser = MainWindow.Users.FirstOrDefault(u => u.Login == TbxLogin.Text);

            if (exsitingUser == null)
            {
                MessageBox.Show("Пользователь не существует");
                return;
            }

            if (exsitingUser.IsBlocked)
            {
                MessageBox.Show("Пользователь заблокирован. Обратитесь к администратору");
                return;
            }

            if (!IsCaptchaPassed)
            {
                if (IsCaptchaSolved())
                {
                    MessageBox.Show("Смарт-капча решена!");
                    IsCaptchaPassed = true;
                    _failedLoginAttempts = 0;
                }
                else
                {
                    _failedLoginAttempts++;
                    MessageBox.Show($"Неверный порядок фрагментов! Осталось попыток: {MaxAttempts - _failedLoginAttempts}.");
                    if (_failedLoginAttempts >= MaxAttempts)
                    {
                        if (!exsitingUser.IsBlocked)
                        {
                            _failedLoginAttempts = 0;
                            exsitingUser.IsBlocked = true;
                            MessageBox.Show("Пользователь был заблокирован. Обратитесь к админстратору");
                        }
                    }
                    return;
                }
            }

            if (exsitingUser.Password == PsbPassword.Password)
            {
                IsCaptchaPassed = false;
                _failedLoginAttempts = 0;
                UserSession.Login(exsitingUser);
                MessageBox.Show($"Добро пожаловать {MapRoles(exsitingUser.RoleId)}");

                Manager.WindowFrame.Navigate(new MainPage());
            }
            else
            {
                _failedLoginAttempts++;
                MessageBox.Show($"Неверный логин или пароль! Осталось попыток {MaxAttempts - _failedLoginAttempts}.");
                if (_failedLoginAttempts >= MaxAttempts)
                {
                    if (!exsitingUser.IsBlocked)
                    {
                        _failedLoginAttempts = 0;
                        exsitingUser.IsBlocked = true;
                        MessageBox.Show("Пользователь был заблокирован. Обратитесь к админстратору");
                    }
                    return;
                }
            }
        }

        private string MapRoles(int roleId)
        {
            switch (roleId)
            {
                case 1:
                    return "Администратор";
                case 2:
                    return "Клиент";
            }

            return string.Empty;
        }

        private void Cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var image in _targetImages)
            {
                image.Source = null;
            }

            for (int i = 0; i < _comboBoxes.Length; i++)
            {
                if (_comboBoxes[i].SelectedIndex >= 0)
                {
                    var cellIndex = _comboBoxes[i].SelectedIndex;

                    _targetImages[cellIndex].Source = _sourceImage[i].Source;
                }
            }
        }

        private bool IsCaptchaSolved()
        {
            for (int i = 0; i < _comboBoxes.Length; i++)
            {
                if (_comboBoxes[i].SelectedIndex != i)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
