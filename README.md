# Disk Analyzer

Система для анализа файловой системы с возможностями поиска дубликатов, группировки файлов по различным критериям и измерения характеристик файлов. Проект построен на архитектуре с разделением на слои и состоит из REST API бэкенда и desktop клиента.

## Возможности

- **Поиск дубликатов** - обнаружение файлов с идентичным содержимым на основе хэширования
- **Группировка файлов** - организация файлов по расширению, размеру или времени последнего доступа
- **Измерения** - подсчет количества файлов и вычисление общего размера
- **Фильтрация** - гибкая система фильтров по размеру, расширению, времени создания/изменения/доступа
- **История операций** - сохранение и просмотр результатов предыдущих анализов
- **Динамическая конфигурация** - получение информации о доступных фильтрах и группировщиках через API

## Архитектура

Проект следует принципам Clean Architecture и разделен на следующие слои:

### Domain Layer
- **Abstractions** - контракты для основной бизнес-логики (`IFileFilter`, `IFileGrouper`, `IFilesMeasurement`, `IDuplicatesFinder`, `IFileSystemScanner`)
- **Models** - доменные модели и их реализации:
  - Core models: `FileDetails`, `FilesGroup`, `DuplicateGroup`, `FilterInfo`, `GrouperInfo`
  - Filters: `SizeFilter`, `ExtensionFilter`, `AccessTimeFilter`, `CreationTimeFilter`, `WriteTimeFilter`, `CompositeFilter`
  - Groupers: `ExtensionGrouper`, `SizeBucketGrouper`, `LastAccessTimeGrouper`
  - Measurements: `TotalSizeMeasurement`, `FilesCountMeasurement`
  - Results: `AnalysisResult`, `DuplicateAnalysisResult`, `GroupingAnalysisResult`, `MeasurementAnalysisResult`
- **Services** - реализация бизнес-логики (`DuplicatesFinder`, `FilesGrouper`, `FilesMeasurer`, `DirectoryWalker`)
- **Attributes** - метаданные для типов (`FilterTypeAttribute`, `FilterInfoAttribute`, `GrouperTypeAttribute`)
- **Extensions** - вспомогательные методы (`FileInfoExtensions`, `FilterInfoExtensions`, `GrouperInfoExtensions`, `FileHashExtensions`)

### API Layer
- **Controllers** - REST API эндпоинты для анализа файлов
- **DTOs** - объекты передачи данных между слоями
- **Validation** - валидация входных параметров (`ExistingPathAttribute`, `FilterValidation`, validators)
- **Factories** - создание объектов фильтров, группировщиков и измерителей через рефлексию
- **Utils** - утилиты (`ApiReflection`, `TypeJsonConverter`)

### Infrastructure Layer
- **Repository** - in-memory хранилище для истории анализов (`IRepository`, `InMemoryRepository`)

### UI Layer
- **WinForms Client** - desktop приложение для взаимодействия с API
- **API Client** - HTTP клиент для связи с бэкендом (`IApiClient`, `ApiClient`)
- **Extensions** - утилиты UI (`StringExtensions`)

## Технологии

- **Backend**: C#, ASP.NET Core, .NET 6+
- **Frontend**: WinForms, .NET
- **Архитектура**: Clean Architecture, Repository Pattern, Factory Pattern, Strategy Pattern
- **API**: RESTful API, JSON
- **Validation**: Custom Validation Attributes

## API Endpoints

### Analysis Operations

#### Поиск дубликатов
```
POST /api/analysis/duplicates
Content-Type: application/json

{
  "path": "C:\\Users\\Documents",
  "filter": {
    "type": "Extension",
    "parameters": { "extension": ".pdf" }
  }
}
```

#### Группировка файлов
```
POST /api/analysis/grouping
Content-Type: application/json

{
  "path": "C:\\Users\\Downloads",
  "grouper": {
    "type": "Extension"
  },
  "filter": { ... }
}
```

#### Измерение файлов
```
POST /api/analysis/measurement
Content-Type: application/json

{
  "path": "C:\\Projects",
  "measurement": {
    "type": "TotalSize"
  },
  "filter": { ... }
}
```

### Metadata

#### Получить доступные фильтры
```
GET /api/request-info/filters
```

#### Получить доступные группировщики
```
GET /api/request-info/groupers
```

#### Получить доступные измерения
```
GET /api/request-info/measurements
```

### History

#### Получить историю анализов
```
GET /api/history
```

#### Получить конкретный результат
```
GET /api/history/{id}
```

## Структура проекта

DiskAnalyzer/
├── Domain/
│   ├── Abstractions/
│   │   ├── IFileFilter.cs
│   │   ├── ICompositeFilter.cs
│   │   ├── IFileGrouper.cs
│   │   ├── IFilesMeasurement.cs
│   │   ├── IDuplicatesFinder.cs
│   │   ├── IFilesGrouper.cs
│   │   ├── IFilesMeasurer.cs
│   │   └── IFileSystemScanner.cs
│   ├── Models/
│   │   ├── FileDetails.cs
│   │   ├── FilesGroup.cs
│   │   ├── DuplicateGroup.cs
│   │   ├── FilterInfo.cs
│   │   ├── GrouperInfo.cs
│   │   ├── Filters/
│   │   │   ├── SizeFilter.cs
│   │   │   ├── ExtensionFilter.cs
│   │   │   ├── AccessTimeFilter.cs
│   │   │   ├── CreationTimeFilter.cs
│   │   │   ├── WriteTimeFilter.cs
│   │   │   └── CompositeFilter.cs
│   │   └── Groupers/
│   │       ├── ExtensionGrouper.cs
│   │       ├── SizeBucketGrouper.cs
│   │       └── LastAccessTimeGrouper.cs
│   ├── Services/
│   │   ├── DuplicatesFinder.cs
│   │   ├── FilesGrouper.cs
│   │   ├── FilesMeasurer.cs
│   │   └── DirectoryWalker.cs
│   ├── Measurements/
│   │   ├── TotalSizeMeasurement.cs
│   │   └── FilesCountMeasurement.cs
│   ├── Results/
│   │   ├── AnalysisResult.cs
│   │   ├── DuplicateAnalysisResult.cs
│   │   ├── GroupingAnalysisResult.cs
│   │   └── MeasurementAnalysisResult.cs
│   ├── Attributes/
│   │   ├── FilterTypeAttribute.cs
│   │   ├── FilterInfoAttribute.cs
│   │   └── GrouperTypeAttribute.cs
│   └── Extensions/
│       ├── FileInfoExtensions.cs
│       ├── FilterInfoExtensions.cs
│       ├── GrouperInfoExtensions.cs
│       └── FileHashExtensions.cs
├── API/
│   ├── Controllers/
│   │   ├── AnalysisControllerBase.cs
│   │   ├── DuplicateFinderController.cs
│   │   ├── GroupingMeasurementsController.cs
│   │   ├── FilesMeasurementsController.cs
│   │   ├── HistoryController.cs
│   │   └── RequestInfoController.cs
│   ├── DTOs/
│   │   ├── DuplicateFinderDto.cs
│   │   ├── FilterDto.cs
│   │   ├── FilesMeasurementDto.cs
│   │   └── GroupingMeasurementDto.cs
│   ├── Validation/
│   │   ├── ExistingPathAttribute.cs
│   │   ├── FilterValidation.cs
│   │   ├── IFilterValidator.cs
│   │   ├── SizeFilterValidator.cs
│   │   └── TimeValidator.cs
│   ├── Factories/
│   │   ├── FilterFactory.cs
│   │   ├── GrouperFactory.cs
│   │   └── FilesMeasurementFactory.cs
│   ├── Utils/
│   │   ├── ApiReflection.cs
│   │   └── TypeJsonConverter.cs
│   ├── Program.cs
│   └── launchSettings.json
├── Infrastructure/
│   └── Repository/
│       ├── IRepository.cs
│       └── InMemoryRepository.cs
└── UI/
    ├── Forms/
    │   ├── MainWindow.cs
    │   ├── MainWindow.Designer.cs
    │   ├── ResultForm.cs
    │   └── ResultForm.Designer.cs
    ├── ApiClient/
    │   ├── IApiClient.cs
    │   └── ApiClient.cs
    ├── Extensions/
    │   └── StringExtensions.cs
    ├── Properties/
    │   ├── Resources.Designer.cs
    │   └── Settings.Designer.cs
    └── Program.cs

## Запуск проекта

### Требования

1. .NET 6.0 SDK или выше
2. Visual Studio 2022 или JetBrains Rider (опционально)

### Backend API

1. Перейдите в директорию с API проектом
2. Запустите приложение:
```
dotnet run
```
3. API будет доступен по адресу http://localhost:5000 (или указанному в launchSettings.json)

### Desktop Client

1. Убедитесь, что API запущен
2. Настройте URL API в конфигурации клиента (Settings.Designer.cs)
3. Запустите WinForms приложение:
```
dotnet run
```

### Примеры использования

#### Составной фильтр
```
{
  "type": "Composite",
  "parameters": {
    "operator": "And",
    "filters": [
      {
        "type": "Size",
        "parameters": { "minSize": 1024, "maxSize": 1048576 }
      },
      {
        "type": "Extension",
        "parameters": { "extension": ".txt" }
      }
    ]
  }
}
```

#### Группировка по размеру с фильтрацией
```
{
  "path": "C:\\Data",
  "grouper": {
    "type": "SizeBucket"
  },
  "filter": {
    "type": "CreationTime",
    "parameters": {
      "minDate": "2024-01-01T00:00:00Z",
      "maxDate": "2024-12-31T23:59:59Z"
    }
  }
}
```

#### Поиск дубликатов с фильтрацией по времени доступа
```
{
  "path": "C:\\Downloads",
  "filter": {
    "type": "AccessTime",
    "parameters": {
      "minDate": "2025-01-01T00:00:00Z",
      "maxDate": "2026-01-19T23:59:59Z"
    }
  }
}
```

## Расширяемость

Система спроектирована с учетом легкого добавления новых типов:

1. Новый фильтр - реализуйте IFileFilter в Domain/Models/Filters/ и добавьте атрибут [FilterType("YourType")]
2. Новый группировщик - реализуйте IFileGrouper в Domain/Models/Groupers/ и добавьте атрибут [GrouperType("YourType")]
3. Новое измерение - реализуйте IFilesMeasurement в Domain/Measurements/

Фабрики автоматически обнаружат новые типы через рефлексию при помощи ApiReflection.

## Паттерны проектирования

- Strategy Pattern - для фильтров, группировщиков и измерений
- Factory Pattern - динамическое создание объектов через рефлексию
- Repository Pattern - абстракция работы с хранилищем
- Composite Pattern - для составных фильтров
- Extension Methods - для обогащения функциональности моделей
- Attribute-based Metadata - для документирования и валидации типов

## Особенности реализации

1. Атрибуты для метаданных - использование FilterInfoAttribute, GrouperTypeAttribute для документирования типов
2. Валидация на уровне API - custom атрибуты валидации (ExistingPath, FilterValidation) с конкретными валидаторами
3. Расширения - extension methods для упрощения работы с FileInfo, фильтрами и группировщиками
4. Type-safe JSON - кастомный TypeJsonConverter для сериализации типов
5. In-memory история - быстрый доступ к результатам предыдущих операций через InMemoryRepository
6. Рефлексия - динамическое обнаружение доступных типов фильтров, группировщиков и измерений

## Лицензия

Этот проект является учебным и предназначен для демонстрации архитектурных паттернов и практик разработки.