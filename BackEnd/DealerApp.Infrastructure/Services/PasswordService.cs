using System;
using System.Linq;
using System.Security.Cryptography;
using DealerApp.Core.Interfaces;
using DealerApp.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace DealerApp.Infrastructure.Services
{
    public class PasswordService : IPasswordHasher
    {
        private readonly PasswordOptions _passwordOptions;
        public PasswordService(IOptions<PasswordOptions> passwordOptions)
        {
            _passwordOptions = passwordOptions.Value;
        }
        public bool Check(string hash, string password)
        {
            var parts = hash.Split('.');
            if (parts.Length != 3)
            {
                throw new FormatException("Unespected Hash Format");
            }

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            using (var algoritm = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA512))
            {
                var keyToCheck = algoritm.GetBytes(_passwordOptions.KeySize);
                return keyToCheck.SequenceEqual(key);
            }
        }

        public string Hash(string password)
        {
            using (var algoritm = new Rfc2898DeriveBytes(password, _passwordOptions.SaltSize,
                                    _passwordOptions.Iterations, HashAlgorithmName.SHA512))
            {
                var key = Convert.ToBase64String(algoritm.GetBytes(_passwordOptions.KeySize));
                var salt = Convert.ToBase64String(algoritm.Salt);
                return $"{_passwordOptions.Iterations}.{salt}.{key}";
            }
        }
    }
}