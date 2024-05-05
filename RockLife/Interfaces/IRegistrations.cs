using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockLife.Interfaces
{
    public interface IRegistrations
    {
        string LoginReg { get; set; }
        string MailReg { get; set; }
        string PasswordReg { get; set; }
        string PasswordConfReg { get; set; }

        void ProcessRegistration();
    }

    public interface IRegistrationValidationStrategy
    {
        bool Validate(string login, string mail, string password, string passwordConf, out string errorMessage);
    }
}
