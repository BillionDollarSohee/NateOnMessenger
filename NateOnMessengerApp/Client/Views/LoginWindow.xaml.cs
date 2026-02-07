using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using client.Models;

namespace client.Views
{
    public partial class LoginWindow : Window
    {
        private readonly HttpClient _httpClient;
        private const string API_BASE_URL = "http://localhost:5190";

        public LoginWindow()
        {
            InitializeComponent();
            _httpClient = new HttpClient { BaseAddress = new Uri(API_BASE_URL) };
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            await AttemptLogin();
        }

        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginButton_Click(sender, e);
            }
        }

        private async Task AttemptLogin()
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password;

            // 유효성 검사
            if (string.IsNullOrWhiteSpace(username))
            {
                ShowError("아이디를 입력해주세요.");
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                ShowError("비밀번호를 입력해주세요.");
                txtPassword.Focus();
                return;
            }

            // 로그인 시도
            try
            {
                var loginRequest = new { Username = username, Password = password };
                var response = await _httpClient.PostAsJsonAsync("/api/user/login", loginRequest);

                if (response.IsSuccessStatusCode)
                {
                    var user = await response.Content.ReadFromJsonAsync<User>();

                    // 로그인 성공 - 메인 화면으로 이동
                    var mainWindow = new MainWindow(user);
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    ShowError("아이디 또는 비밀번호가 일치하지 않습니다.");
                }
            }
            catch (HttpRequestException)
            {
                ShowError("서버에 연결할 수 없습니다. 서버가 실행 중인지 확인해주세요.");
            }
            catch (Exception ex)
            {
                ShowError($"로그인 중 오류가 발생했습니다: {ex.Message}");
            }
        }

        private void ShowError(string message)
        {
            txtError.Text = message;
            txtError.Visibility = Visibility.Visible;
        }
    }
}