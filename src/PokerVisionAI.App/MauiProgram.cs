using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PokerVisionAI.Domain.Options;
using PokerVisionAI.Features;
using PokerVisionAI.Infrastructure;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace PokerVisionAI.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            try
            {
                var builder = MauiApp.CreateBuilder();
                builder
                    .UseMauiApp<App>()
                    .ConfigureFonts(fonts =>
                    {
                        fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    });

                using var appsettingsStream = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream("PokerVisionAI.App.wwwroot.appsettings.json");

                if (appsettingsStream != null)
                {
                    var config = new ConfigurationBuilder()
                        .AddJsonStream(appsettingsStream)
                        .Build();

                    builder.Configuration.AddConfiguration(config);
                }
                var encrypterOptions = builder.Configuration.GetSection("Encrypter").Get<EncrypterOptions>();
                var tableMapOptions = builder.Configuration.GetSection("TableMaps").Get<TableMapOptions>();

                builder.Services.Configure<EncrypterOptions>(options =>
                {
                    { options.Key = encrypterOptions?.Key ?? string.Empty; }
                });

                builder.Services.Configure<TableMapOptions>(options =>
                {
                    { options.PokerTable = tableMapOptions?.PokerTable ?? new List<string>(); }
                });

                builder.Services.AddMauiBlazorWebView();
                builder.Services.AddDataBase(builder.Configuration, true);
                builder.Services.AddUseCases();

                builder.Services.AddMauiBlazorWebView();
                builder.Services.AddDataBase(builder.Configuration, true);
                builder.Services.AddUseCases();

#if DEBUG
                builder.Services.AddBlazorWebViewDeveloperTools();
                builder.Logging.AddDebug();
#endif

                return builder.Build();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al iniciar la app: {ex}");
                throw;
            }
        }
    }
}
