using PokerVisionAI.Domain.Dtos;
using System.Text.Json;

namespace PokerVisionAI.Features.InitialConfig.TableMap;

public class GetAllTableMaps
{
    public async Task<List<TableMapDTO>> Executesync()
    {
        try
        {
            var customFileType = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
            { DevicePlatform.WinUI, new[] { ".json" } },
            { DevicePlatform.iOS, new[] { "public.json" } },
            { DevicePlatform.Android, new[] { "application/json" } },
            { DevicePlatform.macOS, new[] { "json" } }
                });

            var options = new PickOptions
            {
                PickerTitle = "Por favor seleccione un archivo JSON",
                FileTypes = customFileType
            };

            var result = await FilePicker.Default.PickAsync(options);

            if (result != null)
            {
                if (result.FileName.EndsWith("json", StringComparison.OrdinalIgnoreCase))
                {
                    // Leer el contenido del archivo
                    using var stream = await result.OpenReadAsync();
                    using var reader = new StreamReader(stream);
                    var jsonContent = await reader.ReadToEndAsync();

                    // Deserializar el JSON a una lista de RegionDTO
                    var tableMap = JsonSerializer.Deserialize<List<TableMapDTO>>(jsonContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return tableMap;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Por favor seleccione un archivo JSON válido", "OK");
                    return null;
                }
            }
            else
            {
                // Usuario canceló la selección
                return null;
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Error al cargar el archivo: {ex.Message}", "OK");
            return null;
        }
    }
}
