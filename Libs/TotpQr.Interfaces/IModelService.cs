namespace TotpQr.Interfaces;

public interface IModelService
{
    Task<IEnumerable<T>> IndexAsync<T>() where T : IModel;

    Task SaveAsync<T>(T model) where T : class, IModel;
}
