using CommunityToolkit.Mvvm.ComponentModel;
using TotpQr.Interfaces;

namespace TotpQr;

public abstract class ModelViewModel : ObservableRecipient
{
    protected IModelService ModelService { get; }

    protected ModelViewModel(IModelService modelService)
    {
        ArgumentNullException.ThrowIfNull(modelService);
        ModelService = modelService;
    }
}
