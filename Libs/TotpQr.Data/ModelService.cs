using TotpQr.Interfaces;

namespace TotpQr.Data;

public class ModelService : IModelService
{
    public async Task<IEnumerable<T>> IndexAsync<T>()
        where T : IModel
    {
        // Simulate asynchronous operation
        await Task.Delay(100);
        // Return an empty list for demonstration purposes
        return [];
    }

    public async Task SaveAsync<T>(T model)
        where T : class, IModel
    {
        // Simulate asynchronous operation
        await Task.Delay(100);
        // Implement saving logic here
    }

}
