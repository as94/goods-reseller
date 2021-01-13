using System;
using System.Net.Mail;

namespace GoodsReseller.AuthContext.Domain.ValidationRules
{
    public static class EmailValidator
    {
        public static bool IsValid(string email, out MailAddress mailAddress)
        {
            if (email == null)
            {
                throw new ArgumentNullException(nameof(email));
            }
            
            try
            {
                mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                mailAddress = null;
                return false;
            }
        }
    }
}