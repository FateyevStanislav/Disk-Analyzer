namespace DiskAnalyzer.Domain.Abstractions;

/// <summary>
/// Композитный фильтр, объединяющий несколько фильтров с логикой AND.
/// Файл проходит проверку только если ВСЕ внутренние фильтры вернули true.
/// </summary>
public interface ICompositeFilter : IFileFilter
{
    /// <summary>
    /// Список вложенных фильтров (только для чтения).
    /// </summary>
    IReadOnlyList<IFileFilter> Filters { get; }

    /// <summary>
    /// Добавляет фильтр в композицию.
    /// </summary>
    void Add(IFileFilter filter);
}