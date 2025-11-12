using ByteDev.Encoding.Base32;

namespace TotpQr;

public static class PlainTextSecretEx
{
    public static string Base32Encoded(this string s) =>
        string.IsNullOrWhiteSpace(s) ? string.Empty :
        new Base32Encoder().Encode(s).RemoveBase32EndPadding();

    private static string RemoveBase32EndPadding(this string s) => s.TrimEnd('=');
}
