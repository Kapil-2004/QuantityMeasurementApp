using System.Security.Cryptography;
using System.Text;

namespace QuantityMeasurement.AuthService.Services.Security
{
    public static class SecurityHelper
    {
        private const int Iterations = 10000;
        private const int HashSize = 32;
        private const int SaltSize = 16;

        /// <summary>
        /// Hashes a password using PBKDF2 algorithm (HMACSHA256).
        /// Returns a string formatted as "Salt:Hash".
        /// </summary>
        public static string HashPassword(string password)
        {
            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(HashSize);
                return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
            }
        }

        /// <summary>
        /// Verifies a password against a stored PBKDF2 hash.
        /// </summary>
        public static bool VerifyPassword(string password, string storedHash)
        {
            var parts = storedHash.Split(':');
            if (parts.Length != 2) return false;

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] hash = Convert.FromBase64String(parts[1]);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
            {
                byte[] testHash = pbkdf2.GetBytes(HashSize);
                return CryptographicOperations.FixedTimeEquals(hash, testHash);
            }
        }
    }
}
