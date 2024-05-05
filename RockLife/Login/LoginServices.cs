using RockLife.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RockLife.Login
{
    internal class LoginService
    {
        public void ProcessLogin(string login, string password, LoginWindow loginWindow)
        {
            User user = new User();

            //if (user.ValidateLogin(login, password))
            //{
            //    ShowAccountWindow(loginWindow);
            //}
            //else
            //{
            //    MessageBox.Show("Неверный логин или пароль. Попробуйте снова.");
            //}
        }

        private void ShowAccountWindow(LoginWindow loginWindow)
        {
            AccountWindow accountWindow = new AccountWindow();
            accountWindow.Show();
            loginWindow.Close();
        }
    }
}
