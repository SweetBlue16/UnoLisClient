using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text.RegularExpressions;
using UnoLisClient.Logic.UnoLisServerReference.Login;
using UnoLisClient.Logic.UnoLisServerReference.ProfileEdit;
using UnoLisClient.Logic.UnoLisServerReference.Register;
using UnoLisClient.UI.Properties.Langs;

namespace UnoLisClient.UI.Utilities
{
    public static class UserValidator
    {
        private const int MinNicknameLength = 3;
        private const int MaxNicknameLength = 45;
        private const int MaxFullnameLength = 45;
        private const int MinPasswordLength = 8;
        private const int MaxPasswordLength = 255;

        private static readonly Regex _nicknameRegex = new Regex("^[a-zA-Z0-9_-]+$");
        private static readonly Regex _strongPasswordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&\-_])[A-Za-z\d@$!%*?&\-_]{8,255}$");

        public static List<string> ValidateRegistration(RegistrationData registrationData, string rewritedPassword)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(registrationData.Nickname))
            {
                errors.Add(ErrorMessages.NicknameEmptyMessageLabel);
            }
            else
            {
                if (!_nicknameRegex.IsMatch(registrationData.Nickname))
                {
                    errors.Add(ErrorMessages.NicknameFormatMessageLabel);
                }
                if (registrationData.Nickname.Length < MinNicknameLength || registrationData.Nickname.Length > MaxNicknameLength)
                {
                    errors.Add(string.Format(ErrorMessages.NicknameLengthMessageLabel, MinNicknameLength, MaxNicknameLength));
                }
            }

            if (string.IsNullOrWhiteSpace(registrationData.FullName))
            {
                errors.Add(ErrorMessages.FullNameEmptyMessageLabel);
            }
            else if (registrationData.FullName.Length > MaxFullnameLength)
            {
                errors.Add(ErrorMessages.FullNameLengthMessageLabel);
            }

            errors.AddRange(ValidateEmail(registrationData.Email));

            if (string.IsNullOrWhiteSpace(registrationData.Password))
            {
                errors.Add(ErrorMessages.PasswordEmptyMessageLabel);
            }
            else if (!_strongPasswordRegex.IsMatch(registrationData.Password))
            {
                errors.Add(ErrorMessages.WeakPasswordMessageLabel);
            }
            if (!registrationData.Password.Equals(rewritedPassword))
            {
                errors.Add(ErrorMessages.PasswordDontMatchMessageLabel);
            }

            return errors;
        }

        public static List<string> ValidateLogin(AuthCredentials credentials)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(credentials.Nickname))
            {
                errors.Add(ErrorMessages.NicknameEmptyMessageLabel);
            }
            if (string.IsNullOrWhiteSpace(credentials.Password))
            {
                errors.Add(ErrorMessages.PasswordEmptyMessageLabel);
            }
            return errors;
        }

        public static List<string> ValidateProfileUpdate(ProfileData profileData)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(profileData.FullName))
            {
                errors.Add(ErrorMessages.FullNameEmptyMessageLabel);
            }
            else if (profileData.FullName.Length > MaxFullnameLength)
            {
                errors.Add(string.Format(ErrorMessages.FullNameLengthMessageLabel, MaxFullnameLength));
            }

            errors.AddRange(ValidateEmail(profileData.Email));

            if (!string.IsNullOrWhiteSpace(profileData.Password))
            {
                if (!_strongPasswordRegex.IsMatch(profileData.Password))
                {
                    errors.Add(ErrorMessages.WeakPasswordMessageLabel);
                }
                else if (profileData.Password.Length < MinPasswordLength || profileData.Password.Length > MaxPasswordLength)
                {
                    errors.Add(string.Format(ErrorMessages.PasswordLengthMessageLabel, MinPasswordLength, MaxPasswordLength));
                }
            }

            errors.AddRange(ValidateSocialMediaLink(profileData.FacebookUrl, "facebook.com", "Facebook"));
            errors.AddRange(ValidateSocialMediaLink(profileData.InstagramUrl, "instagram.com", "Instagram"));
            errors.AddRange(ValidateSocialMediaLink(profileData.TikTokUrl, "tiktok.com", "TikTok"));

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
