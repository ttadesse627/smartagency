
using System.Security.Cryptography;
using AppDiv.SmartAgency.Application.Contracts.DTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using Org.BouncyCastle.Crypto.Prng;

namespace AppDiv.SmartAgency.Application.Service
{
    public class HelperService
    {
        private readonly ISettingRepository _settingRepository;

        public HelperService(ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
        }
        public static string GenerateRandomCode()
        {
            using var rngCryptoServiceProvider = RandomNumberGenerator.Create();
            var randomBytes = new byte[4];
            rngCryptoServiceProvider.GetBytes(randomBytes);

            var randomValue = BitConverter.ToUInt32(randomBytes, 0);
            var code = randomValue.ToString("D6");

            return code.Substring(0, 6);
        }

        public static int GetMonthDifference(DateTime startDate, DateTime endDate)
        {
            int monthsApart = (endDate.Year - startDate.Year) * 12 + (endDate.Month - startDate.Month);

            // Check if the end day is less than the start day
            if (endDate.Day < startDate.Day)
            {
                // Subtract one month if end day is less than start day
                monthsApart--;
            }

            return monthsApart;
        }
        public int getOtpExpiryDurationSetting()
        {
            var generalSetting = _settingRepository.GetAll().Where(s => s.Key.ToLower() == "generalsetting").FirstOrDefault();
            int expiryDuration = 15;
            if (generalSetting != null)
            {
                expiryDuration = generalSetting.Value.Value<int?>("otp_expiry_duration_in_days") ?? expiryDuration;
            }
            return expiryDuration;
        }
        public PasswordPolicy? getPasswordPolicySetting()
        {
            var passwordPolicy = _settingRepository.GetAll()
                .Where(s => s.Key.ToLower() == "passwordpolicy")
                .FirstOrDefault();
            return passwordPolicy == null ? null : new PasswordPolicy
            {
                Number = passwordPolicy.Value.Value<bool>("number"),
                LowerCase = passwordPolicy.Value.Value<bool>("lowerCase"),
                OtherChar = passwordPolicy.Value.Value<bool>("otherCharacter"),
                UpperCase = passwordPolicy.Value.Value<bool>("upperCase"),
                Min = passwordPolicy.Value.Value<int>("minLength"),
                Max = passwordPolicy.Value.Value<int>("maxLength")

            };
        }
    }




}