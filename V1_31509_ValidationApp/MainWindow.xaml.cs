using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.RegularExpressions;
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

namespace V1_31509_ValidationApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string _apiUrl = "http://localhost:4444/TransferSimulator/snils";
        private HttpClient _httpClient;
        private string _rawValue = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        private async void GetData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var respone = await _httpClient.GetFromJsonAsync<ValueRespone>(_apiUrl);

                if (respone != null)
                {
                    _rawValue = respone.Value;
                    RawValue.Text = _rawValue;

                    ResultValue.Text = null;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SendResult_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_rawValue))
            {
                ResultValue.Text = "Сначала получите данные!";
                return;
            }
            
            if (ValidateSnils(_rawValue))
            {
                ResultValue.Text = "Данные корректны";
                return ;
            }

            ResultValue.Text = "Данные некорректны";

        }

        private bool ValidateSnils(string value)
        {
            return Regex.IsMatch(value, @"^\d{3}\-\d{3}\-\d{3}\s\d{2}$");
        }

    }

    public class ValueRespone
    {
        public string Value { get; set; }
    }
}
