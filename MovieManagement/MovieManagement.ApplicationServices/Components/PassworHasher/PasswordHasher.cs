using System.Security.Cryptography;

namespace MovieManagement.ApplicationServices.Components.PassworHasher;

public class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 128 / 8;
    private const int KeySize = 256 / 8;
    private const int Iterations = 10000;
    private readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;
    private const char Deleimter = ';';

    public string Hash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, _hashAlgorithmName, KeySize);

        return string.Join(Deleimter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
    }

    public bool Verify(string passwordHash, string inputPassword)
    {
        var hashedPassword = passwordHash.Split(Deleimter);
        var salt = Convert.FromBase64String(hashedPassword[0]);
        var hash = Convert.FromBase64String(hashedPassword[1]);

        var inputHash = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, Iterations, _hashAlgorithmName, KeySize);

        return CryptographicOperations.FixedTimeEquals(hash, inputHash);
    }
}
