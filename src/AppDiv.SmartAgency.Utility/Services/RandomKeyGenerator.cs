

using System.Security.Cryptography;

namespace AppDiv.SmartAgency.Utility.Services;
public class RandomKeyGenerator
{
    public static string generateRandomKey()
    {
        // Generate a random key with 256 bits (32 bytes)
        var keyBytes = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(keyBytes);
        }
        var base64Key = Convert.ToBase64String(keyBytes);
        return base64Key;
    }
}