using System.Text;
using System.Security.Cryptography;

namespace EFCoreAPI.Services.Helpers
{

    public static class PasswordHasher
    {
        public static string HashPassword(string password, string salt)
        {
            using var sha256 = SHA256.Create();
            var combinedBytes = Encoding.UTF8.GetBytes(password + salt);
            var hashBytes = sha256.ComputeHash(combinedBytes);

            return Convert.ToHexString(hashBytes); // 64 caracteres hex
        }

        public static bool VerifyPassword(string password, string salt, string hash)
        {
            var computedHash = HashPassword(password, salt);
            return computedHash.Equals(hash, StringComparison.OrdinalIgnoreCase);
        }
    }
}
