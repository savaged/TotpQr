using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using TotpQr.Interfaces;

namespace TotpQr;

public class TotpUriDataIndexViewModel : ModelViewModel
{
    public ObservableCollection<TotpUriDataModel> Index { get; } = [];

    public TotpUriDataIndexViewModel(IModelService modelService)
        : base(modelService)
    {
        WeakReferenceMessenger.Default.Register<ModelChangedMessage>(this, OnModelChanged);
        WeakReferenceMessenger.Default.Register<ModelSavedMessage>(this, OnModelSaved);
    }

    public async Task LoadIndexAsync()
    {
        Index.Clear();
        var items = await ModelService.IndexAsync<TotpUriDataModel>();
        if (items?.Count() == 0)
        {
            Index.Add(TotpUriDataModel.GetExample());
            return;
        }
        foreach (var item in items!)
            Index.Add(item);
    }

    public event EventHandler NewModel = delegate { };

    private void OnModelChanged(object recipient, ModelChangedMessage m)
    {
        if (m.Value == TotpUriDataModel.Empty)
            NewModel.Invoke(this, EventArgs.Empty);
    }

    private void OnModelSaved(object recipient, ModelSavedMessage m)
    {
        if (Index.Contains(m.Value))
            Index.Remove(m.Value);
        Index.Add(m.Value);
    }

}
