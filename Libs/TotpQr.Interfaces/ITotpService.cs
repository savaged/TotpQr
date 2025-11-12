namespace TotpQr.Interfaces;

public interface ITotpService
{
    string GenerateCode(string base32Secret);

    bool ValidateCode(string base32Secret, string code);
}
