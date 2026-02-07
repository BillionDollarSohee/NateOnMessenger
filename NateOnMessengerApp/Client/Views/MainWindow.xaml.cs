using System.Windows;
using client.Models;

namespace client.Views
{
    public partial class MainWindow : Window
    {
        // WPF StartupUri용 기본 생성자
        public MainWindow()
        {
            InitializeComponent();
        }

        // 로그인 후 사용자 전달용 생성자
        public MainWindow(User? user) : this()
        {
            if (user != null)
            {
                txtWelcome.Text = $"{user.Username}님, 환영합니다!";
                this.Title = $"NateOn - {user.Username}";
            }
        }
    }
}
