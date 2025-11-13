using disk_analyzer.Application;
using Microsoft.AspNetCore.Builder;
using NUnitLite;

namespace DiskAnalyzer;

public class Program
{
    public static void Main(string[] args)
    {
        new AutoRun().Execute(args);
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();
        WeightingService.InitService(app);
        app.Run();
    }
}
