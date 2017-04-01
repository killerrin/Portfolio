﻿using Portfolio.API.Extensions;
using Portfolio.API.Models;
using Portfolio.API.Models.Enums;
using Portfolio.API.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Portfolio.API.Services
{
    public class AuthenticationService
    {
        private static Random _random = new Random();
        public static bool EnableUserCreation = true;

        private readonly IRepository<User> _userRepository;
        public AuthenticationService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        #region Password
        private const int WorkFactor = 13;
        public void FakeHash() => BCrypt.Net.BCrypt.HashPassword("", WorkFactor);
        public string HashPassword(int id, string password) => BCrypt.Net.BCrypt.HashPassword(password, WorkFactor + id);
        public bool VerifyPassword(string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);
        public bool IsPasswordExpired(DateTime? expiry)
        {
            if (expiry == null) return false;
            return expiry >= DateTime.UtcNow;
        }
        #endregion

        #region AuthToken
        public bool VerifyAuthToken(string authToken)
        {
            var user = _userRepository.GetAll()
                .Where(x => x.Auth_Token == authToken)
                .FirstOrDefault();

            if (user == null)
                return false;
            return true;
        }

        public string GenerateAuthToken(int id, string username)
        {
            string auth = BCrypt.Net.BCrypt.HashString($"{id}{username}{_random.Next()}");
            return auth;
        }
        #endregion

        #region Validations
        public bool ValidateUsername(string username)
        {
            if (!IntExtensions.IsBetween(username.Length, 1, 40))
                return false;

            if (!Regex.IsMatch(username, @"^[a-zA-Z0-9\s]"))
                return false;

            return true;
        }

        public bool ValidateEmail(string email)
        {
            if (Regex.IsMatch(email, @"^[\w!#$%&'*+/=?`{|}~^-]+(?:\.[!#$%&'*+/=?`{|}~^-]+)*@(?:[A-Z0-9-]+\.)+[A-Z]{2,6}$"))
                return false;
            return true;
        }

        public const PasswordScore MinimumPasswordStrength = PasswordScore.Medium;
        public bool ValidatePassword(string password)
        {
            PasswordScore passwordStrength = CheckPasswordStrength(password);
            Debug.WriteLine($"{passwordStrength.ToString()}");
            if ((int)passwordStrength >= (int)MinimumPasswordStrength)
                return true;
            return false;
        }
        #endregion

        public PasswordScore CheckPasswordStrength(string password)
        {
            int score = 0;

            if (string.IsNullOrWhiteSpace(password) || password.Length < 1)
                return PasswordScore.Blank;

            if (password.Length < 4)
                return PasswordScore.VeryWeak;

            if (password.Length >= 8)
                score++;
            if (password.Length >= 10)
                score++;

            if (Regex.Match(password, @"\d", RegexOptions.ECMAScript).Success)
            {
                score++;
            }

            if (Regex.Match(password, @"[a-z]", RegexOptions.ECMAScript).Success && Regex.Match(password, @"[A-Z]", RegexOptions.ECMAScript).Success)
            {
                score++;
            }

            if (Regex.Match(password, @"[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]", RegexOptions.ECMAScript).Success)
            {
                score++;
            }

            Debug.WriteLine($"Password: {password}, Length: {password.Length}, Score: {score}");
            return (PasswordScore)score;
        }
    }
}