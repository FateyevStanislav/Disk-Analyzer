using DiskAnalyzer.Api.Converters;
using DiskAnalyzer.Api.Modules;
using System.Text.Json.Serialization;

ApiReflection.InitData();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(
    options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        //options.JsonSerializerOptions.Converters.Add(new MetricJsonConverter());
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