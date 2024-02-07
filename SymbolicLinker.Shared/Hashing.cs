namespace SymbolicLinker.Shared;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
internal static class Hashing {
    private static readonly SHA256 SHA256 = SHA256.Create();
    public static string CalculateSHA256(Stream inputStream) {
        var CalculatedBytes = SHA256.ComputeHash(inputStream);
        var StringBits = BitConverter.ToString(CalculatedBytes);
        return StringBits.Replace("-", string.Empty);
    }
    public static async Task<string> CalculateSHA256Async(Stream inputStream) {
        return await Task.Run(() => CalculateSHA256(inputStream));
    }
}
