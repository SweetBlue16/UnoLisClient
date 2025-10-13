using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Mail;
using UnoLisClient.UI.Properties.Langs;

// TODO: Add validation for registration 
namespace UnoLisClient.UI.Validators
{
    public static class UserValidator
    {
        private const int MinNicknameLength = 3;
        private const int MaxNicknameLength = 20;
        private const int MaxFullnameLength = 100;

        private static readonly Regex _nicknameRegex = new Regex("^[a-zA-Z0-9_-]+$");
        private static readonly Regex _strongPasswordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,16}$");

        // Parameter will be Account instead of nickname and password
        public static List<string> ValidateLoginEmptyFields(string nickname, string password)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(nickname))
            {
                errors.Add(ErrorMessages.NicknameEmptyMessageLabel);
            }
            
            if (string.IsNullOrWhiteSpace(password))
            {
                errors.Add(ErrorMessages.PasswordEmptyMessageLabel);
            }
            return errors;
        }

        // Parameter will be Account and Player instead of all the parameters here
        public static List<string> ValidateProfileUpdate(string fullName, string email,
                                                         string currentPassword, string newPassword,
                                                         string facebookUrl, string instagramUrl, string tikTokUrl)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(fullName) && fullName.Length > MaxFullnameLength)
            {
                errors.Add(string.Format(ErrorMessages.FullNameLengthMessageLabel, MaxFullnameLength));
            }

            errors.AddRange(ValidateEmail(email));

            if (!string.IsNullOrWhiteSpace(newPassword) || !string.IsNullOrWhiteSpace(currentPassword))
            {
                errors.AddRange(ValidatePasswordChange(currentPassword, newPassword));
            }

            errors.AddRange(ValidateSocialMediaLink(facebookUrl, "facebook.com", "Facebook"));
            errors.AddRange(ValidateSocialMediaLink(instagramUrl, "instagram.com", "Instagram"));
            errors.AddRange(ValidateSocialMediaLink(tikTokUrl, "tiktok.com", "TikTok"));

            return errors;
        }

        private static List<string> ValidateEmail(string email)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(email))
            {
                errors.Add(ErrorMessages.EmailEmptyMessageLabel);
                return errors;
            }

            try
            {
                var mailAddress = new MailAddress(email);
            }
            catch (FormatException)
            {
                errors.Add(ErrorMessages.EmailFormatMessageLabel);
            }
            return errors;
        }

        private static List<string> ValidatePasswordChange(string currentPassword, string newPassword)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                errors.Add(ErrorMessages.NewPasswordEmptyMessageLabel);
            }
            else if (!_strongPasswordRegex.IsMatch(newPassword))
            {
                errors.Add(ErrorMessages.WeakPasswordMessageLabel);
            }
            return errors;
        }

        private static List<string> ValidateSocialMediaLink(string url, string expectedDomain, string platformName)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(url))
            {
                return errors;
            }

            if (!Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult) || (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
            {
                errors.Add(string.Format(ErrorMessages.SocialLinkFormatMessageLabel, platformName));
            }
            else if (!uriResult.Host.Contains(expectedDomain))
            {
                errors.Add(string.Format(ErrorMessages.SocialLinkDomainMessageLabel, platformName));
            }
            return errors;
        }
    }
}
