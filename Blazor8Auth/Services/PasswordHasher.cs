using Blazor8Auth.Services;
using System.Security.Cryptography;
using Blazor8Auth.Services.Interfaces;

namespace Blazor8Auth.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16; // Salt size
        private const int HashSize = 32; // Hash size
        private const int Iterations = 100000;

        private readonly HashAlgorithmName _algorithm = HashAlgorithmName.SHA512; // Algorithm type

        public string Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize); // Generates salt
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, _algorithm, HashSize); // Generates hash with password and salt
            return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
            
        }

        public bool Verify(string password, string? PasswordHash)
        {
            var parts = PasswordHash.Split('-'); // Splits hash and salt
            var hash = Convert.FromHexString(parts[0]); // Point out hash
            var salt = Convert.FromHexString(parts[1]); // Point out salt

            var inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, _algorithm, HashSize);

            return CryptographicOperations.FixedTimeEquals(hash, inputHash); // Compares hash with input hash (input password)
        }
    }
}
