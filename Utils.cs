namespace WebApi.Utils;
using System.Security.Cryptography;

public static class Utils {
    public static byte[] GenerateRandomKey(int length) {
        byte[] key = new byte[length];

        if (key.Length * 8 < 128)
        {
            // Key size is insufficient for HMAC-SHA256
            Console.WriteLine("Error: Key size is too small for HMAC-SHA256.");
        }
        else
        {
            // Key size is sufficient
            Console.WriteLine("Key size is sufficient for HMAC-SHA256.");
        }

        using (RandomNumberGenerator rng = RandomNumberGenerator.Create()) {
            rng.GetBytes(key);
        }

        return key;
    }

    public static string ConvertKeyToString(byte[] key)
    {
        return Convert.ToBase64String(key);
    }
}