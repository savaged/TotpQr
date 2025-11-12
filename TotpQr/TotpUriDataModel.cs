using CommunityToolkit.Mvvm.ComponentModel;
using TotpQr.Interfaces;

namespace TotpQr;

public partial class TotpUriDataModel : ObservableObject, IModel
{
    private string _uri = string.Empty;

    public static readonly TotpUriDataModel Empty = new();

    public int Id { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Uri))]
    private string site = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Uri))]
    private string email = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Uri))]
    private string secret = string.Empty;

    private const string URI_PREFIX = "otpauth://totp/";
    private const string URI_POSTFIX = "&algorithm=SHA1&digits=6&period=30";

    public string Uri => string.IsNullOrEmpty(_uri) ? GetUri(this) : _uri;

    public override int GetHashCode() =>
        HashCode.Combine(Id, Site, Email, Secret);

    public bool Equals(IModel? other) =>
        other is TotpUriDataModel otherModel
        && GetHashCode() == otherModel.GetHashCode();

    public static bool operator ==(TotpUriDataModel? left, TotpUriDataModel? right) =>
        Equals(left, right);

    public static bool operator !=(TotpUriDataModel? left, TotpUriDataModel? right) =>
        !Equals(left, right);

    public override bool Equals(object? o)
    {
        switch (o)
        {
            case null:
                return false;
            default:
                if (o is IModel m && Equals(m))
                    return true;
                break;
        }
        return false;
    }

    public static TotpUriDataModel GetExample()
    {
        TotpUriDataModel eg = new()
        {
            Site = "test.com",
            Email = "me@test.com",
            Secret = "portal1"
        };
        eg._uri = GetUri(eg);
        return eg;
    }

    private static string GetUri(TotpUriDataModel self) =>
        GetUri(self.Site, self.Email, self.Secret);

    private static string GetUri(string site, string email, string secret) =>
        $"{URI_PREFIX}{site}:{email}?secret={secret.Base32Encoded()}&issuer={site}{URI_POSTFIX}";

}
    
