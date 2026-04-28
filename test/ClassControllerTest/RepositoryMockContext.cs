using ClassController.Abstractions;
using Moq;

namespace ClassControllerTest;

/// <summary>
/// Provides a reusable repository mock context for controller tests.
/// </summary>
internal sealed class RepositoryMockContext<T>
{
    private List<T> persistedData;

    /// <summary>
    /// Initializes a new instance of the <see cref="RepositoryMockContext{T}"/> class.
    /// </summary>
    /// <param name="seedData">The initial data returned by the mocked repository.</param>
    /// <param name="saveResult">The value returned when the mocked repository saves data.</param>
    public RepositoryMockContext(IEnumerable<T>? seedData = null, bool saveResult = true)
    {
        this.persistedData = seedData?.ToList() ?? [];
        this.SaveResult = saveResult;
        this.Mock = new Mock<IRepository<T>>(MockBehavior.Strict);

        this.Mock
            .Setup(repository => repository.LoadData())
            .Returns(() => this.persistedData);

        this.Mock
            .Setup(repository => repository.SaveData(It.IsAny<List<T>>()))
            .Callback<List<T>>(data =>
            {
                this.LastSavedSnapshot = data.ToList();
                this.persistedData = data.ToList();
            })
            .Returns(() => this.SaveResult);
    }

    /// <summary>
    /// Gets the configured repository mock.
    /// </summary>
    public Mock<IRepository<T>> Mock { get; }

    /// <summary>
    /// Gets or sets the value returned by the mocked save operation.
    /// </summary>
    public bool SaveResult { get; set; }

    /// <summary>
    /// Gets the last data snapshot passed to the mocked save operation.
    /// </summary>
    public List<T>? LastSavedSnapshot { get; private set; }
}
