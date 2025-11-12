using CommunityToolkit.Mvvm.Messaging.Messages;

namespace TotpQr;

public class ModelSavedMessage(TotpUriDataModel value)
    : ValueChangedMessage<TotpUriDataModel>(value)
{ }
