using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;
using PokerVisionAI.Features;
using PokerVisionAI.Infrastructure;
using System.Reflection;

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
