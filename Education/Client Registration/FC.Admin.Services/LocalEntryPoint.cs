using Serilog;
using Serilog.Events;

namespace FC.OAuth.Services;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine();
        Console.WriteLine($" ********  FireCloud Web Client App Started : {DateTime.Now.ToString()} ********");
        #region Enable Serilog
        // Log.Logger = new LoggerConfiguration()
        // .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        // .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
        // .Enrich.FromLogContext()
        // .WriteTo.Console()
        // .CreateLogger();
        #endregion
        
        CreateHostBuilder(args).Build().Run();
        //Log.Information("Serilog Enabled for  web host");
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog() // <-- This line enables Serilog
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}