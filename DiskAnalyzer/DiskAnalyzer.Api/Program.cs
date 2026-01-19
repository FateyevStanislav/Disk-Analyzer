using DiskAnalyzer.Api.Converters;
using DiskAnalyzer.Api.Modules;
using DiskAnalyzer.Api.Validation;
using DiskAnalyzer.Api.Validation.Filters;
using DiskAnalyzer.Domain.Abstractions;
using DiskAnalyzer.Domain.Abstractions.Services;
using DiskAnalyzer.Domain.Models.Filters;
using DiskAnalyzer.Domain.Models.Results;
using DiskAnalyzer.Domain.Services;
using DiskAnalyzer.Infrastructure.Repositories;
using System.Text.Json.Serialization;

ApiReflection.InitData();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IRepository<AnalysisResult>>(
    new InMemoryRepository<AnalysisResult>(
        getIdFunc: r => r.Id,
        getDateFunc: r => r.CreatedAt
    )
);
builder.Services.AddSingleton<IFileSystemScanner, DirectoryWalker>();
builder.Services.AddScoped<IFilesMeasurer, FilesMeasurer>();
builder.Services.AddScoped<IFilesGrouper, FilesGrouper>();
builder.Services.AddScoped<IDuplicatesFinder, DuplicatesFinder>();

FilterValidation.RegisterValidator(typeof(SizeFilter), new SizeFilterValidator());
var tv = new TimeValidator();
FilterValidation.RegisterValidator(typeof(AccessTimeFilter), tv);
FilterValidation.RegisterValidator(typeof(CreationTimeFilter), tv);
FilterValidation.RegisterValidator(typeof(WriteTimeFilter), tv);

builder.Services.AddControllers().AddJsonOptions(
    options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.Converters.Add(new TypeJsonConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.UseAllOfForInheritance();
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();