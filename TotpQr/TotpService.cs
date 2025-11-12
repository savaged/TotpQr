using TotpQr.Interfaces;
using OtpNet;
using ByteDev.Encoding.Base32;

namespace TotpQr;

public class TotpService : ITotpService
{
    public string GenerateCode(string base32Secret) =>
        new Totp(GetSecretKey(base32Secret)).ComputeTotp();

    public bool ValidateCode(string base32Secret, string code) =>
        new Totp(GetSecretKey(base32Secret)).VerifyTotp(
            code, out _, VerificationWindow.RfcSpecifiedNetworkDelay);

    private static byte[] GetSecretKey(string base32Secret) =>
        new Base32Encoder().DecodeToBytes(base32Secret);
}
