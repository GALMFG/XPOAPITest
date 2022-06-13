using System;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace XPOAPITest
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormUserInterface());

            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
            .Build();

            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    ConfigureServices(services, config);
                })
                .UseConsoleLifetime()
                .UseSerilog();

            ConfigureLogging(config);
        }
        public static void ConfigureLogging(IConfiguration config)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var outputTemplate = "[{Timestamp:yyyy:MM:dd HH:mm:ss} {SourceContext} {Level:u3} {RequestId} {MemberName}({LineNumber})] - {Message}{NewLine}{Exception}";

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .WriteTo.Console(outputTemplate: outputTemplate)
                .WriteTo.File(Environment.CurrentDirectory + $"\\logs\\main.log", retainedFileCountLimit: 40, fileSizeLimitBytes: 10000000, rollOnFileSizeLimit: true, outputTemplate: outputTemplate)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Environment", environment)
                .CreateLogger();
        }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="config">The config.</param>
        private static void ConfigureServices(
            IServiceCollection services,
            IConfiguration config)
        {

        }
    }
}
