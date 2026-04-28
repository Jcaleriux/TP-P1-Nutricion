using ClassController.Abstractions;

namespace ClassControllerTest;

internal sealed class InMemoryRepository<T> : IRepository<T>
{
    private readonly List<T> items;

    public InMemoryRepository(IEnumerable<T>? seedData = null)
    {
        this.items = seedData?.ToList() ?? new List<T>();
    }

    public bool SaveResult { get; set; } = true;

    public int SaveCallCount { get; private set; }

    public List<T>? LastSavedSnapshot { get; private set; }

    public bool SaveData(List<T> data)
    {
        this.SaveCallCount++;
        this.LastSavedSnapshot = data.ToList();

        this.items.Clear();
        this.items.AddRange(data);

        return this.SaveResult;
    }

    public List<T> LoadData()
    {
        return this.items;
    }
}
