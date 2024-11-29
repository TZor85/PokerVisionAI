using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PokerVisionAI.App.Services;
using PokerVisionAI.Domain.Options;
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
                var encrypterOptions = builder.Configuration.GetSection("Encrypter").Get<EncrypterOptions>();
                var tableMapOptions = builder.Configuration.GetSection("TableMaps").Get<TableMapOptions>();
                var regionOptions = builder.Configuration.GetSection("Regions").Get<RegionOptions>();
                var cardOptions = builder.Configuration.GetSection("Cards").Get<CardOptions>();

                builder.Services.Configure<EncrypterOptions>(options =>
                {
                    { options.Key = encrypterOptions?.Key ?? string.Empty; }
                });

                builder.Services.Configure<TableMapOptions>(options =>
                {
                    { options.PokerTable = tableMapOptions?.PokerTable ?? new List<string>(); }
                });

                builder.Services.Configure<RegionOptions>(options =>
                {
                    { options.Url = regionOptions?.Url ?? string.Empty; }
                    { options.Path = regionOptions?.Path ?? string.Empty; }
                });

                builder.Services.Configure<CardOptions>(options =>
                {
                    { options.Url = cardOptions?.Url ?? string.Empty; }
                    { options.Path = cardOptions?.Path ?? string.Empty; }
                });

                builder.Services.AddMauiBlazorWebView();
                builder.Services.AddDataBase(builder.Configuration, true);
                builder.Services.AddUseCases();

                builder.Services.AddScoped<OcrService>();
                builder.Services.AddScoped<ColorDetectionService>();
                builder.Services.AddScoped<ImageNavigatorService>();
                builder.Services.AddScoped<ImageCropperService>();
                builder.Services.AddScoped<FileProcessorService>();
                builder.Services.AddScoped<WindowCaptureService>();

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
