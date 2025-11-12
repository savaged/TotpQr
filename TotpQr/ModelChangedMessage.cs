using CommunityToolkit.Mvvm.Messaging.Messages;

namespace TotpQr;

public class ModelChangedMessage(TotpUriDataModel value)
    : ValueChangedMessage<TotpUriDataModel>(value)
{ }
