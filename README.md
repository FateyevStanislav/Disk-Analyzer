# DiskAnalyzer

## Инфраструктурный код

- **Репозитории и данные:**  
  - `IMeasurementRecordRepository.cs` — интерфейс для хранения и доступа к измерениям.  
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
  - `IMetric.cs`, `IRecord.cs`, `IGroupingMeasurement.cs`, `IDirectoryMeasurement.cs` — контракты для метрик, записей и измерений.  
  - `BaseMetric.cs`, `BaseRecord.cs` — базовые реализации метрик и записей.  
  - `DirectoryMeasurementRecord.cs` — модель записи измерения директории.

- **Метрики и измерения:**  
  - `FileCountMetric.cs`, `FileSizeMetric.cs`, `GroupCountMetric.cs`, `GroupSizeMetric.cs` — метрики для подсчёта файлов, их размеров и группировки.  
  - `FilesCountMeasurement.cs`, `FilesSizeMeasurement.cs`, `GroupingMeasurement.cs` — конкретные измерения с логикой подсчётов.

- **Группировки и записи:**  
  - `FileGrouping.cs`, `GroupingRecord.cs` — сущности для группировки файлов и соответствующих записей.

- **Форматтеры для отображения значений:**  
  - `IValueFormatter.cs`, `CountFormatter.cs`, `SizeFormatter.cs` — форматтеры для представления значений метрик в удобном виде.

Доменный слой описывает бизнес-модель приложения DiskAnalyzer, включая основные сущности, метрики, измерения и правила представления информации[IDirectoryMeasurement.cs, FilesSizeMeasurement.cs, SizeFormatter.cs и др.].

## API

- **Контроллеры:**
  - `AnalysisControllerBase.cs` — базовый класс для контроллеров анализа 
  - `DuplicateFinderController.cs` — контроллер для запросов поиска дубликатов
  - `FilesMeasurementsController.cs` — контроллер для запросов на измерение файлов
  - `GroupingMeasurementsController.cs` — контроллер для запросов на группировку файлов
  - `HistoryController.cs` — контроллер для работы с иторией запросов
  - `RequestInfoController.cs` — контроллер для получения информации об актуальных фильтрах

- **DTO (Data Transfer Object):**
  - `DuplicateFinderDto.cs` — данные для запроса на поиск дубликатов
  - `FilesMeasurementDto.cs` — данные для запроса на измерение файлов
  - `GroupingMeasurementDto.cs` — данные для запроса на группировку файлов
  - `FilterDto.cs` — данные для добавления фильтра к запросу

- **Фабрики и конвертеры**
  - `FilesMesurementFactory.cs, FilterFactory.cs, GrouperFactory.cs` — фабрики для создания объектов измерений, фильтров и группировщиков на основе DTO
  - `TypeJsonConverter.cs` — конвертер для удобной конвертации Type

- **Валидация**
  - `FilterValidation.cs` — класс валидации фильтров
  - `IFilterValidator.cs` — интерфейс валидатора фильтра
  - `SizeFilterValidator.cs` — валидатор фильта размера
  - `TimeValidator.cs` — универсальных валидатор фильтров времени
  - `ExistingPathAttribute.cs` — аттрибут параметра DTO для проверки корректности пути
  
- **Модули**
  - `ApiReflection.cs` — класс содержащий данные рефлексии, нужные для работы приложения

API слой обеспечивает взаимодействие с приложением через HTTP-запросы и предоставляет клиентам доступ к функционалу DiskAnalyzer
