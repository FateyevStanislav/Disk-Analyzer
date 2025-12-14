using DiskAnalyzer.Domain.Models.Results;

namespace DiskAnalyzer.Domain.Abstractions;

/// <summary>
/// Репозиторий для сохранения и получения результатов анализа.
/// </summary>
/// <remarks>
/// Работает с полиморфной иерархией <see cref="AnalysisResult"/>.
/// Реализация должна корректно сериализовывать/десериализовывать подтипы.
/// </remarks>
public interface IRepository
{
    /// <summary>
    /// Добавляет результат анализа в хранилище.
    /// </summary>
    /// <param name="record">Результат для сохранения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task AddAsync(AnalysisResult record, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает результат по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор результата</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// Найденный результат или <c>null</c>, если не найден.
    /// Возвращается конкретный подтип (<see cref="DuplicateAnalysisResult"/>, и т.д.).
    /// </returns>
    Task<AnalysisResult?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает все результаты анализа.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// Список всех результатов
    /// </returns>
    Task<IReadOnlyList<AnalysisResult>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает все результаты в остортированном виде.
    /// </summary>
    /// <param name="descending">true - по убыванию, false - по возрастанию.</param>
    /// <returns>
    /// Отстортированный список всех результатов
    /// </returns>
    Task<IReadOnlyList<AnalysisResult>> GetAllOrderedAsync(
        bool descending = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет результат по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор результата</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns><c>true</c> если запись была удалена, <c>false</c> если не найдена.</returns>
    Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default);

    Task<int> CountAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет все результаты. ОСТОРОЖНО: операция необратима!
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task ClearAsync(CancellationToken cancellationToken = default);
}