using System.Security.Cryptography;
using System.Text;

namespace Maus.Domain.Extensions;

public static class HashExtensions
{
    public static string ToSha256String(this string value)
    {
        return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(value)));
    }
}