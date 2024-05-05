using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using RockLife.Interfaces;
using RockLife.Login;

namespace RockLife.Login
{
    class RegistrationServices
    {
        public static void ShowErrorPopup(Button placementTarget, string errorMessage)
        {
            //Popup popup = new Popup();
            //TextBlock textBlock = new TextBlock();
            //textBlock.Text = errorMessage;
            //textBlock.Background = Brushes.White;
            //textBlock.Foreground = Brushes.Black;
            //textBlock.Padding = new Thickness(10);
            //popup.Child = textBlock;
            //popup.Width = 400;
            //popup.Height = 100;
            //popup.PlacementTarget = placementTarget;
            //popup.Placement = PlacementMode.Bottom;
            //popup.IsOpen = true;

            Popup popup = new Popup
            {
                AllowsTransparency = true, 
                PlacementTarget = placementTarget,
                Placement = PlacementMode.Bottom,
                StaysOpen = false 
            };

            TextBlock textBlock = new TextBlock
            {
                Text = errorMessage,
                Background = Brushes.White,
                Foreground = Brushes.Black,
                Padding = new Thickness(10),
                TextWrapping = TextWrapping.Wrap 
            };

            Border border = new Border
            {
                Background = Brushes.White, 
                Child = textBlock,
                CornerRadius = new CornerRadius(5), 
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1),
                Padding = new Thickness(10)
            };

            popup.Child = border; 

            popup.IsOpen = true;
        }
    }

    public class LoginValidationStrategy : IRegistrationValidationStrategy
    {
        public bool Validate(string login, string mail, string password, string passwordConf, out string errorMessage)
        {
            errorMessage = "Непраильно введён логин!\nЛогин должен содержать только латинские буквы.";
            return RegistrationValidator.IsValidLogin(login);
        }
    }

    public class EmailValidationStrategy : IRegistrationValidationStrategy
    {
        public bool Validate(string login, string mail, string password, string passwordConf, out string errorMessage)
        {
            errorMessage = "Непраильный формат электронной почты!\nВведите электронную почту в формате example@email.com.";
            return RegistrationValidator.IsValidEmail(mail);
        }
    }
    public class PasswordValidationStrategy : IRegistrationValidationStrategy
    {
        public bool Validate(string login, string mail, string password, string passwordConf, out string errorMessage)
        {
            errorMessage = "Непраильный формат пароля!\nПароль должен содержать латинские буквы и цифры.";
            return RegistrationValidator.IsValidPassword(password);
        }
    }
    public class PasswordConfValidationStrategy : IRegistrationValidationStrategy
    {
        public bool Validate(string login, string mail, string password, string passwordConf, out string errorMessage)
        {
            errorMessage = "Пароли не совпадают!";
            return RegistrationValidator.IsValidPasswordConf(password, passwordConf);
        }
    }
}


