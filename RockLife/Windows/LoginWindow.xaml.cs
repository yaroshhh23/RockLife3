using System.Windows;
using RockLife.Models;
using RockLife.Interfaces;
using RockLife.Login;
using RockLife.Repository;

namespace RockLife
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly UserRepository _userRepository;
        public LoginWindow()
        {
            InitializeComponent();
            _userRepository = new UserRepository(new MyAppContext());
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            this.Hide();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginField.Text;
            string password = PasswordField.Password;

            User user = null;
            try
            {
                var users =  await _userRepository.FindUsersByLoginAsync(login);
                user = users.FirstOrDefault();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при поиске пользователя: " + ex.Message);
                return;
            }

            if (user != null)
            {

                if (user.Password == password)
                {
                    var userId = await _userRepository.GetUserIdByUsernameAsync(login);
                    Application.Current.Properties["UserId"] = userId;

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Windows.SecondMainWindow secondMainWindow = new Windows.SecondMainWindow();
                        secondMainWindow.Show();
                        this.Hide();
                    });
                }

                else
                {
                    MessageBox.Show("Неверный пароль!");
                }
            }
            else
            {
                MessageBox.Show("Пользователь с таким логином не найден!");
            }
        }
    }
}
