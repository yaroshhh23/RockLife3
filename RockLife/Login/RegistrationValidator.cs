using System.Text.RegularExpressions;

namespace RockLife.Login
{
    public class RegistrationValidator
    {
        public static bool IsValidLogin(string login) 
        {
            return Regex.IsMatch(login, @"^[a-zA-Z0-9]+$");
        }

        public static bool IsValidEmail(string mail)
        {
            return Regex.IsMatch(mail, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");
        }

        public static bool IsValidPassword(string password)
        {
            return Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$");
        }

        public static bool IsValidPasswordConf(string password, string passwordConf)
        {
            return password == passwordConf;
        }
        
    }
}
