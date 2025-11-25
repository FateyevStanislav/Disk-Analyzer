# DiskAnalyzer

## Инфраструктурный код

- **Репозитории и данные:**  
  - `IMeasurmentRecordRepository.cs` — интерфейс для хранения и доступа к измерениям.  
  - `ConcDictRepository.cs` — реализация репозитория для словарных данных.  
  - `Entity.cs`, `ValueType.cs` — базовые сущности и типы данных.

- **Логирование:**  
  - `Logger.cs` — класс для логирования событий и ошибок.  
  - `Log.cs`, `LogType.cs` — модели и типы логов.

- **Работа с файлами:**  
  - `FileInfoExtensions.cs` — методы расширения для объектов файлов.  
  - `DirectoryWalker.cs` — класс обхода каталогов.

- **Фильтры и группировщики:**  
  - Интерфейсы `IFileFilter.cs`, `IGrouper.cs` и их реализации:  
    - `ExtensionFilter.cs`, `SizeFilter.cs`, `AccessTimeFilter.cs`, `CreationTimeFilter.cs`, `WriteTimeFilter.cs` — фильтры файлов по разным критериям.  
    - `ExtensionGrouper.cs`, `SizeBucketGrouper.cs`, `LastAccessTimeGrouper.cs` — группировщики для файлов.

Этот инфраструктурный слой обеспечивает базовые службы работы с данными, файловой системой, логированием и метриками, обеспечивая модульность и расширяемость приложения DiskAnalyzer.

## Доменный слой

- **Базовые интерфейсы и модели:**  
  - `IMetric.cs`, `IRecord.cs`, `IGroupingMeasurment.cs`, `IDirectoryMeasurment.cs` — контракты для метрик, записей и измерений.  
  - `BaseMetric.cs`, `BaseRecord.cs` — базовые реализации метрик и записей.  
  - `DirectoryMeasurmentRecord.cs` — модель записи измерения директории.

- **Метрики и измерения:**  
  - `FileCountMetric.cs`, `FileSizeMetric.cs`, `GroupCountMetric.cs`, `GroupSizeMetric.cs` — метрики для подсчёта файлов, их размеров и группировки.  
  - `FilesCountMeasurment.cs`, `FilesSizeMeasurment.cs`, `GroupingMeasurment.cs` — конкретные измерения с логикой подсчётов.

- **Группировки и записи:**  
  - `FileGrouping.cs`, `GroupingRecord.cs` — сущности для группировки файлов и соответствующих записей.

- **Форматтеры для отображения значений:**  
  - `IValueFormatter.cs`, `CountFormatter.cs`, `SizeFormatter.cs` — форматтеры для представления значений метрик в удобном виде.

Доменный слой описывает бизнес-модель приложения DiskAnalyzer, включая основные сущности, метрики, измерения и правила представления информации[IDirectoryMeasurment.cs, FilesSizeMeasurment.cs, SizeFormatter.cs и др.].