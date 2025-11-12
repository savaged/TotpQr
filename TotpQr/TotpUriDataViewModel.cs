using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.ComponentModel;
using TotpQr.Interfaces;
using QRCoder;

namespace TotpQr;

public partial class TotpUriDataViewModel : ModelViewModel
{
    private readonly ITotpService _totpService;

    public TotpUriDataViewModel(IModelService modelService, ITotpService totpService)
        : base(modelService)
    {
        ArgumentNullException.ThrowIfNull(totpService);
        _totpService = totpService;
        PropertyChanged += OnPropertyChanged;
        WireModelPropertyChanged();
    }

    [ObservableProperty]
    private TotpUriDataModel? model = new();

    [ObservableProperty]
    private byte[] qrCode = [];

    [ObservableProperty]
    private int totpCode = 0;

    [ObservableProperty]
    private bool isQrCoded;

    [ObservableProperty]
    private bool isValidQrCode;

    private bool HasSecret => Model is not null && !string.IsNullOrWhiteSpace(Model.Secret);

    public bool CanSave => Model is not null
        && !string.IsNullOrWhiteSpace(Model.Site)
        && !string.IsNullOrWhiteSpace(Model.Email)
        && !string.IsNullOrWhiteSpace(Model.Secret);

    public bool CanVerify => IsQrCoded;

    [RelayCommand(CanExecute = nameof(CanSave))]
    public async Task SaveAsync()
    {
        await ModelService.SaveAsync(Model!);
        WeakReferenceMessenger.Default.Send(new ModelSavedMessage(Model!));
    }

    [RelayCommand]
    public void New() => Model = TotpUriDataModel.Empty;

    [RelayCommand(CanExecute = nameof(CanVerify))]
    public void Verify()
    {
        if (!HasSecret)
            return;
        IsValidQrCode =
            _totpService.ValidateCode(Model!.Secret.Base32Encoded(), TotpCode.ToString("D5"));
    }

    private void GenerateQr()
    {
        IsQrCoded = false;
        if (!HasSecret)
            return;
        QrCode = PngByteQRCodeHelper.GetQRCode(Model!.Uri, QRCodeGenerator.ECCLevel.Q, 20);
        IsQrCoded = true;
        VerifyCommand?.NotifyCanExecuteChanged();
    }

    private void WireModelPropertyChanged()
    {
        if (Model is not null)
        {
            Model.PropertyChanged -= OnModelPropertyChanged;
            Model.PropertyChanged += OnModelPropertyChanged;
        }
    }

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Model))
        {
            WireModelPropertyChanged();
            GenerateQr();
            WeakReferenceMessenger.Default.Send(new ModelChangedMessage(Model!));
        }
    }

    private void OnModelPropertyChanged(object? sender, PropertyChangedEventArgs e) =>
        SaveCommand?.NotifyCanExecuteChanged();

}
