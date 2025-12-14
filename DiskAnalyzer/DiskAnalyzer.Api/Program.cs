using DiskAnalyzer.Api.Converters;
using DiskAnalyzer.Api.Modules;
using DiskAnalyzer.Domain.Abstractions;
using DiskAnalyzer.Domain.Services;
using DiskAnalyzer.Infrastructure.FileSystem;
using DiskAnalyzer.Infrastructure.Repositories;
using System.Text.Json.Serialization;

ApiReflection.InitData();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IRepository, InMemoryRepository>();
builder.Services.AddScoped<IFileSystemScanner, DirectoryWalker>();
builder.Services.AddScoped<FilesMeasurer>();
builder.Services.AddScoped<FilesGrouper>();
builder.Services.AddScoped<DuplicatesFinder>();

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