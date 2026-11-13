using System.Globalization;
using System.Security.Cryptography;

namespace QuanLyDesktop;

/// <summary>
/// Băm và kiểm tra mật khẩu bằng PBKDF2-HMAC-SHA256.
/// Định dạng lưu: PBKDF2-SHA256$iterations$saltBase64$hashBase64.
/// </summary>
internal static class PasswordHasher
{
    private const string Prefix = "PBKDF2-SHA256";
    private const int DefaultIterations = 100_000;
    private const int SaltSize = 16;
    private const int KeySize = 32;
    private const int MinIterations = 10_000;
    private const int MaxIterations = 1_000_000;

    public static string Hash(string password)
    {
        ArgumentNullException.ThrowIfNull(password);

        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] key = Rfc2898DeriveBytes.Pbkdf2(
            password, salt, DefaultIterations, HashAlgorithmName.SHA256, KeySize);

        return string.Join('$',
            Prefix,
            DefaultIterations.ToString(CultureInfo.InvariantCulture),
            Convert.ToBase64String(salt),
            Convert.ToBase64String(key));
    }

    public static bool Verify(string password, string? storedHash)
    {
        if (password is null || string.IsNullOrWhiteSpace(storedHash))
        {
            return false;
        }

        string[] parts = storedHash.Split('$');
        if (parts.Length != 4 || !string.Equals(parts[0], Prefix, StringComparison.Ordinal))
        {
            return false;
        }

        if (!int.TryParse(parts[1], NumberStyles.None, CultureInfo.InvariantCulture, out int iterations)
            || iterations < MinIterations
            || iterations > MaxIterations)
        {
            return false;
        }

        byte[] salt;
        byte[] expectedKey;
        try
        {
            salt = Convert.FromBase64String(parts[2]);
            expectedKey = Convert.FromBase64String(parts[3]);
        }
        catch (FormatException)
        {
            return false;
        }

        if (salt.Length < SaltSize || expectedKey.Length == 0)
        {
            return false;
        }

        byte[] actualKey = Rfc2898DeriveBytes.Pbkdf2(
            password, salt, iterations, HashAlgorithmName.SHA256, expectedKey.Length);

        return CryptographicOperations.FixedTimeEquals(actualKey, expectedKey);
    }
}
