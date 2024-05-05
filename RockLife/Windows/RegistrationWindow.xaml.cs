using RockLife.Models;
using RockLife.Interfaces;
using RockLife.Login;
using RockLife.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace RockLife
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window, IRegistrations
    {
        private readonly UserRepository _userRepository;
        public RegistrationWindow()
        {
            InitializeComponent();
            _userRepository = new UserRepository(new MyAppContext());

        }
        public string LoginReg
        {
            get { return LoginField.Text; }
            set { LoginField.Text = value; }
        }

        public string MailReg
        {
            get { return MailField.Text; }
            set { MailField.Text = value; }
        }

        public string PasswordReg
        {
            get { return PasswordField.Password; }
            set { PasswordField.Password = value; }
        }

        public string PasswordConfReg
        {
            get { return PasswordConfField.Password; }
            set { PasswordConfField.Password = value; }
        }

        public async void ProcessRegistration()
        {
            string login = LoginReg;
            string mail = MailReg;
            string password = PasswordReg;
            string passwordConf = PasswordConfReg;

            IRegistrationValidationStrategy[] strategies = new IRegistrationValidationStrategy[]
            {
                new LoginValidationStrategy(),
                new EmailValidationStrategy(),
                new PasswordValidationStrategy(),
                new PasswordConfValidationStrategy()
            };

            foreach (var strategy in strategies)
            {
                string errorMessage;
                if(!strategy.Validate(login, mail, password, passwordConf, out errorMessage)) 
                {
                    RegistrationServices.ShowErrorPopup(RegistrationButton, errorMessage);
                    return;
                }
            }

            try
            {
                User newUser = new User
                {
                    Login = login,
                    Email = mail,
                    Password = password
                };

                await _userRepository.AddUserAsync(newUser);
                var userId = await _userRepository.GetUserIdByUsernameAsync(login);
                Application.Current.Properties["UserId"] = userId;

                MessageBox.Show("Регистрация успешно завершена!");

                Application.Current.Dispatcher.Invoke(() =>
                {
                    AccountWindow accountWindow = new AccountWindow();
                    accountWindow.Show();
                    Close();
                });
            }
            //catch (Exception ex)
            //{
            //    RegistrationServices.ShowErrorPopup(RegistrationButton, "Произошла ошибка при регистрации: " + ex.Message);
            //}
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                Console.WriteLine($"Ошибка обновления базы данных: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Внутреннее исключение: {ex.InnerException.Message}");
                }
                throw; // Переброс исключения
            }
            var currentWindow = this;

            
        }
        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            ProcessRegistration();
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
        }

        private void Button_Login(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Hide();
        }
    }
}
